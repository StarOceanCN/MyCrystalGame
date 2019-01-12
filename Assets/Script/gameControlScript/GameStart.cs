using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;

namespace Game {
    public class GameStart : MonoBehaviour
    {
        GameObject playerSoldier;
        GameObject enemySoldier;
        GameObject Soldier;
        GameObject mainCamera;
        GameObject playerSoldierIns;
        GameObject enemySoldierIns;

        Vector3 playerPosition;
        Vector3 enemyPosition;

        Tilemap tilemap;

        List<Behaviour> PlayerScript;
        List<Behaviour> EnemyScript;

        private MapSelectScript script;
        void Awake() {

            Soldier = GameObject.Find("Soldier");
            gameObject.SetActive(false);

            var root = GameObject.Find("UI").transform;
            Constants.OpMenu = root.Find("OpMenu").gameObject;
            Constants.SetOpMenuVisible(false);

            Constants.GameInfo = root.Find("GameInfo").gameObject;
            Constants.SetGameInfo(false);

            Constants.PlayerId = root.Find("PlayerId").gameObject;

            Constants.GameOverPanel = root.Find("GameOver").gameObject;
            Constants.HideWinOrLoss();

            mainCamera = GameObject.Find("Main Camera");

        }
        // Use this for initialization
        void Start()
        {
            PlayerScript = new List<Behaviour>();
            EnemyScript = new List<Behaviour>();

            string playerSoldierName = playerData.Instance.isHost ? "Player1" : "Player2";
            string enemySoldierName = playerData.Instance.isHost ? "Player2" : "Player1";
            playerSoldier = CreateSoilder(playerSoldierName);
            enemySoldier = CreateSoilder(enemySoldierName);
            Constants.SetTurnNum();

            LoadColor();
            GameObjInstance();

            /*Debug.Log(camera);*/
            if (!Constants.IsYourTurn)
            {
                RefreshScript();
                StartCoroutine(GetMsg());
                Constants.OtherOneTurn();
            }
            else {
                Constants.YouTurn();
            }
        }
        private GameObject CreateSoilder(string solderName) {
            return (GameObject)Resources.Load("Prefab/"+solderName);
        }
        private void InstanceObject(GameObject Obj,Vector3 Position) {
            playerSoldierIns = Instantiate(Obj);
            Position.x += 0.5f;
            Position.y += 0.5f;
            playerSoldierIns.transform.parent = Soldier.transform;
            playerSoldierIns.GetComponent<Transform>().SetPositionAndRotation(Position, new Quaternion(0, 0, 0, 0));
            playerSoldierIns.GetComponent<EnemyAutoWalk>().enabled = false;
            /*Obj.transform.SetPositionAndRotation(Position, new Quaternion(0, 0, 0, 0));*/
            playerSoldierIns.name = "PlayerSoldier";

            Constants.camera_offset = playerSoldierIns.transform.position;
            Constants.set_camera_position = true;

            /*Debug.Log(gen_obj.GetComponent<playerControl>());*/
            PlayerScript.Add(playerSoldierIns.GetComponent<playerControl>());
        }
        private void InstanceEnemyObject(GameObject Obj, Vector3 Position) {
            enemySoldierIns = Instantiate(Obj);
            Position.x += 0.5f;
            Position.y += 0.5f;
            enemySoldierIns.transform.parent = Soldier.transform;

            enemySoldierIns.GetComponent<Transform>().SetPositionAndRotation(Position, new Quaternion(0, 0, 0, 0));
            enemySoldierIns.GetComponent<playerControl>().enabled = false;
            /*Obj.transform.SetPositionAndRotation(Position, new Quaternion(0, 0, 0, 0));*/
            /*EnemyScript.Add(gen_obj.GetComponent<playerControl>());*/

            enemySoldierIns.name = "EnemySoldier";
            mainCamera.transform.position = Constants.CheckCameraPsoiton(enemySoldierIns.transform.position);
            EnemyScript.Add(enemySoldierIns.GetComponent<EnemyAutoWalk>());
        }
        private Vector3 SetInstancePosition(bool isPlayerInstance) {
/*            Debug.Log(playerData.Instance.isHost);*/
            if (playerData.Instance.isHost&&isPlayerInstance||(!playerData.Instance.isHost)&&(!isPlayerInstance)) {
                return tilemap.CellToWorld(Constants.HostStartPosition.GetPos());
            }
            return tilemap.CellToWorld(Constants.GuestStartPosition.GetPos());
        }
        public void RefreshScript() {
            if (Constants.IsYourTurn)
            {
                foreach (var temp in EnemyScript)
                {
                    temp.enabled = false;
                }
                foreach (var temp in PlayerScript)
                {
                    temp.enabled = true;
                }
            }
            else
            {
                foreach (var temp in EnemyScript)
                {
                    temp.enabled = true;
                }
                foreach (var temp in PlayerScript)
                {
                    temp.enabled = false;
                }
            }
        }
        public void TurnedChange(string walksquence) {
            StartCoroutine(SendMsg(walksquence));
        }
        private IEnumerator SendMsg(string wksq)
        {
            
            int net_count = 0;
            int timeInterval =1;
            while (true)
            {

                SendMsgData op_data;
                op_data = new SendMsgData(Constants.Room_id,playerData.Instance.user_id, wksq);
                string sendDataJson = UnityEngine.JsonUtility.ToJson(op_data);

                UnityWebRequest request = UnityWebRequest.Put(Constants.ServerAdress, sendDataJson);
                request.method = UnityWebRequest.kHttpVerbPOST;

                yield return request.SendWebRequest();

                if (request.responseCode == 200)
                {
                    string text = request.downloadHandler.text;
                    Constants.IsYourTurn = false;
                    Constants.OtherOneTurn();

                    RefreshScript();
                    Constants.youTurnNum--;
                    /*Constants.SetOpMenuVisible(false);*/
                    StartCoroutine(GetMsg());
                    break;
                }
                net_count++;
                if (net_count > 10)
                {
                    Debug.Log("YOU OUT");
                    Application.Quit();
                    break;
                }
                Debug.Log(timeInterval);
                yield return new WaitForSeconds(timeInterval);
                timeInterval *= 2;
            }
        }
        private IEnumerator GetMsg()
        {

            int net_count = 0;
            int timeInterval = 1;
            if (!playerData.Instance.isHost && Constants.youTurnNum <= 0)
            {
                Constants.Finish();
                StartCoroutine(Finish());
            }
            else
            {
                while (true)
                {
                    GetMsgData op_data = new GetMsgData(Constants.Room_id, Constants.Enemy_id);
                    string sendDataJson = UnityEngine.JsonUtility.ToJson(op_data);

                    UnityWebRequest request = UnityWebRequest.Put(Constants.ServerAdress, sendDataJson);
                    request.method = UnityWebRequest.kHttpVerbPOST;

                    yield return request.SendWebRequest();

                    if (request.responseCode == 200)
                    {
                        string text = request.downloadHandler.text;
                        /*Debug.Log(text);*/
                        StringFromJson dat = UnityEngine.JsonUtility.FromJson<StringFromJson>(text);

                        Constants.IsYourTurn = true;
                        Constants.ClearWalkingSquence();
                        /*Debug.Log(enemySoldier.GetComponent<EnemyAutoWalk>());*/
                        Vector3 pos = new Vector3();
                        pos = Constants.camera_position;

                        Constants.camera_offset = enemySoldierIns.transform.position;


                        /* Debug.Log("offset0: " + Constants.camera_offset);*/

                        Constants.set_camera_position = true;
                        /*Debug.Log(enemySoldierIns.transform.position);*/
                        /*Constants.SetOpMenuVisible(false);*/
                        StartCoroutine(EnemyWalkTime(dat.msg, pos));
                        /* Debug.Log(get_key);*/
                        break;
                    }
                    net_count++;
                    if (net_count > 30)
                    {
                        Debug.Log("YOU OUT");
                        Application.Quit();
                        break;
                    }
                    Debug.Log(timeInterval);
                    yield return new WaitForSeconds(timeInterval);
                    timeInterval *= 2;
                    timeInterval = Mathf.Min(timeInterval, 3);
                }
            }
        }
        private IEnumerator EnemyWalkTime(string msg,Vector3 pos) {
            foreach (var temp in EnemyScript)
            {
                temp.enabled = true;
                /*Debug.Log(temp);*/
                for (int i = 0; i < msg.Length; i++)
                {
                    temp.GetComponent<EnemyAutoWalk>().EnemyWalkOneStep(msg[i]);
                    yield return new WaitForSeconds(0.4f);
                }
            }
            if (Constants.youTurnNum > 0)
            {
                foreach (var temp in PlayerScript)
                {
                    temp.enabled = true;
                }
                Constants.SetWalkCountZero();
                Constants.YouTurn();

                /*Debug.Log("offset1: " + Constants.camera_offset);*/
                Constants.camera_offset = pos;
                Constants.set_camera_position = true;
                /* Constants.SetOpMenuVisible(true);*/
            }
            else {
                Constants.Finish();
                StartCoroutine(Finish());
            }

        }
        IEnumerator  Finish() {
            yield return new WaitForSeconds(Constants.WaitTime);
            Application.Quit();
        }
        private void LoadColor() {
            GameObject root = GameObject.Find("Grid");
            GameObject obj = root.transform.Find("map").gameObject;
            tilemap = obj.GetComponent<Tilemap>();
            script = obj.GetComponent<MapSelectScript>();

            Color rcolor = script.GetRedTileColor();
            Color bcolor = script.GetBlueTileColor();
            Constants.player_color = playerData.Instance.isHost ? rcolor : bcolor;
            Constants.enemy_color = (Constants.player_color!=rcolor) ? rcolor : bcolor;
            /*Debug.Log(Constants.enemy_color);*/
        }
        private void GameObjInstance() {
            playerPosition = SetInstancePosition(true);
            enemyPosition = SetInstancePosition(false);

            /*Debug.Log(tilemap.WorldToCell(playerPosition));*/
            InstanceObject(playerSoldier, playerPosition);
            InstanceEnemyObject(enemySoldier, enemyPosition);

        }

    }
}
