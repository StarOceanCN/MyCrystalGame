using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class playerData : MonoBehaviour
    {
        public static playerData Instance;
        public string user_id;
        public bool isHost;
        public int playerFraction;

        private void Awake()
        {
            Instance = this;
            user_id=CreateUserId();
            isHost = false;
            playerFraction = 0;
        }

        private string CreateUserId()
        {
            string baseStr = "qwertyuiopasdfghjklzxcvbnm";
            string resultStr = "";

            System.Random rad = new System.Random();
            for (int i = 0; i < 10; i++)
            {
                int temp = rad.Next(0, 25);
                resultStr += baseStr[temp];
            }
            return resultStr;
        }
    }
}
