using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{
    [Serializable]
    public class StartGameData
    {
        public string req_mode;
        public string user_id;
        public int seed;
        public StartGameData(string uid)
        {
            req_mode = "start_game";
            user_id = uid;
            seed = unchecked((int)DateTime.Now.Ticks);
        }
    }
}
