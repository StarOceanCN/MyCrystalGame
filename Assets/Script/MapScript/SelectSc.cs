using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    public class SelectSc : MonoBehaviour
    {
        Vector3Int lastSelectPosition;
        Tilemap currentTilemap;
        float cameraHeight;
        // Use this for initialization
        void Start()
        {

            currentTilemap = this.GetComponent<Tilemap>();
            lastSelectPosition = new Vector3Int(-1, -1, 0);
            cameraHeight = Constants.camera_z;

            this.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            OnMouseEnter();
        }

        private void OnMouseEnter()
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,-cameraHeight));
            Vector3Int currentTilePosition = currentTilemap.WorldToCell(currentPosition);
            Tile currentTile = currentTilemap.GetTile<Tile>(currentTilePosition);
            currentTilePosition.z = 0;
            float offset_x = currentTilemap.CellToWorld(currentTilePosition).x - currentTilemap.CellToWorld(lastSelectPosition).x;
            float offset_y = currentTilemap.CellToWorld(currentTilePosition).y - currentTilemap.CellToWorld(lastSelectPosition).y;

            currentTilemap.transform.Translate(new Vector3(offset_x, offset_y, 0.0f));
        }

    }
}
