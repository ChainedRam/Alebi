using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public class PuzzleBuilder : MonoBehaviour
    {
        public int endTileX;
        public int endTileY;
        public int width = 4;
        public int height = 3;
        public Sprite sprite;
        public float size;
        // Use this for initialization
        void Start()
        {
            GameObject gameObject = new GameObject("Puzzle");
            Puzzle p = gameObject.AddComponent<Puzzle>();
            NeighbotGrid<Tile> gridBuilder = new NeighbotGrid<Tile>(width, height, (x, y) =>
            {
                GameObject tileGameObject = new GameObject();
                Tile tile;
                SpriteRenderer sr = tileGameObject.AddComponent<SpriteRenderer>();
                sr.sprite = sprite;
                if (endTileX == x && endTileY == y)
                {
                    tileGameObject.name = $"End Point: {x},{y}";
                    tile = tileGameObject.AddComponent<EndTile>();
                    sr.color = new Color(0.5f, 0, 0);
                }
                else
                {
                    tileGameObject.name = ($"Tile: {x},{y}");
                    tile = tileGameObject.AddComponent<Tile>();
                }
                tileGameObject.transform.SetParent(p.transform);
                tileGameObject.transform.position = new Vector3((x - ((float)width / 2) + size), (y - ((float)height / 2) + size), 0);
                return tile;
            });

            p.tile = gridBuilder.Grid;
            
            GameObject obj = new GameObject("Player");
            PuzzlePlayer player = obj.AddComponent<PuzzlePlayer>();
            p.tile[0][0].SetContent(player);
            SpriteRenderer playerSR = player.gameObject.AddComponent<SpriteRenderer>();
            playerSR.sprite = sprite;
            playerSR.color = Color.red;
            player.transform.position = new Vector2(0, 0);

            GameObject blockObj = new GameObject("block");
            BlockContent puzzleBlock = blockObj.AddComponent<BlockContent>();
            p.tile[1][1].SetContent(puzzleBlock);
            SpriteRenderer blockSR = puzzleBlock.gameObject.AddComponent<SpriteRenderer>();
            blockSR.sprite = sprite;
            blockSR.color = Color.black;
            puzzleBlock.transform.position = new Vector2(0, 0);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}