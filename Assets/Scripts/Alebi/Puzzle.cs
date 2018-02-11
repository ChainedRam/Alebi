using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    North = 1,
    South = 0,
    East = 3,
    West = 2,
}

public enum TileContent
{
	Empty, 
	Box, 
    Goal,
	Player
}

/// <summary>
/// Refactor this cancer 
/// </summary>
public class Puzzle : MonoBehaviour
{
	public GameObject[][] Board;
	public TileContent[][] BoardContent; 

	public int Width;
	public int Height; 

	public Vector2 PlayerPosition;
    public Vector2 GoalPosition;

    public Vector2[] BoxPositions;

	public GameObject Player;
    public GameObject Goal;

    public GameObject BoxPrefab;
	public GameObject TilePrefab;

	public Vector2 CellSize;

	public int[][] BoxBoard;


    public GameObject EndWindow; 

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
        BoardContent[(int)PlayerPosition.y][(int)PlayerPosition.x] = TileContent.Player; 

        Goal.transform.SetParent(Board[(int)GoalPosition.y][(int)GoalPosition.x].transform);
        Goal.transform.localPosition = Vector3.zero;
        BoardContent[(int)GoalPosition.y][(int)GoalPosition.x] = TileContent.Goal;

        foreach (var boxPos in BoxPositions)
		{
			GameObject box = Instantiate(BoxPrefab, Board[(int)boxPos.y][(int)boxPos.x].transform);
			BoardContent[(int)boxPos.y][(int)boxPos.x] = TileContent.Box;
			box.name = $"box {boxPos.x},{boxPos.y}";
			box.transform.localPosition = Vector3.zero; 
		}
	}


    float timeWaited; 

	// Update is called once per frame
	void FixedUpdate ()
	{
        if(EndWindow.activeSelf) //means puzzle ended
        {
            return; 
        }

        timeWaited += Time.fixedDeltaTime; 

        if(timeWaited > 1f)
        {
            if(Input.GetKey(KeyCode.RightArrow) && CanMovePlayerTo(Direction.East))
            {
                MovePlayerTo(Direction.East);
                timeWaited = 0; 
            }
            else if (Input.GetKey(KeyCode.UpArrow) && CanMovePlayerTo(Direction.North))
            {
                MovePlayerTo(Direction.North);
                timeWaited = 0; 
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && CanMovePlayerTo(Direction.West))
            { 
                MovePlayerTo(Direction.West);
                timeWaited = 0; 
            }
            else if (Input.GetKey(KeyCode.DownArrow) && CanMovePlayerTo(Direction.South))
            {
                MovePlayerTo(Direction.South);
                timeWaited = 0;
            }


        }

    }


    public bool CanMovePlayerTo(Direction d)
    {
        int tx = (int)PlayerPosition.x + ToX(d);
        int ty = (int)PlayerPosition.y + ToY(d);
        //check bounds 
        if(tx < 0 || tx >= Width || ty < 0 || ty > Height)
        {
            return false;
        }

        switch (BoardContent[ty][tx])
        {
            case TileContent.Empty:
                return true;

            case TileContent.Box:
                int bx = tx + ToX(d);
                int by = ty + ToY(d);

                return (bx >= 0 && bx < Width && by >= 0 && by < Height) && BoardContent[by][bx] == TileContent.Empty;

            case TileContent.Goal:
                return true; 

            default:
                return false; 
        }
    }

    public void MovePlayerTo(Direction d)
    {
        if (CanMovePlayerTo(d) == false)
        {
            throw new System.Exception("Wtf dude");
        }

        int tx = (int)PlayerPosition.x + ToX(d);
        int ty = (int)PlayerPosition.y + ToY(d);

        int px = (int)PlayerPosition.x;
        int py = (int)PlayerPosition.y;

        TileContent prevTilecontent = BoardContent[ty][tx];

        switch (prevTilecontent)
        {
            case TileContent.Empty:
                BoardContent[py][px] = TileContent.Empty;
                break;

            case TileContent.Box:
                int bx = tx + ToX(d);
                int by = ty + ToY(d);

                GameObject box = Board[ty][tx].transform.GetChild(0).gameObject;
                box.transform.SetParent(Board[by][bx].transform);
                BoardContent[by][bx] = TileContent.Box; 
                StartCoroutine(CenterObject(box, 1)); 
                BoardContent[py][px] = TileContent.Empty;
                break;

            case TileContent.Goal:
                GoalReached();
                break; 

            default:
                throw new System.Exception("srsly dude?");
                //break; 
        }

        BoardContent[ty][tx] = TileContent.Player;

        PlayerPosition = new Vector2(tx,ty);
        Player.transform.SetParent(Board[ty][tx].transform); 

        StartCoroutine(CenterObject(Player, 1f)); 
    }

    private void GoalReached()
    {
        EndWindow.SetActive(true); //egh
    }

    IEnumerator CenterObject(GameObject go, float duration)
    {
        int frames = (int) Mathf.Ceil((duration / Time.fixedDeltaTime));

        float speedPerFrame = Vector3.Distance(Vector3.zero, go.transform.localPosition) / frames; 

        for (int i = 0; i < frames; i++)
        {
            go.transform.localPosition = Vector3.MoveTowards(go.transform.localPosition,Vector3.zero,speedPerFrame);

            yield return new WaitForFixedUpdate(); 
        }
    }

    int ToX(Direction d)
    {
        int v = (int)d; 
        return (v/2) * ((v%2)*2 -1); 
    }

    int ToY(Direction d)
    {
        int v = (int)d;
        return (1 - v / 2) * (v * 2 - 1);
    }
}
