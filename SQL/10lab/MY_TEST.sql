use F_MyBase

exec sp_helpindex 'Оборудование'
exec sp_helpindex 'Ответственный'
exec sp_helpindex 'Списанное'

SELECT name, type_desc 
FROM tempdb.sys.indexes 
WHERE object_id = OBJECT_ID('tempdb..#T12345');

IF OBJECT_ID('tempdb..#T12345') IS NOT NULL
BEGIN
    DROP TABLE #T12345;
END

CREATE TABLE #T12345 (
    coast INT,
    name VARCHAR(20)
);
--CREATE clustered index #CL on #T12345(coast asc);
--DROP INDEX #CL ON #T12345;

declare @counter int = 0;
Set nocount on
while @counter < 100000
begin
insert #T12345(coast, name)
values(rand() *100, 'vadim')
if(@counter % 100 = 0) print'....' + cast(@counter as varchar(20));
set @counter = @counter + 1;
end;

CREATE CLUSTERED INDEX IX_T12345_coast ON #T12345(coast);
SELECT coast FROM #T12345 WHERE coast BETWEEN 1 AND 3;

select * from #T12345 where coast between 1 and 3 order by coast;

--2

CREATE table #EX
(    TKEY int, 
      CC int identity(1, 1),
      TF varchar(100)
);

  set nocount on;           
  declare @i int = 0;
  while   @i < 20000 
  begin
       INSERT #EX(TKEY, TF) values(floor(30000*RAND()), replicate('строка ', 10));
        set @i = @i + 1; 
  end;
  SELECT count(*)[количество строк] from #EX;
  SELECT * from #EX

  --CREATE index #EX_NONCLU on #EX(TKEY, CC)
  --DROP INDEX #CL ON #T12345;
    SELECT * from  #EX where  TKEY > 1500 and  CC < 4500;  
    SELECT * from  #EX order by  TKEY, CC
	SELECT * from  #EX where  TKEY = 556 and  CC > 3

--3
CREATE table #EX1
(    TKEY int, 
      CC int identity(1, 1),
      TF varchar(100)
);

  set nocount on;           
  declare @i1 int = 0;
  while   @i1 < 20000 
  begin
       INSERT #EX1(TKEY, TF) values(floor(30000*RAND()), replicate('строка ', 10));
        set @i1 = @i1 + 1; 
  end;
  SELECT count(*)[количество строк] from #EX1;
  SELECT * from #EX1

  --CREATE  index #EX_TKEY_X on #EX1(TKEY) INCLUDE (CC)
 --DROP INDEX #EX_TKEY_X ON #EX1;
  SELECT CC from #EX1 where TKEY>15000 

  --4	
  CREATE table #EX4
(    TKEY int, 
      CC int identity(1, 1),
      TF varchar(100)
);


  set nocount on;           
  declare @i4 int = 0;
  while   @i4 < 20000 
  begin
       INSERT #EX4(TKEY, TF) values(floor(30000*RAND()), replicate('строка ', 10));
        set @i4 = @i4 + 1; 
  end;
  SELECT count(*)[количество строк] from #EX4;
  SELECT * from #EX4

  --CREATE  index #EX_WHERE on #EX4(TKEY) where (TKEY>=15000 and TKEY < 20000);  
  --DROP INDEX #EX_WHERE ON #EX4;
  SELECT TKEY from  #EX4 where TKEY between 5000 and 19999; 
  SELECT TKEY from  #EX4 where TKEY>15000 and  TKEY < 20000  
  SELECT TKEY from  #EX4 where TKEY=17000

  --5
create table #EX5
(
	TKEY int, 
    ID int identity(1, 1),
    TF varchar(100)
);

set nocount on;
declare @iter int = 0;
while @iter < 15000
begin
	INSERT #EX5(TKEY, TF) values(floor(30000 * RAND()), replicate('строка ', 10));
	set @iter += 1;
end

create index #EX5_TKEY on #EX5(TKEY)

SELECT NAME [Индекс], AVG_FRAGMENTATION_IN_PERCENT [Фрагментация (%)]
FROM SYS.DM_DB_INDEX_PHYSICAL_STATS(DB_ID(N'TEMPDB'),
OBJECT_ID(N'#EX5'), NULL, NULL, NULL) SS
JOIN SYS.INDEXES II ON SS.OBJECT_ID = II.OBJECT_ID 
AND SS.INDEX_ID = II.INDEX_ID WHERE NAME IS NOT NULL;

INSERT top(10000) #EX5(TKEY, TF) select TKEY, TF from #EX5; 

SELECT NAME [Индекс], AVG_FRAGMENTATION_IN_PERCENT [Фрагментация (%)]
FROM SYS.DM_DB_INDEX_PHYSICAL_STATS(DB_ID(N'TEMPDB'),
OBJECT_ID(N'#EX5'), NULL, NULL, NULL) SS
JOIN SYS.INDEXES II ON SS.OBJECT_ID = II.OBJECT_ID
AND SS.INDEX_ID = II.INDEX_ID WHERE NAME IS NOT NULL;

alter index #EX5_TKEY on #EX5 reorganize

alter index  #EX5_TKEY on #EX5 rebuild with (online = off)

drop index #EX5_TKEY on #EX5

--6

create index #EX5_TKEY1 on #EX5(TKEY) with (fillfactor = 65)

set nocount on
declare @I6 int = 0
while @I6 < 15000
begin
	insert #EX5(TKEY, TF) 
	values (floor(30000 * rand()), replicate ('строка ', 10))
	set @I6 += 1
end

SELECT NAME [Индекс], AVG_FRAGMENTATION_IN_PERCENT [Фрагментация (%)]
FROM SYS.DM_DB_INDEX_PHYSICAL_STATS(DB_ID(N'TEMPDB'),
OBJECT_ID(N'#EX5'), NULL, NULL, NULL) SS
JOIN SYS.INDEXES II ON SS.OBJECT_ID = II.OBJECT_ID
AND SS.INDEX_ID = II.INDEX_ID WHERE NAME IS NOT NULL;

drop index #EX5_TKEY1 on #EX5
drop table #EX5

--практика

create table #E1
(
	TKEY int, 
    ID int identity(1, 1),
    TF varchar(100)
);

declare @i int = 0
while @i < 20000
begin 
insert into #E1 values(RAND()*100, 'vadim')
set @i +=1;
end;
CREATE nonclustered index #EX_NONCLU on #E1(TKEY, TF)
select * from #E1 where #E1.TKEY > 50
