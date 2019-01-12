using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class moveData
    {
        private opType type;
        private Vector2 destination;
        public moveData(Vector2 destination)
        {
            this.type = opType.move;
            this.destination = new Vector2(destination.x, destination.y);
        }
    }
}  