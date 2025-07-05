use F_MyBase;

go
create function Type_Mashine (@Type varchar(20)) returns int
as
begin
		declare @COUNT int = (select count(*)
						  from  ������������ inner join ��������� on ������������.[�������� ������������] = ���������.[�������� ������������])
						
	return @COUNT;
end;

declare @RES int = dbo.Type_Mashine('���������')
print '���������� ��������� �������: ' + cast(@RES as varchar)

--drop function COUNT_STUDENTS

--2

create FUNCTION StrokeOfMashines(@tz char(20)) returns char(300) 
     as
     begin  
     declare @tv char(20);  
     declare @t varchar(300) = '������: ';  
     declare ZkTovar CURSOR LOCAL 
     for select ������������.[�������� ������������] from ������������ where ������������.[��� ������������] = @tz;
     open ZkTovar;	  
     fetch  ZkTovar into @tv;   	 
     while @@fetch_status = 0                                     
     begin 
         set @t = @t + ', ' + rtrim(@tv);         
         FETCH  ZkTovar into @tv; 
     end;    
     return @t;
     end;  

	declare @i varchar(300) = dbo.StrokeOfMashines('���������');
	print @i

	--3
	use UNIVER;

go
create function EX3 (@FACULTY varchar(20), @PULPIT varchar(20)) returns table
as return
	select f.FACULTY ���������, p.PULPIT �������
	from   FACULTY f left join PULPIT p 
	on	   p.FACULTY = f.FACULTY
	where  f.FACULTY = isnull(@FACULTY, f.FACULTY)
	and	   p.PULPIT = isnull (@PULPIT, p.PULPIT)

--drop function FFACPUL

go
select * from FFACPUL(null, null)
select * from FFACPUL('���', null)
select * from FFACPUL(null, '��')
select * from FFACPUL('����', '��')

--4
use F_MyBase
go
alter function EX42 (@type varchar(20))
returns int
as
begin
declare @count int = (
        select count(*) 
        from ������������ inner join ��������� on ������������.[�������� ������������] = ���������.[�������� ������������]
        where ������������.[��� ������������] = @type);
		return isnull(@count,0);
end;
go

declare @r int = dbo.EX42('���������');
print '� ������������: ' + cast(@r as varchar(20))

declare @r int = dbo.EX42('');
print '� ������������(��� ������� ������): ' + cast(@r as varchar(20))