

namespace Game
{
    using UnityEngine;
    using UnityEngine.UI;
    public enum opType
    {
        move = 0,
        attack = 1,
        defend = 2,
        skill = 3
    }

    public enum SkillID
    {

    }

    public enum RequestMode
    {
        REQUEST_POST_MY_OP = 1,
        REQUEST_POST_ENEMY_OP = 2
    }

    static class Constants {

        //摄像机最小坐标
        public const float camera_x = 10.3f;
        public const float camera_y = 5.8f;
        public const float camera_z = -10f;
        //摄像机最大坐标
        public const float camera_max_x = 39.7f;
        public const float camera_max_y = 44.2f;
        //地图边界
        public const float map_x_min = 0;
        public const float map_x_max = 50;
        public const float map_y_min = 0;
        public const float map_y_max = 50;

        public const int turnNum = 10;
        public const float WaitTime = 15.0f;

        public static bool set_camera_position;
        public static Vector3 camera_offset;
        public static Vector3 camera_position;

        public const int BeansNum = 50;
        public static int RandSeed { get; private set; }
        public struct BeanGenFeild {
            public const int x_left = 2;
            public const int x_right = 47;
            public const int y_up = 47;
            public const int y_down = 2;
        }
        public struct HostStartPosition {
            private const int x = 0;
            private const int y = 0;

            public static Vector3Int GetPos() {
                return new Vector3Int(x, y, 0);
            }
        }
        public struct GuestStartPosition {
            private const int x = 49;
            private const int y = 49;
            public static Vector3Int GetPos()
            {
                return new Vector3Int(x, y, 0);
            }

        }

        public static bool IsYourTurn;
        public static int youTurnNum;
        public static int enemyFraction;
        public static int[,] fractionMap;

        public static string Room_id;
        public static string Enemy_id;
        public static string WalkingSequence;

        public static Color player_color;
        public static Color enemy_color;

        public static int CurrentWalkCount;

        public static GameObject OpMenu;
        public static GameObject GameInfo;
        public static GameObject PlayerId;
        public static GameObject GameOverPanel;

        public const int WalkFei1d=20;

        public const string ServerAdress = "https://1005430667418011.cn-shanghai.fc.aliyuncs.com/2016-08-15/proxy/egg/trigger/";
        static Constants() {
            player_color = new Color();
            enemy_color = new Color();

            set_camera_position = false;
            camera_offset = new Vector3(0f,0f,0f);
            camera_position = new Vector3(0f, 0f, 0f);

            fractionMap = new int[50,50];
        }
        public static Vector3 CheckCameraPsoiton(Vector3 pos) {
            if (pos.x < camera_x)
                pos.x = camera_x;
            else if (pos.x > camera_max_x)
                pos.x = camera_max_x;
            if (pos.y < camera_y)
                pos.y = camera_y;
            else if (pos.y > camera_max_y)
                pos.y = camera_max_y;
            pos.z = camera_z;
            return pos;
        }

        public static void SetWalkCountZero() {
            CurrentWalkCount = 0;
        }

        public static void SetTurnNum() {
            youTurnNum = turnNum;
        }

        public static bool CheckFeild() {
            return CurrentWalkCount >= WalkFei1d;
        }

        public static void SetOpMenuVisible(bool vis) {
            OpMenu.SetActive(vis);
        }


        public static void ClearWalkingSquence() {
            WalkingSequence = "";
        }

        public static void AddOp(char op) {
            WalkingSequence += op;
        }

        public static void SetGameInfo(string info) {
            GameInfo.transform.Find("prompt").GetComponent<Text>().text = info;
        }

        public static void YouTurn() {
            SetOpMenuVisible(true);
            GameInfo.transform.Find("prompt").GetComponent<Text>().text = "你的回合";
        }

        public static void OtherOneTurn() {
            SetOpMenuVisible(false);
            GameInfo.transform.Find("prompt").GetComponent<Text>().text = "对方回合";
        }

        public static void RefreshPlayerFraction() {
            GameInfo.transform.Find("fraction").GetComponent<Text>().text = playerData.Instance.playerFraction.ToString();
        }

        public static void RefreshEnemyFraction() {
            GameInfo.transform.Find("enemyFraction").GetComponent<Text>().text = enemyFraction.ToString();
        }

        public static void SetPlayerId(string info) {
            PlayerId.transform.Find("Text").GetComponent<Text>().text = info;
        }

        public static void SetGameInfo(bool vis) {
            GameInfo.SetActive(vis);
        }

        public static void SetRandSeed(int sed) {
            RandSeed = sed;
        }

        public static int GetBeanFraction() {
            return 10;
        }

        public static int GetPositionFraction(int x,int y) {
            return fractionMap[x,y];
        }

        public static void Finish() {
            if (playerData.Instance.playerFraction > enemyFraction)
            {
                GameOverPanel.transform.Find("win").gameObject.SetActive(true);
            }
            else {
                GameOverPanel.transform.Find("loss").gameObject.SetActive(true);
            }
        }

        public static void HideWinOrLoss() {
            GameOverPanel.transform.Find("win").gameObject.SetActive(false);
            GameOverPanel.transform.Find("loss").gameObject.SetActive(false);
        }
    }
}