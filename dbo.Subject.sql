CREATE TABLE [dbo].[Table]
(
	SubjectID INT PRIMARY KEY,
    SubjectName VARCHAR(255),
    DepartmentID INT,
    Semester INT,
    FOREIGN KEY (DepartmentID) REFERENCES Department(DepartmentID)
)
