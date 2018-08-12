using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public class PuzzleBuilder : MonoBehaviour
    {

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
                GameObject tile = new GameObject($"Tile: {x},{y}");
                Tile t = tile.AddComponent<Tile>();
                tile.transform.SetParent(p.transform);
                SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();
                sr.sprite = sprite;
                tile.transform.position = new Vector3((x - ((float)width / 2) + size), (y - ((float)height / 2) + size), 0);
                return t;
            });

            p.tile = gridBuilder.Grid;
            
            GameObject obj = new GameObject();
            PuzzlePlayer player = obj.AddComponent<PuzzlePlayer>();
            p.tile[0][0].SetContent(player);
            SpriteRenderer playerSR = player.gameObject.AddComponent<SpriteRenderer>();
            playerSR.sprite = sprite;
            playerSR.color = Color.red;
            player.transform.position = new Vector2(0, 0);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}