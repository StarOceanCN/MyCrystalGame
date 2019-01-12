using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class createRoomJsonCls {
    public string user_id;
    public string req_mode;

    public createRoomJsonCls(string uid) {
        req_mode = "create_room";
        user_id = uid;
    }
}
