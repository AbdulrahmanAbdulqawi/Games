// <copyright file="ShortestPath.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// ShortestPath calculated with BFS Algorithm.
    /// </summary>
    public static class ShortestPath
    {
        private static bool[,] mat;
        private static int[] rowNum = { -1, 0, 0, 1 };
        private static int[] colNum = { 0, -1, 1, 0 };

        /// <summary>
        /// BFS algorithm for searching shortest path.
        /// </summary>
        /// <param name="m">Matrix of level walls.</param>
        /// <param name="src">Starting coordinates.</param>
        /// <param name="dest">Destination coordinates.</param>
        /// <returns>Coordinates to the seek loc.</returns>
        public static Point BFS(bool[,] m, Point src, Point dest)
        {
            bool[,] visited;
            if (m != null)
            {
                mat = m;

                if (mat[Convert.ToInt32(src.X), Convert.ToInt32(src.Y)] ||
                    mat[Convert.ToInt32(dest.X), Convert.ToInt32(dest.Y)])
                {
                    return new Point(-1, -1);
                }

                visited = new bool[mat.GetLength(0), mat.GetLength(1)];

                visited[Convert.ToInt32(src.X), Convert.ToInt32(src.Y)] = true;

                Queue<QueueNode> q = new Queue<QueueNode>();
                List<Point> path = new List<Point>();
                path.Add(src);

                QueueNode s = new QueueNode(path, 0);
                q.Enqueue(s); // Enqueue source cell

                while (q.Count != 0)
                {
                    QueueNode curr = q.Peek();
                    Point pt = curr.Pt[curr.Pt.Count - 1];

                    if (Convert.ToInt32(pt.X) == Convert.ToInt32(dest.X) && Convert.ToInt32(pt.Y) == Convert.ToInt32(dest.Y))
                    {
                        if (curr.Pt.Count == 1)
                        {
                            return curr.Pt[0];
                        }
                        else
                        {
                            return curr.Pt[1];
                        }
                    }

                    q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {
                        int row = Convert.ToInt32(pt.X) + rowNum[i];
                        int col = Convert.ToInt32(pt.Y) + colNum[i];

                        if (IsValid(row, col) && !mat[row, col] && !visited[row, col])
                        {
                            List<Point> newPath = new List<Point>(curr.Pt);
                            newPath.Add(new Point(row, col));

                            visited[row, col] = true;
                            QueueNode adjCell = new QueueNode(newPath, curr.Dist + 1);
                            q.Enqueue(adjCell);
                        }
                    }
                }
            }

            return new Point(-1, -1);
        }

        private static bool IsValid(int row, int col)
        {
            return (row >= 0) && (row < mat.GetLength(0)) &&
                   (col >= 0) && (col < mat.GetLength(1));
        }
    }
}