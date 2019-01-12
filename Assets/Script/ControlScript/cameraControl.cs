using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game {
    public class cameraControl : MonoBehaviour {

        private bool flag;
        private Vector3 offset;
        // Use this for initialization
        void Start() {
            flag = false;
        }

        // Update is called once per frame
        void Update() {
            OnMouseDrag();
            if (Constants.set_camera_position) {
                /*Debug.Log('a');*/
                /*Debug.Log("offset: " + Constants.camera_offset);*/
                transform.Translate(Constants.CheckCameraPsoiton(Constants.camera_offset)-transform.position);
                Constants.set_camera_position = false;
            }
            Constants.camera_position = transform.position;
        }

        private void OnMouseDrag()
        {
            if (Input.GetMouseButton(0)) {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");

                transform.Translate(new Vector3(-x, -y, 0) * Time.deltaTime * 20);
             }
            if (Input.GetMouseButtonUp(0)) {
                float left_offset_x = transform.position.x - Constants.camera_x;
                float right_offset_x = transform.position.x - Constants.camera_max_x;

                float up_offset_y = transform.position.y - Constants.camera_max_y;
                float down_offset_y = transform.position.y - Constants.camera_y;

                if (left_offset_x < 0)
                    transform.Translate(new Vector3(-left_offset_x, 0.0f, 0.0f));
                else if (right_offset_x > 0)
                    transform.Translate(new Vector3 (-right_offset_x,0.0f,0.0f) );

                if (up_offset_y > 0)
                    transform.Translate(new Vector3(0.0f,-up_offset_y, 0.0f));
                else if (down_offset_y < 0)
                    transform.Translate(new Vector3(0.0f,-down_offset_y,0.0f));
            }
        }

        public void SetCamPos(Vector3 pos) {
            flag = true;
            offset = pos - transform.position;
            /*Update();*/
        }
    }

}
