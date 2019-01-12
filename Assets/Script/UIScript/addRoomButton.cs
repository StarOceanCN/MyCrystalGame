using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

namespace Game
{
    public class addRoomButton : buttonBase
    {
        [SerializeField]
        public string room_id;
        private bool isNetSuccee;
        protected override void Awake()
        {
            /*Debug.Log("add room awake!");*/

            GameObject root = GameObject.Find("UI");
            /*Debug.Log(root);*/
            gameUI = root.transform.Find("RoomUI").gameObject;
            gameReady = root.transform.Find("ReadyUI").gameObject;
          /*  PlayerId = root.transform.Find("PlayerId").gameObject;*/

            /*Debug.Log(gameReady);*/
            room_id = null;
            isNetSuccee = false;
        }

        private void Update()
        {
            if (isNetSuccee) {

                gameReady.GetComponent<GameReadyUIScript>().SetHost(room_id);
                playerData.Instance.isHost = false;
                isNetSuccee = false;
                gameUI.SetActive(false);
                gameReady.SetActive(true);
                Constants.PlayerId.SetActive(true);
                Constants.SetGameInfo(true);
            }
        }

        protected override void OnMouseChick(BaseEventData baseData)
        {

            if (room_id == null)
            {
                Debug.Log("YOU DONT SELECT ROOM!");
            }
            else
            {

                StartCoroutine(AddRoom(room_id));

            }
        }
        public void SetRoomId(string id) {
            room_id = id;
        }

        private IEnumerator AddRoom(string rid) {

            int net_count = 0;
            float timeInterval = 1.0f;
            
            while (true)
            {
                AddRoomJsonCls op_data;
                op_data = new AddRoomJsonCls(rid,playerData.Instance.user_id);
                string sendDataJson = UnityEngine.JsonUtility.ToJson(op_data);

                UnityWebRequest request = UnityWebRequest.Put(Constants.ServerAdress, sendDataJson);
                request.method = UnityWebRequest.kHttpVerbPOST;

                yield return request.SendWebRequest();

                if (request.responseCode == 200)
                {
                    string text = request.downloadHandler.text;
                    Constants.Room_id = rid;
                    isNetSuccee = true;
                    break;
                }

                Debug.Log(request.responseCode);
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
