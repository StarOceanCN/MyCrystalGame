using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game {
    [Serializable]
    public class requestEnemyOp {
        private RequestMode mode;

        public requestEnemyOp() {
            mode = RequestMode.REQUEST_POST_ENEMY_OP;
        }

       
    }

}
