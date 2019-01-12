using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class OpMenuConfirmButton : buttonBase
    {
        protected override void Awake()
        {
            gameControl = GameObject.Find("Game").transform.Find("GameManager").gameObject;
        }
        protected override void OnMouseChick(BaseEventData baseData)
        {
            gameControl.GetComponent<GameStart>().TurnedChange(Constants.WalkingSequence);
        }
    }
}
