create table #EX3
(
	TKEY int, 
    ID int identity(1, 1),
    TF varchar(100)
);

set nocount on;
declare @iter int = 0;
while @iter < 20000
begin
	INSERT #EX3(TKEY, TF) values(floor(30000 * RAND()), replicate('строка ', 10));
	set @iter += 1;
end

select ID, TKEY from #EX3 where TKEY > 15000

checkpoint;
DBCC DROPCLEANBUFFERS;

create index #EX3_TKEY_ID on #EX3(TKEY) include (ID)

drop index #EX3_TKEY_ID on #EX3
drop table #EX3