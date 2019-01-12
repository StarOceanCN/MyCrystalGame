using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game {
    public class playerControl : MonoBehaviour {


        private float speed = 5.0f;
        private int hori;
        private int vert;

        private MapSelectScript script;

        // Use this for initialization
        void Start()
        {
            /*Debug.Log("player control !");*/
            script = GameObject.Find("Grid").transform.Find("map").gameObject.GetComponent<MapSelectScript>();
            if (name == "PlayerSoldier")
                script.SetTileColor(transform.position,Constants.player_color);
        }

        // Update is called once per frame
        void Update() {
            if (!Constants.CheckFeild())
                onUserControl();
        }

        private void onUserControl() {
            hori = 0;
            vert = 0;
            if (Input.GetKeyDown(KeyCode.W) &&
                script.CheckCanMove(new Vector3(transform.position.x, transform.position.y + 1, 0)))
            {
                vert++;
                Constants.CurrentWalkCount++;
                Constants.AddOp('w');

            }
            if (Input.GetKeyDown(KeyCode.S) &&
                script.CheckCanMove(new Vector3(transform.position.x, transform.position.y - 1, 0)))
            {
                vert--;
                Constants.CurrentWalkCount++;
                Constants.AddOp('s');
            }
            if (Input.GetKeyDown(KeyCode.A) &&
                script.CheckCanMove(new Vector3(transform.position.x - 1, transform.position.y, 0)))
            {
                hori--;
                Constants.CurrentWalkCount++;
                Constants.AddOp('a');
            }
            if (Input.GetKeyDown(KeyCode.D) &&
                script.CheckCanMove(new Vector3(transform.position.x + 1, transform.position.y, 0)))
            {
                hori++;
                Constants.CurrentWalkCount++;
                Constants.AddOp('d');
            }

            //本地缩放因子*精灵大小
            //         float sp_size_x = this.transform.localScale.x * this.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
            //         float sp_size_y=this.transform.localScale.y * this.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
            if (hori != 0 || vert != 0)
            {
                transform.Translate(hori * 1, vert * 1, 0.0f);
                script.SetTileColor(transform.position,Constants.player_color);
            }
//             if (Constants.CheckFeild())
//                 Constants.SetOpMenuVisible(true);
        }
    }
}
