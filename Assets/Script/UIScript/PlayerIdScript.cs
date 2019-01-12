using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlayerIdScript : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
            transform.Find("Text").GetComponent<Text>().text = playerData.Instance.user_id;
        }
    }
}
