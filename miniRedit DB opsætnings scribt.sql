Use master
GO

Create Database MiniRedit
GO

Use MiniRedit
GO


------------------------------------------------------------------------------------------------------------
-- Create Tables

Create Table [Users](
	[UserId] [Int] NOT NULL Primary Key Identity(1, 1),
    [Name] [NVarChar](100) NOT NULL,
    [UserName] [NVarChar](50) NOT NULL,
	[Password] [VarChar](50) NOT NULL,
    [Date] Datetime Not NULL Default Getdate(),
	[Deletet] bit not Null Default 1
)
GO
Create Table [Boards](
	[BoardId] [Int] NOT NULL Primary Key Identity(1, 1),
    [Title] [NVarChar](100) NOT NULL,
    [Date] Datetime Not NULL Default Getdate(),
	[Deletet] bit not Null Default 1
)
GO
Create Table [Posts](
	[PostId] [Int] NOT NULL Primary Key Identity(1, 1),
    [Title] [NVarChar](100) NOT NULL,
    [Content] [NVarChar](2500) NOT NULL,
    [Date] Datetime Not NULL Default Getdate(),
	[Deletet] bit not Null Default 1,
	[UserId] [Int] NOT NULL References Users(UserId),
    [BoardId] [Int] NOT NULL References Boards(BoardId),
)
GO


------------------------------------------------------------------------------------------------------------
-- Create Sample Data

Insert Into Boards(Title)
Values('Default'),
('meh'),
('yeet');
GO

Insert Into Users(Name, Username, Password)
Values('Default', 'DefaultUser','meh'),
('Peter','pet' ,'meh'),
('Nicolai','HackerMan' ,'meh'),
('Casper','toxiDanfoss' ,'meh');

GO

Insert Into Posts(Title, Content, UserId, BoardId)
Values('Default', 'Default', 1, 1),
('meh', 'meh', 2, 2),
('Hacking', 'Ha Yeah like im gonna show you!', 3, 3),
('Toxisety', '***** ******* *******', 4, 2),
('Yeet', 'yeet', 1, 3);
GO


------------------------------------------------------------------------------------------------------------
-- Boards SP + CRUD

--Create
Create Procedure CreateBoard
@NewBoardTitle NVarChar(100)
AS
IF NOT EXISTS (SELECT Title FROM Boards WHERE Title = @NewBoardTitle)
BEGIN
    INSERT INTO Boards(Title )
    VALUES (@NewBoardTitle);
END;
GO


--ReadAll
Create Procedure ReadBoards
As
Select *
From Boards
GO


--ReadOneBoards
Create Procedure ReadOneBoard
@BoardId int
As
Select *
From Boards
Where BoardId = @BoardId
GO


--Update
Create Procedure UpdateBoard
@NewBoardTitle NVarChar(100),
@BoardId Int
As
UPDATE Boards 
SET Title = @NewBoardTitle
WHERE BoardId = @BoardId
GO


--Delete
Create Procedure DeleteBoard
@BoardId Int
As
UPDATE Boards 
SET Deletet = 0
WHERE BoardId = @BoardId
GO


--Full Delete
Create Procedure FullDeleteBoard
@BoardId Int
As
Delete Boards WHERE BoardId = @BoardId
Go



------------------------------------------------------------------------------------------------------------
-- Posts SP CRUD

--Create
Create Procedure CreatePost
@Title VarChar(100),
@Content VarChar(2500),
@UserId Int,
@BoardId Int,
@PostId int out
AS
Insert Into Posts(Title, Content, UserId, BoardId)
Values(@Title, @Content, @UserId, @BoardId);

set @PostId = SCOPE_IDENTITY()
GO


--ReadAll
Create Procedure ReadPosts
As
Select *
From Posts
GO


--ReadOnePost
Create Procedure ReadOnePost
@PostId int
As
Select *
From Posts
Where PostId = @PostId
GO


--GetPostByBoard
Create Procedure GetPostByBoard
@BoardId int
As
Select *
From Posts
Where BoardId = @BoardId
GO


--GetPostByUserId
Create Procedure GetPostByuserId
@UserId int
As
Select *
From Posts
Where UserId = @UserId
GO


--Read top 10
Create Procedure GetTopTenPosts
As
Select Top 10 *
From Posts
Order By PostId DESC
GO


--Update
Create Procedure UpdatePost
@Title VarChar(100),
@Content VarChar(2500),
@PostId Int,
@BoardId Int
As
UPDATE Posts
SET Title = @Title, Content = @Content, BoardId = @BoardId
WHERE PostId = @PostId
GO


--Delete
Create Procedure DeletePost
@PostId Int
As
UPDATE Posts 
SET Deletet = 0
WHERE PostId = @PostId
GO


--Full Delete
Create Procedure FullDeletePost
@PostId Int
As
Delete Posts WHERE PostId = @PostId
Go



------------------------------------------------------------------------------------------------------------
-- User SP CRUD

--Create
Create Procedure CreateUser
@Name NVarChar(100),
@UserName NVarChar(50),
@Password VarChar(50),
@UserId int out
AS
Insert Into Users(Name, UserName, Password, UserId)
Values(@Name, @UserName, @Password, @UserId);

set @UserId = SCOPE_IDENTITY()
GO


--ReadAll
Create Procedure ReadUsers
As
Select *
From Users
GO


--ReadOneUser
Create Procedure ReadOneUser
@UserId int
As
Select *
From Users
Where UserId = @UserId
GO


--ReadOneUserForLogin
Create Procedure ReadOneUserForLogin
@UserName NVarChar(50),
@Password VarChar(50)
As
Select *
From Users
Where UserName = @UserName and Password = @Password
GO


--Update
Create Procedure UpdateUser
@Name NVarChar(100),
@UserName NVarChar(50),
@Password VarChar(50),
@UserId int
As
UPDATE Users
SET Name = @Name, UserName = @UserName, Password = @Password
WHERE UserId = @UserId
GO


--Delete
Create Procedure DeleteUser
@UserId Int
As
UPDATE Users 
SET Deletet = 0
WHERE UserId = @UserId
GO


--Full Delete
Create Procedure FullDeleteUser
@UserId Int
As
Delete Users 
WHERE UserId = @UserId
GO

------------------------------------------------------------------------------------------------------------
