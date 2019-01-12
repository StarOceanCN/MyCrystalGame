using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Game {
    public class MapSelectScript : MonoBehaviour {

        Tilemap currentTilemap;
        Tile crystal;
        
        Tile red_tile;
        Tile blue_tile;
        private float cameraHeight;
        private void Awake()
        {
            red_tile = Resources.Load("Prefab/RedTile")as Tile;
            blue_tile = Resources.Load("Prefab/BlueTile") as Tile;
            crystal = Resources.Load("Prefab/crystal") as Tile;

            gameObject.SetActive(false);
        }
        // Use this for initialization
        void Start()
        {
            currentTilemap = this.GetComponent<Tilemap>();
            cameraHeight = Constants.camera_z;
            GenerateBeans();
        }

        // Update is called once per frame
        void Update()
        {
            OnMouseDown();
            OnMouseEnter();
        }

        private void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            { 
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cameraHeight));
                Vector3Int currentTilePosition = currentTilemap.WorldToCell(currentPosition);

                Debug.Log(currentTilePosition);
            }
        }
        private void OnMouseEnter()
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cameraHeight));
            Vector3Int currentTilePosition = currentTilemap.WorldToCell(currentPosition);
            Tile currentTile = currentTilemap.GetTile<Tile>(currentTilePosition);

            if (currentTile != null&& currentTile.name == "crystal")
            {
                int f = Constants.GetPositionFraction(currentTilePosition.x, currentTilePosition.y);
                Constants.SetPlayerId("水晶的价值是" + f);
                return;
            }
            if (Constants.IsYourTurn) {
                Constants.SetPlayerId("还剩" + Constants.youTurnNum.ToString() + "回合，还能走" + (Constants.WalkFei1d - Constants.CurrentWalkCount).ToString() +"步");
            }
            else
                Constants.SetPlayerId("还剩" + Constants.youTurnNum.ToString() + "回合");
        }
        public bool CheckCanMove(Vector3 pos) {//检查能否移动
            Tile tile = currentTilemap.GetTile<Tile>(currentTilemap.WorldToCell(pos));
            if (pos.x > Constants.map_x_max ||
                pos.x < Constants.map_x_min ||
                pos.y > Constants.map_y_max ||
                pos.y < Constants.map_y_min)
            {
                return false;
            }
            if (tile == null)
            {
                return true;
            }
            else if (tile.name == "crystal") {//因为如果返回为true模型会直接行走到bean上，所以在这直接做分数++运算
                playerData.Instance.playerFraction += Constants.GetPositionFraction(currentTilemap.WorldToCell(pos).x, currentTilemap.WorldToCell(pos).y);
                Constants.RefreshPlayerFraction();
                ClearTile(pos);
                return true;
            }
            else if (tile.color != Constants.player_color)
            {
                return false;
            }
            return true;
        }
        public void SetTileColor(Vector3 pos,Color clr) {//设置地图上颜色
            Vector3Int currentTilePosition = currentTilemap.WorldToCell(pos);
            currentTilemap.SetTile(currentTilePosition, clr==red_tile.color?red_tile:blue_tile);
        }

        public Color GetRedTileColor() {
            return red_tile.color;
        }

        public Color GetBlueTileColor() {
            return blue_tile.color;
        }
        private void GenerateBeans()
        {
//             int[,] baseArr = new int[50,50];
//             for (int i = 0; i < 50; i++)
//                 for (int j = 0; j < 50; j++)
//                     baseArr[i, j] = 0;
            System.Random rd = new System.Random(Constants.RandSeed);
            for (int i = 0;  i < Constants.BeansNum; i++) {
                while (true) {
                    int x = rd.Next(Constants.BeanGenFeild.x_left, Constants.BeanGenFeild.x_right);
                    int y = rd.Next(Constants.BeanGenFeild.y_down, Constants.BeanGenFeild.y_up);
                    int f = rd.Next(1,10);

//                     if (baseArr[x, y] != 0)
//                         continue;
                    if (BeanIns(x,y,f)) {
                        /*baseArr[x, y]++;*/
                        break;
                    }  
                }
            }
        }
        private bool BeanIns(int x,int y,int f) {
            /*Tile tile = currentTilemap.GetTile<Tile>(new Vector3Int(x, y, 0));*/
            if (!currentTilemap.HasTile(new Vector3Int(x,y,0))) {
                currentTilemap.SetTile(new Vector3Int(x,y,0),crystal);

                Constants.fractionMap[x, y] = f;
                /*Debug.Log(crystal.name);*/
                return true;
            }
            return false;
        }

        private void ClearTile(Vector3 pos) {
            Vector3Int posi = currentTilemap.WorldToCell(pos);
            if (currentTilemap.HasTile(posi))
                currentTilemap.SetTile(posi,null);
        }

        public void CheckEnemyFraction(Vector3 pos) {
            Tile tile = currentTilemap.GetTile<Tile>(currentTilemap.WorldToCell(pos));
            if (tile == null)
                return;
            if (tile.name == "crystal")
            {
                Constants.enemyFraction += Constants.GetPositionFraction(currentTilemap.WorldToCell(pos).x, currentTilemap.WorldToCell(pos).y);
                Constants.RefreshEnemyFraction();
                ClearTile(pos);
            }
        }
    }
}
