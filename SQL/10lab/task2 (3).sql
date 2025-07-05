create table #EX2
(
	TKEY int, 
    ID int identity(1, 1),
    TF varchar(100)
);

set nocount on;
declare @iter int = 0;
while @iter < 15000
begin
	INSERT #EX2(TKEY, TF) values(floor(30000 * RAND()), replicate('строка ', 10));
	set @iter += 1;
end

select count(*)[Количество строк] from #EX2;
select * from #EX2

CREATE index #EX2_NONCLU on #EX2(TKEY, ID)

checkpoint;  
DBCC DROPCLEANBUFFERS;  

SELECT * from  #EX2 where  TKEY > 1500 and ID < 3;  
SELECT * from  #EX2 order by  TKEY, ID

select * from #EX2 where TKEY = 3556 and ID > 3

drop index #EX2_NONCLU on #EX2
drop table #EX2