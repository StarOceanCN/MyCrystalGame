using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{
    [Serializable]
    public class SendMsgData {
        public string req_mode;
        public string room;
        public string user_id;
        public string msg;

        public SendMsgData(string rid,string uid,string msg) {
            req_mode = "send_msg";
            room = rid;
            user_id = uid;
            this.msg = msg;
        }
    }
}
