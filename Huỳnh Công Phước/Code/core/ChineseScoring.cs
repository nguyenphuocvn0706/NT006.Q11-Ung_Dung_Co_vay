using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Vay
{
    public class ChineseScoring
    {
        private readonly double komi;

        public ChineseScoring(double komi = 7.5)
        {
            this.komi = komi;
        }

        /// <summary>
        /// Tính điểm theo luật Trung Quốc
        /// </summary>
        public (double blackScore, double whiteScore) Calculate(Game_Engine game)
        {
            int size = game.Size;
            int[,] board = game.Board;

            bool[,] visited = new bool[size, size];

            int blackTerritory = 0;
            int whiteTerritory = 0;

            // --- 1. Tìm Territory ---
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (board[y, x] == 0 && !visited[y, x])
                    {
                        var region = FloodTerritory(board, visited, x, y, size);

                        if (region.owner == 1)
                            blackTerritory += region.count;
                        else if (region.owner == 2)
                            whiteTerritory += region.count;
                    }
                }
            }

            // --- 2. Đếm số quân trên bàn ---
            int blackStones = 0;
            int whiteStones = 0;

            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                {
                    if (board[y, x] == 1) blackStones++;
                    if (board[y, x] == 2) whiteStones++;
                }

            double blackScore = blackTerritory + blackStones;
            double whiteScore = whiteTerritory + whiteStones + komi;

            return (blackScore, whiteScore);
        }

        private (int owner, int count) FloodTerritory(
            int[,] board, bool[,] visited, int sx, int sy, int size)
        {
            Queue<(int, int)> q = new();
            q.Enqueue((sx, sy));
            visited[sy, sx] = true;

            int count = 0;

            bool touchesBlack = false;
            bool touchesWhite = false;

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            while (q.Count > 0)
            {
                var (x, y) = q.Dequeue();
                count++;

                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    if (nx < 0 || nx >= size || ny < 0 || ny >= size) continue;

                    int v = board[ny, nx];

                    if (v == 1) touchesBlack = true;
                    if (v == 2) touchesWhite = true;

                    if (v == 0 && !visited[ny, nx])
                    {
                        visited[ny, nx] = true;
                        q.Enqueue((nx, ny));
                    }
                }
            }

            if (touchesBlack && !touchesWhite) return (1, count);
            if (touchesWhite && !touchesBlack) return (2, count);

            return (0, count);
        }
    }
}
