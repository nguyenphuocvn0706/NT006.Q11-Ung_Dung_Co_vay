using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Co_Vay
{
    /// <summary>
    /// Engine chính cho game cờ vây.
    /// Được thiết kế để:
    /// - Serialize/Deserialize lên Firebase bằng System.Text.Json
    /// </summary>
    public class Game_Engine
    {
        // Lượt hiện tại: 1 = Đen, 2 = Trắng
        [JsonInclude]
        public int CurrentPlayer { get; private set; } = 1;

        // Cờ đã bỏ lượt
        [JsonInclude]
        public bool BlackPassed { get; private set; }

        [JsonInclude]
        public bool WhitePassed { get; private set; }

        // Kích thước bàn cờ (19 cho 19x19)
        [JsonInclude]
        public int Size { get; private set; }

        // Mảng 2 chiều thật dùng để chơi, KHÔNG serialize trực tiếp
        [JsonIgnore]
        public int[,] Board { get; private set; }

        /// <summary>
        /// Dạng board để serialize (int[][]).
        /// System.Text.Json không hỗ trợ int[,], nên ta map qua int[][].
        /// </summary>
        [JsonInclude]
        public int[][] SerializableBoard
        {
            get
            {
                if (Board == null) return null;

                var result = new int[Size][];
                for (int y = 0; y < Size; y++)
                {
                    result[y] = new int[Size];
                    for (int x = 0; x < Size; x++)
                        result[y][x] = Board[y, x];
                }
                return result;
            }
            set
            {
                if (value == null) return;
                int rows = value.Length;
                if (rows == 0) return;
                int cols = value[0].Length;

                Size = rows;
                Board = new int[rows, cols];

                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < cols; x++)
                    {
                        Board[y, x] = value[y][x];
                    }
                }
            }
        }

        // Sự kiện Double Pass – KHÔNG được serialize
        [JsonIgnore]
        public Action DoublePassHappened;

        // ===== Constructors =====

        /// <summary>
        /// Constructor không tham số:
        /// dùng cho deserialization từ Firebase (System.Text.Json bắt buộc).
        /// </summary>
        public Game_Engine()
        {
            
        }

        /// <summary>
        /// Constructor dùng khi khởi tạo game mới:
        /// new Game_Engine(19);
        /// </summary>
        public Game_Engine(int size)
        {
            Size = size;
            Board = new int[size, size];
            CurrentPlayer = 1;
            BlackPassed = false;
            WhitePassed = false;
        }

        //truy cập từ form khác
        public void CopyFrom(Game_Engine other)
        {
            this.CurrentPlayer = other.CurrentPlayer;
            this.BlackPassed = other.BlackPassed;
            this.WhitePassed = other.WhitePassed;
        }
        // ===== Logic game =====

        public void ResetPassFlags()
        {
            BlackPassed = false;
            WhitePassed = false;
        }

        public bool InBounds(int x, int y)
            => x >= 0 && x < Size && y >= 0 && y < Size;

        public bool TryPlayMove(int x, int y, out int captured, out string error)
        {
            error = "";
            captured = 0;

            // Người chơi đã pass KHÔNG được đánh nữa
            if ((CurrentPlayer == 1 && BlackPassed) ||
                (CurrentPlayer == 2 && WhitePassed))
            {
                error = "You have skipped your turn and cannot make any more moves.";
                return false;
            }

            if (!InBounds(x, y))
            {
                error = "The move is outside the board.";
                return false;
            }

            if (Board[y, x] != 0)
            {
                error = "This cell is already occupied.";
                return false;
            }

            Board[y, x] = CurrentPlayer;

            int opponent = (CurrentPlayer == 1 ? 2 : 1);

            // Ăn quân đối thủ
            int totalCaptured = 0;
            foreach (var (nx, ny) in GetNeighbors(x, y))
            {
                if (InBounds(nx, ny) && Board[ny, nx] == opponent)
                {
                    if (!HasLiberties(nx, ny, opponent))
                    {
                        totalCaptured += RemoveGroup(nx, ny, opponent);
                    }
                }
            }

            // Tự sát?
            if (!HasLiberties(x, y, CurrentPlayer))
            {
                // Nếu không ăn được quân nào mà lại tự sát → không hợp lệ
                if (totalCaptured == 0)
                {
                    Board[y, x] = 0;
                    error = "Nước đi tự sát.";
                    return false;
                }
            }

            captured = totalCaptured;

            // Reset pass của bên còn lại
            if (CurrentPlayer == 1)
                WhitePassed = false;
            else
                BlackPassed = false;

            // Đổi lượt
            CurrentPlayer = (CurrentPlayer == 1 ? 2 : 1);

            return true;
        }

        public void Pass()
        {
            if (CurrentPlayer == 1)
                BlackPassed = true;
            else
                WhitePassed = true;

            // Double pass → kết thúc trận
            if (BlackPassed && WhitePassed)
            {
                DoublePassHappened?.Invoke();
                return;
            }

            // Chuyển lượt
            CurrentPlayer = (CurrentPlayer == 1 ? 2 : 1);
        }

        private bool HasLiberties(int x, int y, int color)
        {
            bool[,] visited = new bool[Size, Size];
            return CheckLiberty(x, y, color, visited);
        }

        private bool CheckLiberty(int x, int y, int color, bool[,] visited)
        {
            if (!InBounds(x, y)) return false;
            if (visited[y, x]) return false;

            visited[y, x] = true;

            if (Board[y, x] == 0)
                return true; // có khí

            if (Board[y, x] != color)
                return false;

            foreach (var (nx, ny) in GetNeighbors(x, y))
            {
                if (CheckLiberty(nx, ny, color, visited))
                    return true;
            }

            return false;
        }

        private int RemoveGroup(int x, int y, int color)
        {
            bool[,] visited = new bool[Size, Size];
            return RemoveGroupDFS(x, y, color, visited);
        }

        private int RemoveGroupDFS(int x, int y, int color, bool[,] visited)
        {
            if (!InBounds(x, y)) return 0;
            if (visited[y, x]) return 0;
            if (Board[y, x] != color) return 0;

            visited[y, x] = true;
            Board[y, x] = 0;

            int removed = 1;

            foreach (var (nx, ny) in GetNeighbors(x, y))
            {
                removed += RemoveGroupDFS(nx, ny, color, visited);
            }

            return removed;
        }

        private (int, int)[] GetNeighbors(int x, int y)
        {
            return new (int, int)[]
            {
                (x+1, y),
                (x-1, y),
                (x, y+1),
                (x, y-1)
            };
        }
    }
}
