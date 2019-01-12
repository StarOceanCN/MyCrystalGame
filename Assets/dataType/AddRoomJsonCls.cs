using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{
    [Serializable]
    public class AddRoomJsonCls
    {
        public string user_id;
        public string user2_id;
        public string req_mode;

        public AddRoomJsonCls(string uid, string u2id)
        {
            req_mode = "join_room";
            user_id = uid;
            user2_id = u2id;
        }
    }
}
