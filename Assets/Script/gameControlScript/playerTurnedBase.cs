using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Game
{
    public class playerTurnedBase : MonoBehaviour
    {
        [SerializeField]Behaviour[] turnControlScript;
        private bool isMyTurn;
        private bool requesting;
        private float timeInterval;
        private int net_count;
        // Use this for initialization
        void Start()
        {
            isMyTurn = true;
            requesting = false;
            timeInterval = 1.0f;
            net_count = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                DisableScript();
                isMyTurn = false;
                StartCoroutine(postMyOp());
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("close!");
                Application.Quit();
            }
//             if (requesting) {
//                 timeInterval = 1.0f;
//                 StartCoroutine(postEnumyOp());
//             }
//             if (isMyTurn)
//             {
//                 EnableScript();
//             }
        }


        IEnumerator postMyOp()
        {
            net_count = 0;
            timeInterval = 1.0f;
            string get_key="";
            while (true)
            {
                OpData op_data;
                op_data = new OpData();
                op_data.setOpData(1, opType.move, 1, new Vector2(1, 1), new Vector2(2, 2));
                /*op_data.print();*/
                string sendDataJson = UnityEngine.JsonUtility.ToJson(op_data);
               /* Debug.Log(sendDataJson);*/

                UnityWebRequest request = UnityWebRequest.Put(Constants.ServerAdress, sendDataJson);
                request.method = UnityWebRequest.kHttpVerbPOST;

                yield return request.SendWebRequest();

                if (request.responseCode == 200)
                  {
                      string text = request.downloadHandler.text;
                      get_key = text;
                     /* Debug.Log(get_key);*/
                      requesting = true;
                      break;
                  }
                  net_count++;
                  if (net_count > 3) {
                     Debug.Log("YOU OUT");
                     Application.Quit();
                  }
                Debug.Log(timeInterval);
                yield return new WaitForSeconds(timeInterval);
                TimeCalculate();
            }

            while (true)
            {
                OpData op_data;
                op_data = new OpData();
                op_data.req_mode = "get_msg";
                op_data.msg_id = get_key;
                op_data.setOpData(1, opType.move, 1, new Vector2(1, 1), new Vector2(2, 2));
                /*op_data.print();*/
                string sendDataJson = UnityEngine.JsonUtility.ToJson(op_data);
                /* Debug.Log(sendDataJson);*/

                UnityWebRequest request = UnityWebRequest.Put(Constants.ServerAdress, sendDataJson);
                request.method = UnityWebRequest.kHttpVerbPOST;

                yield return request.SendWebRequest();

                if (request.isNetworkError)
                {
                    Debug.Log(request.error);
                    yield return new WaitForSeconds(timeInterval);
                    TimeCalculate();
                    continue;
                    /*currentNetState = NetState.ERROR;*/
                }
                else
                {
                    if (request.responseCode == 200)
                    {
                        string text = request.downloadHandler.text;
                        OpData res;
                        res = UnityEngine.JsonUtility.FromJson<OpData>(text);
                        res.print();
                        requesting = true;
                        break;
                    }
                    else
                        Debug.Log(request.responseCode);
                }
                break;//
            }


        }
        IEnumerator postEnumyOp()
        {
            while (true)
            {
                requestEnemyOp req_enemy_op;
                req_enemy_op = new requestEnemyOp();

                string sendDataJson = UnityEngine.JsonUtility.ToJson(req_enemy_op);

                UnityWebRequest request = UnityWebRequest.Put(Constants.ServerAdress, sendDataJson);
                request.method = UnityWebRequest.kHttpVerbPOST;

                yield return request.SendWebRequest();

                if (request.isNetworkError)
                {
                    Debug.Log(request.error);
                    yield return new WaitForSeconds(timeInterval);
                    TimeCalculate();
                    continue;
                }
                else
                {
                    if (request.responseCode == 200)
                    {
                        string enemyOp = request.downloadHandler.text;
                        if (enemyOp == null)
                        {
                            Debug.Log("cnm csy");
                        }
                        else
                        {
                            Debug.Log("敌人开始动啦");
                            isMyTurn = true;
                        }
                    }
                }
            }
        }
        private void DisableScript()
        {
            for (int i = 0; i < turnControlScript.Length; i++) {
                turnControlScript[i].enabled = false;
            }
        }
        private void EnableScript() {
            for (int i = 0; i < turnControlScript.Length; i++)
            {
                turnControlScript[i].enabled = true;
            }
        }

        private void TimeCalculate() {
            timeInterval *= 2.0f;
        }

    }
}
