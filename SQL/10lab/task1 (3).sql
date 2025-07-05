use UNIVER 

exec SP_HELPINDEX 'AUDITORIUM' 
exec SP_HELPINDEX 'AUDITORIUM_TYPE'
exec SP_HELPINDEX 'FACULTY'
exec SP_HELPINDEX 'PROFESSION'
exec SP_HELPINDEX 'PROGRESS'
exec SP_HELPINDEX 'PULPIT'
exec SP_HELPINDEX 'STUDENT'
exec SP_HELPINDEX 'SUBJECT'
exec SP_HELPINDEX 'TEACHER'

CREATE TABLE #EX1
(	
	ID int identity(1,1),
	STRING varchar(13)
)

set nocount on
declare @I int = 0
while @I < 1000
begin
	insert #EX1
	values ('str ' + cast(@I as char))
	set @I += 1
end

SELECT * FROM #EX1 WHERE ID BETWEEN 20 AND 60;

checkpoint; 
DBCC DROPCLEANBUFFERS; 

create clustered index #EX1_CL on #EX1(ID asc)

drop index #EX1_CL on #EX1
drop table #EX1