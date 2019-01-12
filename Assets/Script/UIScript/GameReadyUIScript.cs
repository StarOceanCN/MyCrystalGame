using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Game {
    public class GameReadyUIScript : MonoBehaviour {

        [SerializeField]
        public string isHost;
        private Transform infoPanel;
        private Transform cntPanel;
        private Transform hostName;
        private Transform guestName;

        private Button btn;
        void Awake()
        {
            infoPanel = transform.Find("Host");
            cntPanel = infoPanel.Find("player_count");
            hostName = infoPanel.Find("host_name");
            guestName = infoPanel.Find("guest_name");
            btn = infoPanel.Find("Button").GetComponent<Button>();

            /*Debug.Log("ready awake!");*/
            gameObject.SetActive(false);
        }
        // Use this for initialization
        void Start() {

            hostName.gameObject.GetComponent<Text>().text = isHost;

            if (isHost == playerData.Instance.user_id)
            {
                guestName.gameObject.GetComponent<Text>().text = "烫烫烫烫烫烫";
                StartCoroutine(GetGuest());
            }
            else {
                StartCoroutine(GetRoomData());
                btn.gameObject.SetActive(false);
                guestName.gameObject.GetComponent<Text>().text = playerData.Instance.user_id;
            }

    }
        public void SetCntPanel(int cnt) {
            cntPanel.Find("cnt").gameObject.GetComponent<Text>().text = cnt.ToString();
        }
        public void SetHost(string host_id) {
            isHost = host_id;
        }
        private IEnumerator GetGuest() {
            while (true) {
                ReadyRequestGuestInfo op_data;
                op_data = new ReadyRequestGuestInfo(playerData.Instance.user_id);
                string sendDataJson = UnityEngine.JsonUtility.ToJson(op_data);

                UnityWebRequest request = UnityWebRequest.Put(Constants.ServerAdress, sendDataJson);
                request.method = UnityWebRequest.kHttpVerbPOST;

                yield return request.SendWebRequest();

                if (request.responseCode == 200)
                {
                    string text = request.downloadHandler.text;
                    User2IdData data;
                    data = UnityEngine.JsonUtility.FromJson<User2IdData>(text);
                    if (data.user2_id != "")
                    {
                        Constants.Enemy_id = data.user2_id;
                        guestName.gameObject.GetComponent<Text>().text = data.user2_id;
                        break;
                    }
                }
               yield return new WaitForSeconds(2.0f);
                /*Debug.Log("some one come into my room?");*/
                Constants.SetGameInfo("还没人进来吗?");
            }
        }
        private IEnumerator GetRoomData() {

            while (true)
            {
                ReadyRequestGuestInfo op_data;
                op_data = new ReadyRequestGuestInfo(isHost);
                string sendDataJson = UnityEngine.JsonUtility.ToJson(op_data);

                UnityWebRequest request = UnityWebRequest.Put(Constants.ServerAdress, sendDataJson);
                request.method = UnityWebRequest.kHttpVerbPOST;

                yield return request.SendWebRequest();

                if (request.responseCode == 200)
                {
                    string text = request.downloadHandler.text;
                    User2IdData data;
                    data = UnityEngine.JsonUtility.FromJson<User2IdData>(text);
                    if (data.status == "Started")
                    {
                        Constants.SetRandSeed(data.seed);
                        btn.GetComponent<ReadyPanelStartGameScript>().GuestStartGame();
                        Constants.Enemy_id = data.user_id;
                        Constants.IsYourTurn = false;
                        break;
                    }
                }
                yield return new WaitForSeconds(1.0f);
                Constants.SetGameInfo("还没人进来吗?");
            }
        }
    }
}

