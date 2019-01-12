using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

namespace Game
{
    public class ReadyPanelStartGameScript : buttonBase
    {

        protected override void Awake()
        {
            base.Awake();

            gameReady = GameObject.Find("ReadyUI");
            /*Debug.Log(gameReady);*/
            GameObject root = GameObject.Find("Grid");
            /* Debug.Log(root);*/
            gameBackGround = root.transform.Find("map").gameObject;
            /*Debug.Log(gameBackGround);*/
            gameMap = root.transform.Find("select").gameObject;
            /*Debug.Log(gameMap);*/

            baseMap = root.transform.Find("themap").gameObject;

            gameControl = GameObject.Find("Game").transform.Find("GameManager").gameObject;
            /*Debug.Log(gameControl);*/
        }
        protected override void OnMouseChick(BaseEventData baseData)
        {
            StartCoroutine(StartGame());
        }
        protected override void SetCanVisible()
        {
            gameReady.SetActive(false);
            gameMap.SetActive(true);
            gameControl.SetActive(true);
            gameBackGround.SetActive(true);
            baseMap.SetActive(true);
        }

        private IEnumerator StartGame() {
                StartGameData op_data;
                op_data = new StartGameData(playerData.Instance.user_id);
                Constants.SetRandSeed(op_data.seed);
                string sendDataJson = UnityEngine.JsonUtility.ToJson(op_data);

                UnityWebRequest request = UnityWebRequest.Put(Constants.ServerAdress, sendDataJson);
                request.method = UnityWebRequest.kHttpVerbPOST;

                yield return request.SendWebRequest();

                if (request.responseCode == 200)
                {
                    string text = request.downloadHandler.text;
                    SetCanVisible();
                    Constants.IsYourTurn = true;
                    /*Debug.Log(text);*/
                }

            /*Debug.Log("room just have one player!");*/
            Constants.SetGameInfo("一个人的游戏是不能玩的!");

        }
        public void GuestStartGame() {
            SetCanVisible();
        }
    }
}
