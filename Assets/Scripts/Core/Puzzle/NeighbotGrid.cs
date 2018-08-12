using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class NeighbotGrid <T> where T: NeighbotGrid<T>.INeighbor<T>
    {
        public interface INeighbor<K>
        {
            K[] Neighbor { set; get; }
        }
        public int Width;
        public int Height;
        public T[][] Grid;

        public NeighbotGrid(int width, int height, Func<int, int, T> create)
        {
            this.Width = width;
            this.Height = height;

            // step 1: creating the first big array
            Grid = new T[height][];
            // step 2: creating each element in big array
            for (int i = 0; i < Grid.Length; i++)
            {
                Grid[i] = new T[width];
            }

            // step 3: populating the big array with small arrays
            for (int i = 0; i < Grid.Length; i++)
            {
                for (int j = 0; j < Grid[i].Length; j++)
                {
                        Grid[i][j] = create(j, i);
                }
            }


            for (int i = 0; i < Grid.Length; i++)
            {
                for (int j = 0; j < Grid[i].Length; j++)
                {
                    if (IsInBound(i - 1, j, Grid.Length, Grid[i].Length))
                    {
                        Grid[i][j].Neighbor[0] = Grid[i - 1][j];
                        //i-1,j
                    }

                    if (IsInBound(i + 1, j, Grid.Length, Grid[i].Length))
                    {
                        Grid[i][j].Neighbor[1] = Grid[i + 1][j];
                        //i+1,j
                    }

                    if (IsInBound(i, j + 1, Grid.Length, Grid[i].Length))
                    {
                        Grid[i][j].Neighbor[2] = Grid[i][j + 1];
                        //i,j+1
                    }

                    if (IsInBound(i, j - 1, Grid.Length, Grid[i].Length))
                    {
                        Grid[i][j].Neighbor[3] = Grid[i][j - 1];
                        //i,j-1
                    }

                }
            }
        }

        public static bool IsInBound(int i, int j, int w, int h)
        {
            return i >= 0 && j >= 0 && i < w && j < h;
        }

        public T Get(int i, int j)
        {
            return Grid[i][j];
        }
    }

