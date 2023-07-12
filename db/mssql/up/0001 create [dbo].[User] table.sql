CREATE TABLE [dbo].[User] (
    [UserId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    [Username] VARCHAR(254) NOT NULL,
    [Email] VARCHAR(254) NOT NULL,
    [PasswordSalt] VARCHAR(100) NOT NULL,
    [PasswordHash] VARCHAR(100) NOT NULL,
    [CreatedAt] DATETIME DEFAULT GETDATE(),
    [UpdatedAt] DATETIME DEFAULT GETDATE(),
    CONSTRAINT [UQ_Users_Username] UNIQUE ([Username]),
    CONSTRAINT [UQ_Users_Email] UNIQUE ([Email])
);
