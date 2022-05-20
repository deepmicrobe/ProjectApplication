USE [testdb]

-- Drop the table if it already exists
IF OBJECT_ID('dbo.NoteAttributes', 'U') IS NOT NULL
DROP TABLE dbo.NoteAttributes
GO

-- Drop the table if it already exists
IF OBJECT_ID('dbo.Notes', 'U') IS NOT NULL
DROP TABLE dbo.Notes
GO

-- Drop the table if it already exists
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
DROP TABLE dbo.Users
GO

-- Drop the table if it already exists
IF OBJECT_ID('dbo.Projects', 'U') IS NOT NULL
DROP TABLE dbo.Projects
GO

-- Drop the table if it already exists
IF OBJECT_ID('dbo.Attributes', 'U') IS NOT NULL
DROP TABLE dbo.Attributes
GO