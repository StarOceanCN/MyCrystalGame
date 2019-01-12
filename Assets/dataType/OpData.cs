using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class OpData
    {

        public string req_mode;
        public string msg_id;
        public int room_id;
        public int type;
        public int target;
        public Vector2 from_pos;
        public Vector2 to_pos;

        public OpData()
        {
            req_mode = "send_msg";
            msg_id = "";
        }

        public void setOpData(int r_id, opType type, int target, Vector2 from, Vector2 to)
        {
            room_id = r_id;
            this.type = (int)type;
            this.target = target;
            this.from_pos = from;
            this.to_pos = to;
        }

        public void print() {
            Debug.Log(req_mode);
            Debug.Log(room_id);
            Debug.Log(type);
            Debug.Log(target);
            Debug.Log(from_pos);
            Debug.Log(to_pos);
        }
    }
}
