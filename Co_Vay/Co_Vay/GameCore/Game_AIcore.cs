using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic; 
using System.Diagnostics; 

namespace Co_Vay
{
    public class Game_AIcore
    {
        public struct MoveCandidate
        {
            public int X;
            public int Y;
            public int Score;
            public bool IsPass => X == -1 && Y == -1;
        }

        private readonly Game_Engine game;
        private readonly int machineColor;
        private readonly int localColor;
        private readonly int boardSize;
        private readonly Random rng;

        private static readonly (int dx, int dy)[] Neighbors4 = new (int dx, int dy)[]
        {
            (1,0), (-1,0), (0,1), (0,-1)
        };

        private const int BRANCHING_WIDTH = 20;
        private const int CANDIDATE_LIMIT = 30;

        private readonly int timeLimitMs = 1000; 
        private Stopwatch sw;

        public Game_AIcore(Game_Engine game, int machineColor, int localColor, int boardSize, Random rng = null)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            this.machineColor = machineColor;
            this.localColor = localColor;
            this.boardSize = boardSize;
            this.rng = rng ?? new Random();
        }

        /// <summary>
        /// Trả về nước đi tốt nhất (có thể là pass), sử dụng tìm kiếm Alpha-Beta độ sâu 3-4.
        /// Dùng cho AI chơi chính.
        /// </summary>
        public MoveCandidate GetBestMove(int maxDepth = 3)
        {
            sw = Stopwatch.StartNew();

            var candidates = GetSortedCandidateMoves(game);
            MoveCandidate bestMove = candidates[0];
            int bestValue = int.MinValue;

            foreach (var move in candidates)
            {
                if (sw.ElapsedMilliseconds > timeLimitMs) break;

                var nextState = CloneState(game);
                bool played;

                if (move.IsPass)
                {
                    nextState.Pass();
                    played = true;
                }
                else
                {
                    played = nextState.TryPlayMove(
                        move.X,
                        move.Y,
                        out _,
                        out _
                    );
                }

                if (!played) continue;

                int value = AlphaBeta(nextState, maxDepth, int.MinValue, int.MaxValue, false);
                if (value > bestValue)
                {
                    bestValue = value;
                    bestMove = move;
                }
            }

            return bestMove;
        }

        /// <summary>
        /// Trả về danh sách nước đi tiềm năng đã sắp xếp, sử dụng đánh giá sâu hơn (1-ply search).
        /// Tương thích với code cũ, nhưng thông minh hơn.
        /// </summary>
        public List<MoveCandidate> GetSortedMoves()
        {
            var shallowCandidates = GetSortedCandidateMoves(game);
            var deepList = new List<MoveCandidate>();

            foreach (var move in shallowCandidates)
            {
                var nextState = CloneState(game);
                bool played = false;
                if (move.IsPass)
                {
                    nextState.Pass();
                    played = true;
                }
                else
                {
                    played = nextState.TryPlayMove(move.X, move.Y, out _, out _);
                }

                if (!played) continue;

                // Đánh giá sâu hơn: AlphaBeta depth=2 từ vị trí sau nước đi (lượt đối thủ)
                int deepScore = AlphaBeta(nextState, 2, int.MinValue, int.MaxValue, false);

                deepList.Add(new MoveCandidate
                {
                    X = move.X,
                    Y = move.Y,
                    Score = deepScore
                });
            }

            // Sắp xếp và thêm noise nhỏ
            deepList.Sort((a, b) => b.Score.CompareTo(a.Score));
            for (int i = 0; i < deepList.Count; i++)
            {
                var mc = deepList[i];
                mc.Score += rng.Next(-3, 4);
                deepList[i] = mc;
            }
            deepList.Sort((a, b) => b.Score.CompareTo(a.Score));

            return deepList.Take(CANDIDATE_LIMIT).ToList();
        }

        private Game_Engine CloneState(Game_Engine original)
        {
            var clone = new Game_Engine(original.Size);

            // Copy board trực tiếp 
            for (int y = 0; y < boardSize; y++)
                for (int x = 0; x < boardSize; x++)
                    clone.Board[y, x] = original.Board[y, x];

            // Copy các state khác (lượt, pass, ...)
            clone.CopyFrom(original);
            return clone;
        }

        private int AlphaBeta(Game_Engine state, int depth, int alpha, int beta, bool isMax)
        {
            if (sw != null && sw.ElapsedMilliseconds > timeLimitMs)
                return EvaluatePosition(state, machineColor);

            if (depth == 0)
                return EvaluatePosition(state, machineColor);

            int stones = CountStones(state);
            int bw = stones < 20 ? 10 : stones < 80 ? 16 : 12;
            var moves = GetSortedCandidateMoves(state).Take(bw).ToList();


            if (isMax)
            {
                int maxEval = int.MinValue;
                foreach (var move in moves)
                {
                    var nextState = CloneState(state);
                    bool played = false;
                    if (move.IsPass)
                    {
                        nextState.Pass();
                        played = true;
                    }
                    else
                    {
                        played = nextState.TryPlayMove(move.X, move.Y, out _, out _);
                    }
                    if (!played) continue;

                    int eval = AlphaBeta(nextState, depth - 1, alpha, beta, false);
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, maxEval);
                    if (alpha >= beta) break;
                }
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (var move in moves)
                {
                    var nextState = CloneState(state);
                    bool played = false;
                    if (move.IsPass)
                    {
                        nextState.Pass();
                        played = true;
                    }
                    else
                    {
                        played = nextState.TryPlayMove(move.X, move.Y, out _, out _);
                    }
                    if (!played) continue;

                    int eval = AlphaBeta(nextState, depth - 1, alpha, beta, true);
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, minEval);
                    if (alpha >= beta) break;
                }
                return minEval;
            }
        }

        private int CountStones(Game_Engine s)
        {
            int c = 0;
            for (int y = 0; y < boardSize; y++)
                for (int x = 0; x < boardSize; x++)
                    if (s.Board[y, x] != 0) c++;
            return c;
        }

        /// <summary>
        /// Danh sách ứng cử viên nước đi (gần đá, mở rộng, hoshi) + pass, sắp xếp theo eval tĩnh.
        /// </summary>
        private List<MoveCandidate> GetSortedCandidateMoves(Game_Engine state)
        {
            var candidates = new HashSet<(int x, int y)>();

            // Lấy tất cả empty adj đến quân có trên bàn
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    if (state.Board[y, x] == 0) continue;
                    foreach (var (dx, dy) in Neighbors4)
                    {
                        int nx = x + dx;
                        int ny = y + dy;
                        if (state.InBounds(nx, ny) && state.Board[ny, nx] == 0)
                        {
                            candidates.Add((nx, ny));
                        }
                    }
                }
            }

            // Mở rộng bậc 2
            var extended = new HashSet<(int x, int y)>(candidates);
            foreach (var (cx, cy) in candidates.ToList())
            {
                foreach (var (dx, dy) in Neighbors4)
                {
                    int nx = cx + dx;
                    int ny = cy + dy;
                    if (state.InBounds(nx, ny) && state.Board[ny, nx] == 0)
                    {
                        extended.Add((nx, ny));
                    }
                }
            }
            candidates = extended;

            // Thêm Hoshi points
            int[] hoshiPoints = { 3, 9, 15 };
            foreach (int hx in hoshiPoints)
            {
                foreach (int hy in hoshiPoints)
                {
                    if (state.InBounds(hx, hy) && state.Board[hy, hx] == 0)
                    {
                        candidates.Add((hx, hy));
                    }
                }
            }

            // Đánh giá từng candidate
            var list = new List<MoveCandidate>();
            foreach (var (px, py) in candidates)
            {
                int score = EvaluateMove(state, px, py);
                if (score == int.MinValue) continue;
                score += rng.Next(5);
                list.Add(new MoveCandidate { X = px, Y = py, Score = score });
            }

            // Thêm pass
            int passScore = EvaluatePosition(state, machineColor);
            list.Add(new MoveCandidate { X = -1, Y = -1, Score = passScore });

            list.Sort((a, b) => b.Score.CompareTo(a.Score));
            return list;
        }

        private int EvaluateMove(Game_Engine state, int x, int y)
        {
            if (!state.InBounds(x, y))
                return int.MinValue;

            if (state.Board[y, x] != 0)
                return int.MinValue;

            int score = 0;

            // 1) Ưu tiên Hoshi
            if ((x == 3 || x == 9 || x == 15) && (y == 3 || y == 9 || y == 15))
            {
                score += 60;
            }

            // 2) Ưu tiên dải từ biên đến nửa bàn
            int distToEdge = Math.Min(Math.Min(x, boardSize - 1 - x),
                                      Math.Min(y, boardSize - 1 - y));

            if (distToEdge <= 1)          // sát biên
                score += 10;
            else if (distToEdge <= 4)     // vùng mở rộng thường gặp trong fuseki
                score += 40;
            else if (distToEdge <= 7)     // cận trung tâm
                score += 20;
            else                          // quá giữa
                score -= 5;

            // 3) Phân tích quân xung quanh (bán kính 3)
            int friendlyNear = 0;
            int enemyNear = 0;

            for (int dy = -3; dy <= 3; dy++)
            {
                for (int dx = -3; dx <= 3; dx++)
                {
                    if (dx == 0 && dy == 0) continue;

                    int nx = x + dx;
                    int ny = y + dy;
                    if (!state.InBounds(nx, ny)) continue;

                    int v = state.Board[ny, nx];
                    if (v == 0) continue;

                    int manhattan = Math.Abs(dx) + Math.Abs(dy);
                    if (manhattan == 0 || manhattan > 4) continue;

                    if (v == machineColor)
                    {
                        score += 18 - manhattan * 3; // gần quân mình → cộng
                        friendlyNear++;
                    }
                    else if (v == localColor)
                    {
                        score -= 10 - manhattan * 2; // gần quân địch → trừ
                        enemyNear++;
                    }
                }
            }

            // 4) Phá lãnh thổ trắng – nếu xung quanh đa số là trắng
            if (enemyNear >= 4 && enemyNear > friendlyNear + 1)
            {
                score += 70;
            }

            // 5) Kiểm tra có khí trống bên cạnh không (tránh nghẹt khí rõ ràng)
            bool hasEmptyNeighbor = false;
            foreach (var (dx, dy) in Neighbors4)
            {
                int nx = x + dx;
                int ny = y + dy;
                if (state.InBounds(nx, ny) && state.Board[ny, nx] == 0)
                {
                    hasEmptyNeighbor = true;
                    break;
                }
            }

            if (!hasEmptyNeighbor)
            {
                score -= 25;
            }

            return score;
        }

        /// <summary>
        /// Đánh giá vị trí bàn cờ toàn diện: stones, groups + libs, influence, endgame territory.
        /// </summary>
        private int EvaluatePosition(Game_Engine state, int color)
        {
            int oppColor = 3 - color;
            int myStones = 0;
            int oppStones = 0;

            // Đếm stones
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    int v = state.Board[y, x];
                    if (v == color) myStones++;
                    else if (v == oppColor) oppStones++;
                }
            }

            double score = (myStones - oppStones) * 15;

            // Kiểm tra endgame
            if (state.BlackPassed && state.WhitePassed)
            {
                // Tính territory (Japanese style approx with Chinese area)
                bool[,] vis = new bool[boardSize, boardSize];
                int terrMy = 0;
                int terrOpp = 0;

                for (int sy = 0; sy < boardSize; sy++)
                {
                    for (int sx = 0; sx < boardSize; sx++)
                    {
                        if (state.Board[sy, sx] != 0 || vis[sy, sx]) continue;

                        HashSet<int> borders = new HashSet<int>();
                        int rsize = 0;
                        Stack<(int, int)> st = new Stack<(int, int)>();
                        st.Push((sx, sy));
                        vis[sy, sx] = true;

                        while (st.Count > 0)
                        {
                            var (cx, cy) = st.Pop();
                            rsize++;

                            foreach (var (dx, dy) in Neighbors4)
                            {
                                int nx = cx + dx;
                                int ny = cy + dy;
                                if (state.InBounds(nx, ny))
                                {
                                    int v = state.Board[ny, nx];
                                    if (v == 0)
                                    {
                                        if (!vis[ny, nx])
                                        {
                                            vis[ny, nx] = true;
                                            st.Push((nx, ny));
                                        }
                                    }
                                    else
                                    {
                                        borders.Add(v);
                                    }
                                }
                            }
                        }

                        if (borders.Count == 1)
                        {
                            int borderC = borders.First();
                            if (borderC == color) terrMy += rsize;
                            else if (borderC == oppColor) terrOpp += rsize;
                        }
                    }
                }

                int myArea = terrMy + myStones;
                int oppArea = terrOpp + oppStones;
                int finalScore = myArea - oppArea;
                if (color == 2) finalScore += 7; // Komi approx
                return finalScore * 5; // Scale
            }

            // Midgame: group liberties + influence
            bool[,] visited = new bool[boardSize, boardSize];
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    if (state.Board[y, x] == 0 || visited[y, x]) continue;

                    int c = state.Board[y, x];
                    var liberties = new HashSet<(int, int)>();
                    int groupSize = 0;
                    Stack<(int, int)> stack = new Stack<(int, int)>();
                    stack.Push((x, y));
                    visited[y, x] = true;

                    while (stack.Count > 0)
                    {
                        var (cx, cy) = stack.Pop();
                        groupSize++;

                        foreach (var (dx, dy) in Neighbors4)
                        {
                            int nx = cx + dx;
                            int ny = cy + dy;
                            if (!state.InBounds(nx, ny)) continue;

                            if (state.Board[ny, nx] == 0)
                            {
                                liberties.Add((nx, ny));
                            }
                            else if (state.Board[ny, nx] == c && !visited[ny, nx])
                            {
                                visited[ny, nx] = true;
                                stack.Push((nx, ny));
                            }
                        }
                    }

                    double groupValue = groupSize * 10 + liberties.Count * 4;
                    if (c == color)
                        score += groupValue;
                    else
                        score -= groupValue;
                }
            }

            // Influence trên empty points
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    if (state.Board[y, x] != 0) continue;

                    int myAdj = 0;
                    int oppAdj = 0;
                    foreach (var (dx, dy) in Neighbors4)
                    {
                        int nx = x + dx;
                        int ny = y + dy;
                        if (state.InBounds(nx, ny))
                        {
                            int v = state.Board[ny, nx];
                            if (v == color) myAdj++;
                            else if (v == oppColor) oppAdj++;
                        }
                    }
                    int diff = myAdj - oppAdj;
                    score += diff * 3;
                }
            }

            // Center control bonus
            for (int cy = 4; cy < boardSize - 4; cy += 4)
            {
                for (int cx = 4; cx < boardSize - 4; cx += 4)
                {
                    int v = state.Board[cy, cx];
                    if (v == color) score += 15;
                    else if (v == oppColor) score -= 15;
                }
            }

            return (int)score;
        }
    }
}