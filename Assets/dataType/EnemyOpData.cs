using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{
    [Serializable]
    public class EnemyOpData
    {

        private RequestMode mode;
        private int room_id;
        private opType type;
        private int target;
        private Vector2 from_pos;
        private Vector2 to_pos;

        public EnemyOpData()
        {
            mode = RequestMode.REQUEST_POST_ENEMY_OP;
        }

        public void setOpData(int r_id, opType type, int target, Vector2 from, Vector2 to)
        {
            room_id = r_id;
            this.type = type;
            this.target = target;
            this.from_pos = from;
            this.to_pos = to;
        }
    }
}
