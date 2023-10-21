CREATE TABLE [dbo].[AttendanceAndExam] (
    [StudentID]         NVARCHAR (50) NULL,
    [SubjectID]         INT           NULL,
    [Semester]          INT           NULL,
    [Sessional1LabAttendance]     INT           NULL,
    [Sessional1LectureAttendance] INT           NULL,
	[Sessional2LabAttendance]     INT           NULL,
    [Sessional2LectureAttendance] INT           NULL,
	[Sessional3LabAttendance]     INT           NULL,
    [Sessional3LectureAttendance] INT           NULL,
    [SessionalExam1]    INT           NULL,
    [SessionalExam2]    INT           NULL,
    [SessionalExam3]    INT           NULL,
    CONSTRAINT [FK_Student] FOREIGN KEY ([StudentID]) REFERENCES [dbo].[Students] ([Enrollment_No]),
    CONSTRAINT [FK_Subject] FOREIGN KEY ([SubjectID]) REFERENCES [dbo].[Subjects] ([SubjectID])
);

