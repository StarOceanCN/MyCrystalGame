using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class EnemyAutoWalk : MonoBehaviour
    {
        private bool IsWalk;
        private char op;

        private MapSelectScript script;

        // Use this for initialization
        void Start()
        {
            IsWalk = false;
            script = GameObject.Find("Grid").transform.Find("map").gameObject.GetComponent<MapSelectScript>();
            script.SetTileColor(transform.position, Constants.enemy_color);
            /*Debug.Log(Constants.enemy_color);*/
        }

        // Update is called once per frame
        //考虑如果update卡顿会丢失操作
        void Update()
        {
            if (IsWalk)
            {
                if (op == 'w')
                    transform.Translate(0f, 1f, 0f);
                if (op == 'a')
                    transform.Translate(-1f, 0f, 0f);
                if (op == 's')
                    transform.Translate(0f, -1f, 0f);
                if (op == 'd')
                    transform.Translate(1f, 0f, 0f);
                IsWalk = false;
                script.CheckEnemyFraction(transform.position);
                script.SetTileColor(transform.position, Constants.enemy_color);
            }
        }

        public void EnemyWalkOneStep(char op)
        {
            IsWalk = true;
            this.op = op;
        }
    }
}
