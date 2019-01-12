using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{
    [Serializable]
    public class ReadyRequestGuestInfo 
    {
        public string req_mode;
        public string user_id;

        public ReadyRequestGuestInfo(string uid) {
            req_mode = "get_room";
            user_id = uid;
        }
    }
}
