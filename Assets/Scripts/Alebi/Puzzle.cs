using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TileContent
{
    Empty, 
    Box, 
    Player
}


public class Puzzle : MonoBehaviour
{
    public GameObject[][] Board;
    public TileContent[][] BoardContent; 

    public int Width;
    public int Height; 

    public Vector2 PlayerPosition;

    public Vector2[] BoxPositions;

    public GameObject Player;

    public GameObject BoxPrefab;
    public GameObject TilePrefab;

    public Vector2 CellSize;

    public int[][] BoxBoard; 

    // Use this for initialization
    void Start ()
    {
        float ix = (-CellSize.x / 2) * Width;
        float iy = (-CellSize.y / 2) * Height;

        Board = new GameObject[Height][];
        BoardContent = new TileContent[Height][];
        for (int i = 0; i < Height; i++)
        {
            Board[i] = new GameObject[Width];
            BoardContent[i] = new TileContent[Width]; 
            for (int j = 0; j < Width; j++)
            {
                BoardContent[i][j] = TileContent.Empty; 
                Board[i][j] = Instantiate(TilePrefab, this.transform);
                Board[i][j].name =$"{i},{j}";
                Board[i][j].transform.localPosition = new Vector3(ix + CellSize.x * j, iy + CellSize.y * i, 0);
            }
        }

        Player.transform.SetParent(Board[(int)PlayerPosition.y][(int)PlayerPosition.x].transform);
        Player.transform.localPosition = Vector3.zero;

        foreach (var boxPos in BoxPositions)
        {
            GameObject box = Instantiate(BoxPrefab, Board[(int)boxPos.y][(int)boxPos.x].transform);
            BoardContent[(int)boxPos.y][(int)boxPos.x] = TileContent.Box;
            box.name = $"box {boxPos.x},{boxPos.y}";
            box.transform.localPosition = Vector3.zero; 
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
