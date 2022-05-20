select
	--count(1)
	*
from dbo.Users du
where
	du.Username = @username
	and du.Password = @password