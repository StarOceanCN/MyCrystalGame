using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Game
{
    public class listContentScript : MonoBehaviour
    {
        [SerializeField]
        private string room_id;
        [SerializeField]
        private int cnt=0;

        private GameObject gameUI;
        private Transform addRoomBtn;

        private EventTrigger trigger;

        // Use this for initialization
        void Start()
        {
            gameUI = GameObject.Find("UI").transform.Find("RoomUI").gameObject; ;
            addRoomBtn = gameUI.transform.Find("addRoom");

            this.gameObject.AddComponent<EventTrigger>();
            trigger = this.gameObject.GetComponent<EventTrigger>();
            AddTriggerEntry(EventTriggerType.PointerClick, OnMouseChick);
        }

        public void SetText(string text)
        {
            Transform txt = transform.Find("name");
            txt.GetComponent<Text>().text = text;
        }

        public void SetRoomId(string room_id)
        {
            this.room_id = room_id;
        }

        public void SetCnt(int player_cnt) {
            this.cnt = player_cnt;
            Transform txt = transform.Find("player_count");
            txt.GetComponent<Text>().text = cnt.ToString();
        }
        private void AddTriggerEntry(EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> callback)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener(callback);
            trigger.triggers.Add(entry);
        }

        private void OnMouseChick(BaseEventData baseData) {
            this.gameObject.GetComponent<Image>().color = new Color(255.0f, 0.0f, 0.0f);
            if (cnt > 1)
                Debug.Log("ROOM IS FULL!");
            else
                addRoomBtn.gameObject.GetComponent<addRoomButton>().SetRoomId(room_id);
        }
    }
}

