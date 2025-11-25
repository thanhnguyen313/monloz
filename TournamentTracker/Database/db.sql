-- 1. Sử dụng đúng Database
USE [TournamentTracker];
GO

-- 2. Tạo bảng Teams (Đội bóng)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Teams]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Teams](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [TEAMNAME] [nvarchar](100) NOT NULL,
        [COACH] [nvarchar](100) NULL,
        [LOGOPATH] [nvarchar](255) NULL,
        PRIMARY KEY CLUSTERED ([ID] ASC)
    );
    PRINT 'Da tao bang Teams.';
END
GO

-- 3. Tạo bảng Players (Cầu thủ)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Players]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Players](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [PLAYERNAME] [char](36) NULL,
        [POSITION] [char](3) NULL,
        [AGE] [int] NULL,
        [IDTEAM] [int] NULL,
        PRIMARY KEY CLUSTERED ([ID] ASC)
    );
    
    -- Tạo khóa ngoại liên kết Player với Team
    ALTER TABLE [dbo].[Players] WITH CHECK ADD CONSTRAINT [FK_Players_Teams] FOREIGN KEY([IDTEAM])
    REFERENCES [dbo].[Teams] ([ID]);
    
    PRINT 'Da tao bang Players.';
END
GO

-- 4. Tạo bảng Matches (Lịch thi đấu) - Cái này cần cho tính năng bạn vừa làm
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Matches]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Matches](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [Round] [int] NOT NULL,
        [HomeTeamID] [int] NOT NULL,
        [AwayTeamID] [int] NOT NULL,
        [HomeScore] [int] NULL,
        [AwayScore] [int] NULL,
        PRIMARY KEY CLUSTERED ([ID] ASC)
    );
    PRINT 'Da tao bang Matches.';
END
GO
USE [TournamentTracker];
GO

-- Kiểm tra xem bảng có chưa, chưa có thì mới tạo
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Account]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Account](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [DisplayName] [nvarchar](100) NULL, -- Tên hiển thị (VD: Admin, Staff)
        [UserName] [varchar](100) NOT NULL, -- Tên đăng nhập
        [Password] [varchar](100) NOT NULL, -- Mật khẩu
        [Type] [int] DEFAULT 1,             -- Loại tài khoản (1: User, 0: Admin...)
        [CreatedAt] [datetime] DEFAULT GETDATE(), -- Ngày tạo (Tự động lấy giờ hiện tại)
        PRIMARY KEY CLUSTERED ([ID] ASC)
    );
    
    -- Thêm một tài khoản mẫu để test đăng nhập luôn
    INSERT INTO Account (DisplayName, UserName, Password, Type) 
    VALUES (N'Admin', 'admin', '123456', 0);
    
    PRINT 'Da tao bang Account va them tai khoan mau (admin/123456)';
END
ELSE
BEGIN
    PRINT 'Bang Account da ton tai roi.';
END
GO


--Thêm dữ liệu test
-- Thêm 4 đội bóng vào bảng Teams
INSERT INTO Teams (TEAMNAME, COACH) VALUES (N'Manchester Utd', N'Erik ten Hag');
INSERT INTO Teams (TEAMNAME, COACH) VALUES (N'Chelsea FC', N'Pochettino');
INSERT INTO Teams (TEAMNAME, COACH) VALUES (N'Arsenal', N'Arteta');
INSERT INTO Teams (TEAMNAME, COACH) VALUES (N'Liverpool', N'Klopp');
-- Thêm 2 trận đấu cho Vòng 1
INSERT INTO Matches (Round, HomeTeamID, AwayTeamID, HomeScore, AwayScore) 
VALUES (1, 1, 2, NULL, NULL); -- MU (1) vs Chelsea (2)

INSERT INTO Matches (Round, HomeTeamID, AwayTeamID, HomeScore, AwayScore) 
VALUES (1, 3, 4, NULL, NULL); -- Arsenal (3) vs Liverpool (4)