using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Vay
{
    public class JapaneseScoring
    {
        private readonly double komi;

        public JapaneseScoring(double komi = 6.5)
        {
            this.komi = komi;
        }

        /// <summary>
        /// Tính điểm theo luật Nhật Bản
        /// </summary>
        public (double blackScore, double whiteScore) Calculate(
            Game_Engine game,
            int blackPrisoners,   // số quân trắng bị đen bắt
            int whitePrisoners)   // số quân đen bị trắng bắt
        {
            int size = game.Size;
            int[,] board = game.Board;

            bool[,] visited = new bool[size, size];

            int blackTerritory = 0;
            int whiteTerritory = 0;

            // --- 1. Đếm lãnh thổ ---
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

            // --- 2. Điểm theo công thức ---
            double blackScore = blackTerritory + blackPrisoners;
            double whiteScore = whiteTerritory + whitePrisoners + komi;

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

            return (0, count); // trung lập
        }
        public (double blackScore, double whiteScore) Calculate(Game_Engine game)
        {
            // mặc định không có tù nhân
            return Calculate(game, 0, 0);
        }
    }
}
