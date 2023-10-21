CREATE TABLE [dbo].[Students]
(
	[Enrollment_No] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [Firstname] NVARCHAR(50) NOT NULL, 
    [Lastname] NVARCHAR(50) NOT NULL, 
    [DOB] DATE NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [Phone] NVARCHAR(50) NOT NULL, 
    [Department] NVARCHAR(50) NOT NULL, 
    [Semester] INT NOT NULL
)
