use UNIVER;

DECLARE @capacity int = (select cast(sum(AUDITORIUM_CAPACITY) as int) from AUDITORIUM),		
		@q int = (select cast(count(*) as int) from AUDITORIUM),							
		@avg int = (select cast(avg(AUDITORIUM_CAPACITY) as int) from AUDITORIUM);			

DECLARE	@lessavg int = (select cast(count(*) as int) from AUDITORIUM where AUDITORIUM_CAPACITY < @avg);
DECLARE	@percent float = cast(cast(@lessavg as float) / cast(@q as float) * 100  as float);

if @capacity > 200
begin
	SELECT @q 'Количество аудиторий', @avg 'Среднее количество мест',
	@lessavg 'Количество аудиторий < среднего', cast(@percent as varchar) + '%' 'Процент аудиторий, которые < среднего'
end
else if @capacity < 200
begin
	print @capacity
end;
