CREATE DATABASE GoGameDB;
GO
USE GoGameDB;
GO

CREATE TABLE [User] (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100),
    Elo INT DEFAULT 1000,
    JoinDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE GameRoom (
    RoomID INT IDENTITY(1,1) PRIMARY KEY,
    RoomName NVARCHAR(50) NOT NULL,
    HostID INT NOT NULL,
    Status NVARCHAR(20) DEFAULT 'Waiting',
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (HostID) REFERENCES [User](UserID)
);
GO

CREATE TABLE GameMatch (
    MatchID INT IDENTITY(1,1) PRIMARY KEY,
    RoomID INT NOT NULL,
    PlayerBlackID INT NOT NULL,
    PlayerWhiteID INT NOT NULL,
    WinnerID INT NULL,
    StartTime DATETIME DEFAULT GETDATE(),
    EndTime DATETIME NULL,
    BoardSize INT DEFAULT 19,
    Result NVARCHAR(50),
    FOREIGN KEY (RoomID) REFERENCES GameRoom(RoomID),
    FOREIGN KEY (PlayerBlackID) REFERENCES [User](UserID),
    FOREIGN KEY (PlayerWhiteID) REFERENCES [User](UserID),
    FOREIGN KEY (WinnerID) REFERENCES [User](UserID)
);
GO

CREATE TABLE Move (
    MoveID INT IDENTITY(1,1) PRIMARY KEY,
    MatchID INT NOT NULL,
    PlayerID INT NOT NULL,
    MoveNumber INT NOT NULL,
    X INT NOT NULL,
    Y INT NOT NULL,
    PlayedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MatchID) REFERENCES GameMatch(MatchID),
    FOREIGN KEY (PlayerID) REFERENCES [User](UserID)
);
GO

CREATE TABLE ChatMessage (
    MessageID INT IDENTITY(1,1) PRIMARY KEY,
    RoomID INT NOT NULL,
    SenderID INT NOT NULL,
    Content NVARCHAR(500),
    SentAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoomID) REFERENCES GameRoom(RoomID),
    FOREIGN KEY (SenderID) REFERENCES [User](UserID)
);
GO

CREATE TABLE PlayerRoom (
    RoomID INT NOT NULL,
    UserID INT NOT NULL,
    JoinTime DATETIME DEFAULT GETDATE(),
    PRIMARY KEY (RoomID, UserID),
    FOREIGN KEY (RoomID) REFERENCES GameRoom(RoomID),
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);
GO

INSERT INTO [User] (Username, PasswordHash, Email) VALUES 
('player1', 'hash1', 'p1@example.com'),
('player2', 'hash2', 'p2@example.com'),
('player3', 'hash3', 'p3@example.com');
GO

INSERT INTO GameRoom (RoomName, HostID, Status) VALUES 
('Room A', 1, 'Waiting'),
('Room B', 2, 'Playing');
GO

INSERT INTO GameMatch (RoomID, PlayerBlackID, PlayerWhiteID, WinnerID, BoardSize, Result)
VALUES (1, 1, 2, 1, 19, 'Black wins by 3.5 points');
GO

INSERT INTO Move (MatchID, PlayerID, MoveNumber, X, Y) VALUES
(1, 1, 1, 3, 3),
(1, 2, 2, 16, 16),
(1, 1, 3, 4, 4);
GO

INSERT INTO ChatMessage (RoomID, SenderID, Content) VALUES
(1, 1, 'Good luck!'),
(1, 2, 'Have fun!');
GO

INSERT INTO PlayerRoom (RoomID, UserID) VALUES
(1, 1),
(1, 2),
(2, 2),
(2, 3);
GO