select distinct
	dn.*
from dbo.Notes dn
where
	dn.ProjectId = @projectId