using ChainedRam.Core.Dialog;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChainedRam.Alebi.Puzzle
{
    public enum TileContent
    {
        Empty,
        Box,
        Goal,
        Player,
        Wall
    }

    /// <summary>
    /// Refactor this cancer 
    /// </summary>
    public class Puzzle : MonoBehaviour
    {
        private enum Direction
        {
            Up = 1,
            Down = 0,
            Right = 3,
            Left = 2,
        }

        public float speed;

        public GameObject[][] Board;
        public TileContent[][] BoardContent;

        public int Width;
        public int Height;

        private Vector2 PlayerPosition_defult;
        public Vector2 PlayerPosition;
        public Vector2 GoalPosition;

              
        public Vector2[] BoxPositions;
        public Vector2[] WallPositions;

        public GameObject Player;
        public GameObject Goal;

        public GameObject BoxPrefab;
        public GameObject TilePrefab;
        public GameObject WallPrefab;

        public Vector2 CellSize;


        List<GameObject> BoxList;
        public int[][] BoxBoard;

        public GameObject EndWindow;

        public Puzzle Next;

        private bool IsPaused;

        public Dialog StartDialog;
        public Dialog EndDialog;

        public DialogBox DialogBox;

        public AudioClip movesfx;
        public AudioClip boldersfx;
        public AudioClip resetsfx;
        public AudioClip goalsfx;

        public AudioSource source;

        // Use this for initialization
        private void Start()
        {
            Generate();
        }

        public void Generate()
        {
            PlayerPosition_defult = PlayerPosition;
            float ix = (-CellSize.x / 2) * Width;
            float iy = (-CellSize.y / 2) * Height;
            BoxList = new List<GameObject>();
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
                    Board[i][j].name = $"{i},{j}";
                    Board[i][j].transform.localPosition = new Vector3(ix + CellSize.x * j, iy + CellSize.y * i, 0);
                }
            }
            //the starting point
            Player.transform.SetParent(Board[(int)PlayerPosition.y][(int)PlayerPosition.x].transform);
            Player.transform.localPosition = Vector3.zero;
            BoardContent[(int)PlayerPosition.y][(int)PlayerPosition.x] = TileContent.Player;

            Goal.transform.SetParent(Board[(int)GoalPosition.y][(int)GoalPosition.x].transform);
            Goal.transform.localPosition = Vector3.zero;
            BoardContent[(int)GoalPosition.y][(int)GoalPosition.x] = TileContent.Goal;

            foreach (var boxPos in BoxPositions)
            {
                GameObject box = Instantiate(BoxPrefab, Board[(int)boxPos.y][(int)boxPos.x].transform);
                box.transform.localPosition = Vector3.zero;
                box.name = $"box {boxPos.x},{boxPos.y}";
                BoardContent[(int)boxPos.y][(int)boxPos.x] = TileContent.Box;
                BoxList.Add(box);
            }

            foreach (var WallPos in WallPositions)
            {
                GameObject Wall = Instantiate(WallPrefab, Board[(int)WallPos.y][(int)WallPos.x].transform);
                BoardContent[(int)WallPos.y][(int)WallPos.x] = TileContent.Wall;
                Wall.name = $"Wall {WallPos.x},{WallPos.y}";
                Wall.transform.localPosition = Vector3.zero;
            }

            if (StartDialog != null)
            {
                IsPaused = true;
                StartDialog.OnEnd += () => IsPaused = false;

                DialogBox.StartDialog(StartDialog);
            }
        }

        float TimeWaited;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (EndWindow.activeSelf || IsPaused) //means puzzle ended
            {
                return;
            }

            TimeWaited += Time.fixedDeltaTime;

            if (TimeWaited > speed)
            {
                if (Input.GetKey(KeyCode.RightArrow) && CanMovePlayerTo(Direction.Right))
                {
                    MovePlayerTo(Direction.Right);
                    TimeWaited = 0;
                }
                else if (Input.GetKey(KeyCode.UpArrow) && CanMovePlayerTo(Direction.Up))
                {
                    MovePlayerTo(Direction.Up);
                    TimeWaited = 0;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) && CanMovePlayerTo(Direction.Left))
                {
                    MovePlayerTo(Direction.Left);
                    TimeWaited = 0;
                }
                else if (Input.GetKey(KeyCode.DownArrow) && CanMovePlayerTo(Direction.Down))
                {
                    MovePlayerTo(Direction.Down);
                    TimeWaited = 0;
                }
            }
        }

        private bool CanMovePlayerTo(Direction d)
        {
            int tx = (int)PlayerPosition.x + ToX(d);
            int ty = (int)PlayerPosition.y + ToY(d);

            //check bounds 
            if (tx < 0 || tx >= Width || ty < 0 || ty >= Height)
            {
                return false;
            }

            switch (BoardContent[ty][tx])
            {
                case TileContent.Empty:                    
                    return true;
                case TileContent.Wall:
                    return false;

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

        private void MovePlayerTo(Direction d)
        {
            if (CanMovePlayerTo(d) == false)
            {
                throw new System.Exception("Wtf dude");
            }

            int tx = (int)PlayerPosition.x + ToX(d);
            int ty = (int)PlayerPosition.y + ToY(d);

            int px = (int)PlayerPosition.x;
            int py = (int)PlayerPosition.y;

            TileContent NextTilecontent = BoardContent[ty][tx];
            Action OnReached = null; 

            switch (NextTilecontent)
            {
                case TileContent.Empty:
                    BoardContent[py][px] = TileContent.Empty;
                    if (ty == PlayerPosition_defult.y && tx == PlayerPosition_defult.x)
                    {

                        ResetPuzzle();
                        source.PlayOneShot(resetsfx);
                    }
                    else
                    {
                        source.PlayOneShot(movesfx, 0.1f);
                    }
                    break;

                case TileContent.Box:
                    int bx = tx + ToX(d);
                    int by = ty + ToY(d);

                    GameObject box = Board[ty][tx].transform.GetChild(0).gameObject;
                    box.transform.SetParent(Board[by][bx].transform);
                    BoardContent[by][bx] = TileContent.Box;
                    StartCoroutine(CenterObject(box, speed));
                    BoardContent[py][px] = TileContent.Empty;
                    source.PlayOneShot(boldersfx, 0.25f);
                    break;


                case TileContent.Goal:
                    OnReached =  GoalReached;
                    source.PlayOneShot(goalsfx, 1.00f);
                    break; 

                default:
                    throw new System.Exception("srsly dude?");
                    //break; 
            }


            BoardContent[ty][tx] = TileContent.Player;

            PlayerPosition = new Vector2(tx, ty);
            Player.transform.SetParent(Board[ty][tx].transform);

            StartCoroutine(CenterObject(Player, speed, OnReached));
        }

        private void GoalReached()
        {
            if (EndDialog != null)
            {
                IsPaused = true;

                EndDialog.OnEnd += EndPuzzle;
                DialogBox.StartDialog(EndDialog);
            }
            else
            {
                EndPuzzle(); 
            }

        }

        public void EndPuzzle()
        {
            gameObject.SetActive(false);
            if (Next)
            {
                Next.enabled = true;
            }
            else
            {
                EndWindow.SetActive(true); //egh
            }
        }

        IEnumerator CenterObject(GameObject go, float duration, Action onReached = null)
        {
            int frames = (int)Mathf.Ceil((duration / Time.fixedDeltaTime));

            float speedPerFrame = Vector3.Distance(Vector3.zero, go.transform.localPosition) / frames;

            for (int i = 0; i < frames; i++)
            {
                go.transform.localPosition = Vector3.MoveTowards(go.transform.localPosition, Vector3.zero, speedPerFrame);

                yield return new WaitForFixedUpdate();
            }

            if(onReached != null)
            {
                onReached.Invoke(); 
            }
        }

        int ToX(Direction d)
        {
            int v = (int)d;
            return (v / 2) * ((v % 2) * 2 - 1);
        }

        int ToY(Direction d)
        {
            int v = (int)d;
            return (1 - v / 2) * (v * 2 - 1);
        }
        public void ResetPuzzle()
        {

            for (int i = 0; i < BoxList.Count; i++)
            {
                GameObject box = BoxList[i];
                string s=box.transform.parent.name;
                string [] ss = s.Split(',');
                int x = int.Parse(ss[0]);
                int y = int.Parse(ss[1]);
                Vector2 t = BoxPositions[i];
                BoardContent[y][x] = TileContent.Empty;
                box.transform.SetParent(Board[(int)t.y][(int)t.x].transform);
                BoardContent[(int)t.y][(int)t.x] = TileContent.Box;
                box.transform.localPosition = Vector3.zero;
            }
        }
    }

}