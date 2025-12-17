USE master;
GO

-- =============================================================
-- BƯỚC 1: RESET DATABASE (Xóa cũ tạo mới sạch sẽ)
-- =============================================================
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'TournamentTracker')
BEGIN
    ALTER DATABASE [TournamentTracker] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [TournamentTracker];
END
GO

CREATE DATABASE [TournamentTracker];
GO
USE [TournamentTracker];
GO

-- =============================================================
-- BƯỚC 2: TẠO CẤU TRÚC BẢNG (SCHEMA)
-- =============================================================

-- 1. Bảng ACCOUNT (Tài khoản đăng nhập)
CREATE TABLE [dbo].[Account](
    [ID] INT IDENTITY(1,1) PRIMARY KEY,
    [Username] NVARCHAR(50) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(255) NOT NULL, -- Mật khẩu (SHA256)
    [CreatedAt] DATETIME DEFAULT GETDATE()
);

-- 2. Bảng TOURNAMENTS (Giải đấu) - [ĐÃ CẬP NHẬT]
CREATE TABLE [dbo].[Tournaments](
    [ID] INT IDENTITY(1,1) PRIMARY KEY,
    [NAME] NVARCHAR(100) NOT NULL,
    [LOCATION] NVARCHAR(100) NULL,
    [STARTDATE] DATE NULL,
    [PRIZE] NVARCHAR(50) NULL,
    [POSTERPATH] NVARCHAR(255) NULL,
    [SPORT] NVARCHAR(50),   
    [TEAM_COUNT] INT,
    [CreatedBy] INT NULL, -- Liên kết: Giải này do ai tạo?
    
    -- [MỚI] 3 CỘT LƯU FORMAT GIẢI ĐẤU
    [FormatMode] NVARCHAR(50) DEFAULT 'Single',    -- 'Single' hoặc 'Multi'
    [Stage1Format] NVARCHAR(50) DEFAULT 'Knockout', -- Vòng 1: 'Knockout', 'Round Robin', 'Group Stage'
    [Stage2Format] NVARCHAR(50) NULL,               -- Vòng 2 (Nếu có): 'Knockout', NULL

    FOREIGN KEY (CreatedBy) REFERENCES Account(ID)
);

-- 3. Bảng TEAMS (Đội bóng)
CREATE TABLE [dbo].[Teams](
    [ID] INT IDENTITY(1,1) PRIMARY KEY,
    [TournamentID] INT NOT NULL, -- Đội thuộc về giải nào
    [TEAMNAME] NVARCHAR(100) NOT NULL,
    [COACH] NVARCHAR(100) NULL,
    FOREIGN KEY (TournamentID) REFERENCES Tournaments(ID) ON DELETE CASCADE
);

-- 4. Bảng PLAYERS (Cầu thủ)
CREATE TABLE [dbo].[Players](
    [ID] INT IDENTITY(1,1) PRIMARY KEY,
    [IDTEAM] INT NOT NULL, -- Cầu thủ thuộc đội nào
    [PLAYERNAME] NVARCHAR(100) NOT NULL,
    [POSITION] NVARCHAR(50) NULL, 
    [AGE] INT NULL,
    [NUMBER] INT NULL,
    FOREIGN KEY (IDTEAM) REFERENCES Teams(ID) ON DELETE CASCADE
);

-- 5. Bảng MATCHES (Trận đấu)
CREATE TABLE [dbo].[Matches](
    [ID] INT IDENTITY(1,1) PRIMARY KEY,
    [TournamentID] INT NOT NULL, -- Trận đấu thuộc giải nào
    [Round] INT NOT NULL,          -- Vòng đấu (1, 2, 3...)
    [RoundType] INT DEFAULT 0,     -- 0: Vòng bảng/Vòng 1, 1: Knockout/Vòng 2
    [GroupName] NVARCHAR(10) NULL, -- Tên bảng đấu (A, B, C...)
    
    [HomeTeamID] INT NOT NULL,     -- Đội nhà
    [AwayTeamID] INT NOT NULL,     -- Đội khách
    
    [HomeScore] INT NULL,          -- Tỷ số (NULL = Chưa đá)
    [AwayScore] INT NULL,
    
    [MatchDate] DATETIME NULL,
    [Location] NVARCHAR(100) NULL,
    [Status] INT DEFAULT 0,        -- 0: Chưa đá, 1: Đang đá, 2: Kết thúc
    [WinnerID] INT NULL,           -- Đội thắng
    [ParentMatchId] INT NULL,      -- Nhánh đấu cha (cho cây knockout)

    FOREIGN KEY (TournamentID) REFERENCES Tournaments(ID), 
    FOREIGN KEY (HomeTeamID) REFERENCES Teams(ID),
    FOREIGN KEY (AwayTeamID) REFERENCES Teams(ID),
    FOREIGN KEY (WinnerID) REFERENCES Teams(ID),
    FOREIGN KEY (ParentMatchId) REFERENCES Matches(ID)
);
GO


-- ======================================================================================
-- KHAI BÁO CÁC BIẾN CẦN DÙNG (Để lưu ID tự động)
-- ======================================================================================
DECLARE @TourID INT;
DECLARE @T1 INT, @T2 INT, @T3 INT, @T4 INT, @T5 INT;

-- ======================================================================================
-- KỊCH BẢN 1: GIẢI CHAMPIONS LEAGUE MINI (4 Đội - Knockout)
-- ======================================================================================
PRINT N'=== Đang tạo dữ liệu giải 1: Champions League Mini ===';

-- 1. Tạo Giải
INSERT INTO [dbo].[Tournaments] 
([NAME], [LOCATION], [STARTDATE], [PRIZE], [POSTERPATH], [SPORT], [TEAM_COUNT], [FormatMode], [Stage1Format]) 
VALUES
(N'Champions League Mini 2025', N'Europe', '2025-05-01', N'$10,000,000', N'/images/ucl.jpg', N'Soccer', 4, 'Single', 'Knockout');

-- Lấy ID giải vừa tạo
SET @TourID = SCOPE_IDENTITY();

-- 2. Tạo 4 Đội
INSERT INTO [dbo].[Teams] ([TournamentID], [TEAMNAME], [COACH]) VALUES
(@TourID, N'Real Madrid', N'Carlo Ancelotti'),
(@TourID, N'Man City', N'Pep Guardiola'),
(@TourID, N'Bayern Munich', N'Vincent Kompany'),
(@TourID, N'Liverpool', N'Arne Slot');

-- Lấy ID các đội vừa tạo để dùng cho việc xếp lịch đấu và thêm cầu thủ
SELECT @T1 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'Real Madrid';
SELECT @T2 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'Man City';
SELECT @T3 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'Bayern Munich';
SELECT @T4 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'Liverpool';

-- 3. Thêm Cầu thủ (Mẫu vài người)
INSERT INTO [dbo].[Players] ([IDTEAM], [PLAYERNAME], [POSITION], [NUMBER]) VALUES
(@T1, N'Vinicius Jr', 'FW', 7), (@T1, N'Jude Bellingham', 'MF', 5),
(@T2, N'Erling Haaland', 'FW', 9), (@T2, N'Kevin De Bruyne', 'MF', 17),
(@T3, N'Harry Kane', 'FW', 9), (@T3, N'Musiala', 'MF', 42),
(@T4, N'Mo Salah', 'FW', 11), (@T4, N'Van Dijk', 'DF', 4);

-- 4. Tạo Lịch thi đấu (Bán kết)
INSERT INTO [dbo].[Matches] 
([TournamentID], [Round], [RoundType], [HomeTeamID], [AwayTeamID], [HomeScore], [AwayScore], [MatchDate], [Status], [WinnerID]) 
VALUES
-- Trận 1: Real vs Man City (Đã đá xong, Real thắng)
(@TourID, 1, 1, @T1, @T2, 3, 1, '2025-05-01 20:00:00', 2, @T1),
-- Trận 2: Bayern vs Liverpool (Chưa đá)
(@TourID, 1, 1, @T3, @T4, NULL, NULL, '2025-05-02 20:00:00', 0, NULL);


-- ======================================================================================
-- KỊCH BẢN 2: GIẢI SINH VIÊN IT (5 Đội - Vòng tròn/Round Robin)
-- ======================================================================================
PRINT N'=== Đang tạo dữ liệu giải 2: IT Student League ===';

-- 1. Tạo Giải
INSERT INTO [dbo].[Tournaments] 
([NAME], [LOCATION], [STARTDATE], [PRIZE], [POSTERPATH], [SPORT], [TEAM_COUNT], [FormatMode], [Stage1Format]) 
VALUES
(N'IT Student League 2025', N'Ho Chi Minh City', '2025-09-05', N'5,000,000 VND', N'/images/uit.jpg', N'Soccer', 5, 'Single', 'Round Robin');

SET @TourID = SCOPE_IDENTITY();

-- 2. Tạo 5 Đội
INSERT INTO [dbo].[Teams] ([TournamentID], [TEAMNAME], [COACH]) VALUES
(@TourID, N'UIT K17', N'Nguyen Van A'),
(@TourID, N'BK Polytechnic', N'Tran Van B'),
(@TourID, N'US Science', N'Le Van C'),
(@TourID, N'RMIT Tech', N'John Doe'),
(@TourID, N'FPT Software', N'Pham Van D');

-- Lấy ID
SELECT @T1 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'UIT K17';
SELECT @T2 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'BK Polytechnic';
SELECT @T3 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'US Science';
SELECT @T4 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'RMIT Tech';
SELECT @T5 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'FPT Software';

-- 3. Thêm Cầu thủ
INSERT INTO [dbo].[Players] ([IDTEAM], [PLAYERNAME], [POSITION], [NUMBER]) VALUES
(@T1, N'Nguyen Van An', 'GK', 1), (@T1, N'Le Tuan Tu', 'FW', 10),
(@T2, N'Tran Minh', 'MF', 8), (@T3, N'Le Hoang', 'DF', 4);

-- 4. Tạo Lịch đấu (Vòng 1 & Vòng 2)
INSERT INTO [dbo].[Matches] 
([TournamentID], [Round], [HomeTeamID], [AwayTeamID], [HomeScore], [AwayScore], [MatchDate], [Status]) 
VALUES
-- Vòng 1: UIT vs BK (Đang đá)
(@TourID, 1, @T1, @T2, 1, 1, GETDATE(), 1),
-- Vòng 1: US vs RMIT (Chưa đá)
(@TourID, 1, @T3, @T4, NULL, NULL, DATEADD(DAY, 1, GETDATE()), 0),
-- Vòng 2: FPT vs UIT (Chưa đá)
(@TourID, 2, @T5, @T1, NULL, NULL, DATEADD(DAY, 7, GETDATE()), 0);


-- ======================================================================================
-- KỊCH BẢN 3: GIẢI GIAO HỮU VĂN PHÒNG (4 Đội - Group Stage)
-- ======================================================================================
PRINT N'=== Đang tạo dữ liệu giải 3: Office Friendly Cup ===';

-- 1. Tạo Giải
INSERT INTO [dbo].[Tournaments] 
([NAME], [LOCATION], [STARTDATE], [PRIZE], [POSTERPATH], [SPORT], [TEAM_COUNT], [FormatMode], [Stage1Format]) 
VALUES
(N'Office Friendly Cup', N'Da Nang', '2025-06-15', N'Thùng Bia Heineken', N'/images/beer.jpg', N'Soccer', 4, 'Single', 'Group Stage');

SET @TourID = SCOPE_IDENTITY();

-- 2. Tạo 4 Đội
INSERT INTO [dbo].[Teams] ([TournamentID], [TEAMNAME], [COACH]) VALUES
(@TourID, N'Team Marketing', N'Ms. Lan'),
(@TourID, N'Team Sales', N'Mr. Hung'),
(@TourID, N'Team Dev', N'Mr. Tuan'),
(@TourID, N'Team HR', N'Ms. Mai');

-- Lấy ID
SELECT @T1 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'Team Marketing';
SELECT @T2 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'Team Sales';
SELECT @T3 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'Team Dev';
SELECT @T4 = ID FROM Teams WHERE TournamentID = @TourID AND TEAMNAME = N'Team HR';

-- 3. Tạo Lịch đấu (Đã đá hết rồi)
INSERT INTO [dbo].[Matches] 
([TournamentID], [Round], [GroupName], [HomeTeamID], [AwayTeamID], [HomeScore], [AwayScore], [Status], [WinnerID]) 
VALUES
(@TourID, 1, N'A', @T1, @T2, 2, 4, 2, @T2), -- Sales thắng
(@TourID, 1, N'A', @T3, @T4, 5, 1, 2, @T3), -- Dev thắng
(@TourID, 2, N'A', @T1, @T3, 0, 0, 2, NULL); -- Hòa

GO
PRINT N'=== ĐÃ TẠO XONG TOÀN BỘ DỮ LIỆU ===';