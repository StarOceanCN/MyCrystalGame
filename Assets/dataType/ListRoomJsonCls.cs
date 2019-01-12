using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{
    [Serializable]
    public class Serialization<T>
    {
        [SerializeField]
        List<T> target;
        public List<T> ToList() { return target; }

        public Serialization(List<T> target)
        {
            this.target = target;
        }
    }

    [Serializable]
    public class ListRoomJsonCls {
        public string req_mode;

        public ListRoomJsonCls()
        {
            req_mode = "list_room";
        }
    }

    [Serializable]
    public class RoomDataCls {
        public string user_id;
        public string user2_id;
        public int user_cnt;
        public string status;

        public void print() {
            Debug.Log(user_id);
            Debug.Log(user2_id);
            Debug.Log(user_cnt);
            Debug.Log(status);
        }
    }
}
