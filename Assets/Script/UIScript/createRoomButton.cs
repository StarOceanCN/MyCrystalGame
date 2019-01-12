using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

namespace Game
{
    public class createRoomButton : buttonBase
    {
        private bool isNetSuccee=false;

        protected override void Awake()
        {
            /*Debug.Log("add room awake!");*/
            GameObject root = GameObject.Find("UI");
            /*Debug.Log(root);*/
            gameUI = root.transform.Find("RoomUI").gameObject;
            gameReady = root.transform.Find("ReadyUI").gameObject;
            /*PlayerId = root.transform.Find("PlayerId").gameObject;*/

           /* Debug.Log(gameReady);*/
            isNetSuccee = false;
        }

        private void Update()
        {
            if (isNetSuccee)
            {
                gameReady.GetComponent<GameReadyUIScript>().SetHost(playerData.Instance.user_id);
                playerData.Instance.isHost = true;
                isNetSuccee = false;
                SetCanVisible();
                
            }
        }
        protected override void OnMouseChick(BaseEventData baseData) {
            StartCoroutine(createRoom());
        }
        protected override void SetCanVisible()
        {
            gameUI.SetActive(false);
            gameReady.SetActive(true);
            Constants.PlayerId.SetActive(true);
            Constants.SetGameInfo(true);
        }

        private IEnumerator createRoom() {

            int net_count = 0;
            float timeInterval = 1.0f;
            isNetSuccee = false;
            while (true)
            {
                createRoomJsonCls op_data;
                op_data = new createRoomJsonCls(playerData.Instance.user_id);
                string sendDataJson = UnityEngine.JsonUtility.ToJson(op_data);

                UnityWebRequest request = UnityWebRequest.Put(Constants.ServerAdress, sendDataJson);
                request.method = UnityWebRequest.kHttpVerbPOST;

                yield return request.SendWebRequest();

                if (request.responseCode == 200)
                {
                    string text = request.downloadHandler.text;
                    Constants.Room_id = playerData.Instance.user_id;
                    isNetSuccee = true;
                    break;
                }
                net_count++;
                if (net_count > 3)
                {
                    Debug.Log("YOU OUT");
                    Application.Quit();
                }
                Debug.Log(timeInterval);
                yield return new WaitForSeconds(timeInterval);
                timeInterval *= 2;
            }

        }

    }
}
