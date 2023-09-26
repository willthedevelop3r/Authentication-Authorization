CREATE TABLE [dbo].[Users]
(
	[EmployeeId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),  
    [FirstName] NVARCHAR(50) NOT NULL,
    [LastName] NVARCHAR(50) NOT NULL,
    [PasswordHash] NVARCHAR(512) NOT NULL
)
