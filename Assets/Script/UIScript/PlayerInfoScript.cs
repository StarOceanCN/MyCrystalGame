using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public class PlayerInfoScript : MonoBehaviour {

        Transform name_txt;
        private void Awake()
        {
            name_txt = transform.Find("name_txt");
        }
        // Use this for initialization
        void Start() {
            name_txt.GetComponent<Text>().text = playerData.Instance.user_id;
    
    }

        // Update is called once per frame
        void Update() {

        }
    }
}
