using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Game
{
    public class buttonBase : MonoBehaviour
    {

        protected GameObject gameMap;//鼠标焦点图片
        protected GameObject gameControl;//游戏控制器
        protected GameObject gameUI;//游戏roomlist ui层
        protected GameObject gameBackGround;//障碍、豆子存在的titlemap
        protected GameObject gameReady;//游戏准备ui层
        /*protected GameObject PlayerId;//左上角玩家id*/
        protected GameObject baseMap;//真正的地图
        private Color colorForSave;

        protected Button btn;

        private EventTrigger trigger;

        protected virtual void Awake() {
        }
        // Use this for initialization
        protected virtual void  Start()
        {
            btn = gameObject.GetComponent<Button>();
            trigger = btn.gameObject.AddComponent<EventTrigger>();

            AddTriggerEntry(EventTriggerType.PointerClick, OnMouseChick);
            AddTriggerEntry(EventTriggerType.PointerEnter, OnMouseEnter);
            AddTriggerEntry(EventTriggerType.PointerExit, OnMouseExit);
        }

        protected virtual void AddTriggerEntry(EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> callback)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener(callback);
            trigger.triggers.Add(entry);
        }

        protected virtual void OnMouseChick(BaseEventData baseData)
        {
            SetCanVisible();
        }

        protected virtual void OnMouseEnter(BaseEventData baseData)
        {
            colorForSave = gameObject.GetComponent<Image>().color;
            gameObject.GetComponent<Image>().color = new Color(212.0f, 245.0f, 20.0f,125.0f);
        }

        protected virtual void OnMouseExit(BaseEventData baseData)
        {
            gameObject.GetComponent<Image>().color = colorForSave;
        }

        protected virtual void SetCanVisible() {

        }
    }
}
