using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Vay
{
    public enum RoomType
    {
        Random,
        Private
    }

    public enum MatchStatus
    {
        Waiting,
        InProgress,
        Finished,
        Cancelled
    }

    public enum MatchResultType
    {
        None,
        DoublePass,
        Resign,
        Cancelled
    }

    [Serializable]
    public class PlayerInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    [Serializable]
    public class MatchInfo
    {
        public string MatchId { get; set; }
        public RoomType RoomType { get; set; }

        public MatchStatus Status { get; set; }

        public PlayerInfo BlackPlayer { get; set; }
        public PlayerInfo WhitePlayer { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public MatchResultType ResultType { get; set; }
        public string WinnerPlayerId { get; set; }

        public double? BlackScore { get; set; }
        public double? WhiteScore { get; set; }

        public string ScoringRule { get; set; }          // "Japanese" hoặc "Chinese"
        public int TimePerSideMinutes { get; set; }      // 15, 30, 60, 120, 180

        public bool DrawRequested { get; set; }          // đề nghị hòa
        public string DrawRequesterId { get; set; }      // Id người gửi đề nghị hòa

        public bool IsRandom { get; set; } = false;
    }

    [Serializable]
    public class ChatMessage
    {
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
        public long Timestamp { get; set; }   
    }
}
