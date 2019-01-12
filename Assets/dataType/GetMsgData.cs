using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{
    [Serializable]
    public class GetMsgData
    {
        public string req_mode;
        public string room;
        public string user_id;

        public GetMsgData(string rid, string uid) {
            req_mode = "get_msg";
            room = rid;
            user_id = uid;
        }
    }
    [Serializable]
    public class StringFromJson{
        public string msg;
    }
}
