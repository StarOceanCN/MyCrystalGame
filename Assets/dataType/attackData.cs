using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class attackData
    {
        private opType type;
        private Vector2 attackPostion;

        public attackData(Vector2 attackPosition)
        {
            this.type = opType.attack;
            this.attackPostion = new Vector2(attackPostion.x, attackPostion.y);
        }
    }
}
