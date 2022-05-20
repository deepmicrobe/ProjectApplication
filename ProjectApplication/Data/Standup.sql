--use testdb

declare
	@creation datetime = GETUTCDATE()

--begin tran
INSERT INTO dbo.Projects (Name) VALUES ('Project1');
INSERT INTO dbo.Projects (Name) VALUES ('Project2');
INSERT INTO dbo.Projects (Name) VALUES ('Project3');
INSERT INTO dbo.Projects (Name) VALUES ('Project4');
INSERT INTO dbo.Projects (Name) VALUES ('Project5');
INSERT INTO dbo.Attributes (Name) VALUES ('Attribute1');
INSERT INTO dbo.Attributes (Name) VALUES ('Attribute2');
INSERT INTO dbo.Attributes (Name) VALUES ('Attribute3');
INSERT INTO dbo.Attributes (Name) VALUES ('Attribute4');
INSERT INTO dbo.Attributes (Name) VALUES ('Attribute5');
INSERT INTO dbo.Users (Username, Password, Creation) VALUES ('Bob', 'BobPassword', @creation);
INSERT INTO dbo.Users (Username, Password, Creation) VALUES ('Fred', 'FredPassword', @creation);
INSERT INTO dbo.Users (Username, Password, Creation) VALUES ('George', 'GeorgePassword', @creation);
--rollback

select
	*
from dbo.Projects

select
	*
from dbo.Attributes

select
	*
from dbo.Users