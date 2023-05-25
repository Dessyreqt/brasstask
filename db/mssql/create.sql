USE [master]

DECLARE @Created bit
SET @Created = 0

IF NOT EXISTS(SELECT * FROM sys.databases WHERE [name] = 'BrassTask')
BEGIN
	SET @Created = 1

	CREATE DATABASE [BrassTask]

	ALTER DATABASE [BrassTask] SET COMPATIBILITY_LEVEL = 130
END

SELECT @Created
