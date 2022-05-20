USE master
GO
IF NOT EXISTS (
   SELECT name
   FROM sys.databases
   WHERE name = N'testdb'
)
CREATE DATABASE [testdb]
GO

USE [testdb]

-- Create a new table called 'Users' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
DROP TABLE dbo.Users
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Users
(
   UserId INT NOT NULL IDENTITY(1, 1) PRIMARY KEY, -- primary key column
   Username [NVARCHAR](450) NOT NULL UNIQUE,
   -- TODO: store as encrypted instead of plaintext
   Password [NVARCHAR](MAX) NOT NULL,
   Creation DATETIME NOT NULL,
   LastLoggedIn DATETIME NULL
);
GO

-- Create a new table called 'Projects' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Projects', 'U') IS NOT NULL
DROP TABLE dbo.Projects
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Projects
(
   ProjectId INT NOT NULL IDENTITY(1, 1) PRIMARY KEY, -- primary key column
   Name [NVARCHAR](450) NOT NULL UNIQUE
);
GO

-- Create a new table called 'Attributes' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Attributes', 'U') IS NOT NULL
DROP TABLE dbo.Attributes
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Attributes
(
   AttributeId INT NOT NULL IDENTITY(1, 1) PRIMARY KEY, -- primary key column
   Name [NVARCHAR](450) NOT NULL UNIQUE
);
GO

-- Create a new table called 'Notes' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Notes', 'U') IS NOT NULL
DROP TABLE dbo.Notes
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Notes
(
   NoteId INT NOT NULL IDENTITY(1, 1) PRIMARY KEY, -- primary key column
   Text [NVARCHAR](450) NOT NULL,
   Creation DATETIME NOT NULL,
   ProjectId INT NOT NULL FOREIGN KEY REFERENCES dbo.Projects(ProjectId)
);
GO

-- Create a new table called 'NoteAttributes' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.NoteAttributes', 'U') IS NOT NULL
DROP TABLE dbo.NoteAttributes
GO
-- Create the table in the specified schema
CREATE TABLE dbo.NoteAttributes
(
   NoteId INT NOT NULL FOREIGN KEY REFERENCES dbo.Notes(NoteId),
   AttributeId INT NOT NULL FOREIGN KEY REFERENCES dbo.Attributes(AttributeId)
);
GO