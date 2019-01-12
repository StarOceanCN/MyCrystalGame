using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Game
{
    public class roomListScript : MonoBehaviour
    {

        RectTransform listContent;
        private void Awake()
        {
            //request data
            listContent = this.GetComponent<ScrollRect>().content;
        }
        // Use this for initialization
        void Start()
        {
            listContent.transform.localPosition += new Vector3(6.0f,0.0f,0.0f);
            StartCoroutine(LoadContent());
        }

        IEnumerator LoadContent()
        {
            while (true)
            {
                ListRoomJsonCls op_data;
                op_data = new ListRoomJsonCls();
                string sendDataJson = UnityEngine.JsonUtility.ToJson(op_data);

                UnityWebRequest request = UnityWebRequest.Put(Constants.ServerAdress, sendDataJson);
                request.method = UnityWebRequest.kHttpVerbPOST;

                yield return request.SendWebRequest();

                if (request.responseCode == 200)
                {
                    ContentClear();
                    string text = request.downloadHandler.text;
                    List<RoomDataCls> roomList = UnityEngine.JsonUtility.FromJson<Serialization<RoomDataCls>>(text).ToList();
                    foreach (var tmp in roomList) {
                        GameObject prefab = (GameObject)Resources.Load("Prefab/listContent") as GameObject;
                        GameObject temp =Instantiate(prefab, listContent);
                        var script = prefab.GetComponent<listContentScript>();
                        script.SetText(tmp.user_id);
                        script.SetRoomId(tmp.user_id);
                        script.SetCnt(tmp.user_cnt);
                    }
                    /*Debug.Log("roomlist has been refresh!");*/
                }
                yield return new WaitForSeconds(2.0f);
            }
        }
        private void ContentClear()
        {
            foreach (Transform child in listContent) {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
