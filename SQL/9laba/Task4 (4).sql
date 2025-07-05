USE UNIVER

DECLARE @t2 int = 45, 
		@z float(10),
		@x int = 52;

if @t2 > @x
begin
	set @z = power(sin(@t2),2);
	print 'Z = '+ cast(@z as varchar(15));	
end

else if @t2 < @x
begin
	set @z = 4 * (@t2 + @x);
	print 'Z = '+ cast(@z as varchar(15));
end

else if @t2 = @x
begin
	set @z = 1 - exp(@x-2);
	print 'Z = '+ cast(@z as varchar(15));
end

DECLARE @full_name varchar(100) = 'Федорович Вадим Геннадьевич';

set @full_name = (select substring(@full_name, 1, charindex(' ', @full_name)) +
substring(@full_name, charindex(' ', @full_name) + 1, 1) + '.' +
substring(@full_name, charindex(' ', @full_name, charindex(' ', @full_name) + 1)+ 1, 1) + '.');

print @full_name;

DECLARE @next_month int = MONTH(GETDATE()) + 1;
select * from STUDENT where MONTH(STUDENT.BDAY) = @next_month;

select CASE
	when DATEPART(weekday, PDATE) = 1 then 'Понедельник'
	when DATEPART(weekday, PDATE) = 2 then 'Вторник'
	when DATEPART(weekday, PDATE) = 3 then 'Среда'
	when DATEPART(weekday, PDATE) = 4 then 'Четверг'
	when DATEPART(weekday, PDATE) = 5 then 'Пятница'
	when DATEPART(weekday, PDATE) = 6 then 'Суббота'
	when DATEPART(weekday, PDATE) = 7 then 'Воскресенье'
end
from PROGRESS where SUBJECT = 'СУБД'
select * from PROGRESS