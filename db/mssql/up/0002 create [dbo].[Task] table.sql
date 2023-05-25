CREATE TABLE [dbo].[Task] (
    [TaskId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    [UserId] UNIQUEIDENTIFIER,
    [TaskName] VARCHAR(100) NOT NULL,
    [Description] VARCHAR(MAX),
    [ReminderDate] DATETIME,
    [RepeatInterval] INT,
    [IsRepeatEnabled] BIT,
    [CreatedAt] DATETIME DEFAULT GETDATE(),
    [UpdatedAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId])
);
