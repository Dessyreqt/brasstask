CREATE TABLE [dbo].[AuditLog] (
    [AuditLogId] INT IDENTITY(1,1) PRIMARY KEY,
    [TableName] VARCHAR(100) NOT NULL,
    [UserId] UNIQUEIDENTIFIER,
    [RecordId] UNIQUEIDENTIFIER NOT NULL,
    [ActionType] VARCHAR(20) NOT NULL,
    [ActionDate] DATETIME NOT NULL,
    [ColumnName] VARCHAR(100),
    [OldValue] VARCHAR(MAX),
    [NewValue] VARCHAR(MAX),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId])
);
