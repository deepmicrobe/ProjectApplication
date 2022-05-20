--use testdb

insert into dbo.Notes (Text, Creation, ProjectId) values (@text, @creation, @projectId);
select scope_identity();