namespace GoClient
{
    public class GoGame
    {
        public int Size { get; } //kích thước bàn cờ
        public int[,] Board { get; private set; } //0 = ô trống, 1 = đen, 2 = trắng
        public int CurrentPlayer { get; private set; } = 1; // 1=đen, 2=trắng
        public (int x, int y)? KoPoint { get; private set; } //Điểm ko theo luật

        public GoGame(int size = 19)
        {
            //tạo bàn cờ
            Size = size; 
            Board = new int[size, size];
        }
        
        //hàm kiểm tra tọa độ có nằm trong bàn cờ hay không
        public bool InBounds(int x, int y) =>
            x >= 0 && x < Size && y >= 0 && y < Size;
        //định nghĩa về 4 khí xung quanh
        IEnumerable<(int x, int y)> Neighbors(int x, int y)
        {
            if (InBounds(x + 1, y)) yield return (x + 1, y);
            if (InBounds(x - 1, y)) yield return (x - 1, y);
            if (InBounds(x, y + 1)) yield return (x, y + 1);
            if (InBounds(x, y - 1)) yield return (x, y - 1);
        }
        //hàm tạo nhóm các quân cờ với nhau
        void GetGroup(int x, int y, out List<(int, int)> stones, out HashSet<(int, int)> liberties)
        {
            int color = Board[y, x];
            stones = new();
            liberties = new();
            //tránh bị duyệt trùng
            bool[,] visited = new bool[Size, Size];
            Queue<(int, int)> q = new();

            visited[y, x] = true;
            q.Enqueue((x, y));

            while (q.Count > 0)
            {
                var (cx, cy) = q.Dequeue();
                stones.Add((cx, cy));
                //duyệt 4 quân xung quanh
                foreach (var (nx, ny) in Neighbors(cx, cy))
                {
                    if (Board[ny, nx] == 0)
                        liberties.Add((nx, ny)); //ô trống, khí của nhóm
                    else if (Board[ny, nx] == color && !visited[ny, nx])
                    {
                        visited[ny, nx] = true; //quân cùng màu, chưa duyệt -> cùng nhóm
                        q.Enqueue((nx, ny));
                    }
                }
            }
        }
        //hàm kiểm tra nước cờ
        public bool TryPlayMove(int x, int y, out List<(int, int)> captured, out string error)
        {
            captured = new();
            error = null;

            if (!InBounds(x, y))
            {
                //error = "Ngoài bàn cờ";
                return false;
            }

            if (Board[y, x] != 0)
            {
                //error = "Vị trí đã có quân";
                return false;
            }

            if (KoPoint.HasValue && KoPoint.Value == (x, y))
            {
                error = "Vi phạm luật ko";
                return false;
            }
            //đặt quân tạm thời
            int color = CurrentPlayer; //màu quân hiện tại
            int enemy = (color == 1) ? 2 : 1; //màu đối thủ

            Board[y, x] = color;

            List<(int, int)> allCaptured = new();//nơi chứa những quân bị bắt
            //kiểm tra bắt quân
            foreach (var (nx, ny) in Neighbors(x, y))
            {
                if (Board[ny, nx] == enemy)
                {
                    //kiểm tra nhóm quân của đối thủ
                    GetGroup(nx, ny, out var grp, out var libs);
                    if (libs.Count == 0) //hết khí -> bị bắt
                    {
                        allCaptured.AddRange(grp);
                    }
                }
            }
            //xóa quân đó khi bị bắt
            foreach (var (cx, cy) in allCaptured)
                Board[cy, cx] = 0;
            //hàm chống tự sát
            GetGroup(x, y, out var myGrp, out var myLibs);
            if (myLibs.Count == 0 && allCaptured.Count == 0)
            {
                Board[y, x] = 0;
                //error = "Tự sát (không có khí)";
                return false;
            }
            //cập nhật luật ko
            KoPoint = null;
            if (allCaptured.Count == 1)
                KoPoint = allCaptured[0];
            //kết thúc lượt
            captured = allCaptured;
            CurrentPlayer = (CurrentPlayer == 1) ? 2 : 1;
            return true;
        }
        // hàm bỏ lượt
        public void Pass()
        {
            KoPoint = null;
            CurrentPlayer = (CurrentPlayer == 1) ? 2 : 1;
        }

        // ========= Setters (cho Firebase) ==========
        //dùng để gán lại trạng thái game từ dữ liệu bên ngoài
        public void SetBoard(int[,] b) => Board = b;
        public void SetCurrentPlayer(int p) => CurrentPlayer = p;
        public void SetKoPoint((int, int)? p) => KoPoint = p;
    }
}

