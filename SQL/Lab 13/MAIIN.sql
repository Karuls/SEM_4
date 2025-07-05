use F_MyBase

go
--create PROCEDURE GETCOUNT
--as
--begin
--declare @count int = (select COUNT(*) from Оборудование)
--select @count Число
--return @count + 1;
--end;

declare @call int = 0 ;
exec @call = GETCOUNT;
print 'fe' + cast(@call as varchar(10));
--2

declare @i int = 0, @type varchar(20) = 'гончарное', @c int = 0;
exec @i = GETCOUNT @type, @count1 = @c output
print 'Кол-во оборудовая типа '+ @type + ': ' +cast(@c as varchar(10));

--3
CREATE TABLE #Zk (
   [Название оборудования] [nchar](10) NULL,
	[Тип оборудования] [nchar](10) NULL,
	[Дата поступления] [date] NULL,
	[Количество] [nchar](10) NOT NULL,
	[Подразделение установки] [nchar](10) NULL
);


--ALTER PROCEDURE GETCOUNT @p VARCHAR(20)
--AS
--BEGIN
--    DECLARE @k INT = (SELECT COUNT(*) FROM Оборудование);
--    SELECT * FROM Оборудование WHERE Оборудование.[Тип оборудования] = @p;
--END;

INSERT #Zk EXEC GETCOUNT @p = 'гончарное';
INSERT #Zk EXEC GETCOUNT @p = 'Шлифовка';

Select * from #Zk

--4
CREATE PROCEDURE EX4 
    @a NCHAR(10), 
    @b NCHAR(10), 
    @c DATE, 
    @d NCHAR(10), 
    @f NCHAR(10)
AS
BEGIN
    BEGIN TRY
        INSERT INTO #Zk VALUES (@a, @b, @c, @d, @f);
    END TRY
    BEGIN CATCH 
        PRINT 'Номер ошибки      : ' + CAST(ERROR_NUMBER() AS VARCHAR(6));
        PRINT 'Сообщение         : ' + ERROR_MESSAGE();
        PRINT 'Уровень           : ' + CAST(ERROR_SEVERITY() AS VARCHAR(6));
        PRINT 'Состояние (State) : ' + CAST(ERROR_STATE() AS VARCHAR(6));
        PRINT 'Строка с ошибкой  : ' + CAST(ERROR_LINE() AS VARCHAR(6));
        RETURN -1;
    END CATCH
END;
--drop procedure EX4
 DECLARE @a NCHAR(10), 
         @b NCHAR(10), 
         @c DATE, 
         @d NCHAR(10), 
         @f NCHAR(10)
exec EX4 @a = 'станок16', @b = 'гончарное', @c = '03-03-2222', @d = '11', @f = 'УАХ';
select * from #Zk
--5

CREATE PROC TypeOfMashine @p CHAR(10)
AS
BEGIN TRY
    -- Проверяем существование
    IF NOT EXISTS (SELECT 1 FROM Оборудование WHERE Оборудование.[Тип оборудования] = @p)
        RAISERROR('Ошибка в параметрах', 16, 1)
    
    -- Объявляем переменные
    DECLARE @subject_list VARCHAR(500) = ''
    DECLARE @count INT = 0
    
    -- Формируем строку с  через запятую
    SELECT @subject_list = @subject_list + RTRIM([Название оборудования]) + ', ',
           @count = @count + 1
    FROM Оборудование 
    WHERE Оборудование.[Тип оборудования] = @p
    
    -- Убираем последнюю запятую и пробел
    SET @subject_list = LEFT(@subject_list, LEN(@subject_list) - 1)
    
    -- Выводим отчет
    PRINT 'Станки '+ @p + ': ' + @subject_list
    
    -- Возвращаем количество 
    RETURN @count
    
END TRY
BEGIN CATCH
    PRINT 'Ошибка в параметрах'
    PRINT 'Сообщение: ' + ERROR_MESSAGE()
    RETURN 0
END CATCH
GO

exec TypeOfMashine @p = 'гончарное'

--6 
Create PROC EX6 @p CHAR(10)
AS
BEGIN TRY
    set transaction isolation level SERIALIZABLE;          
    begin tran
	insert into #Zk([Название оборудования],[Тип оборудования], [Дата поступления],	[Количество], [Подразделение установки])
	values('станок22', 'гончарное', '03-03-2222', '11', 'EFE');
	exec TypeOfMashine @p 
	select * from #Zk
	commit tran;
    
END TRY
BEGIN CATCH
    PRINT 'Ошибка в параметрах'
    
     if (@@trancount > 0) 
	 begin
	 rollback tran ; 
	 end;
     return -1;	

END CATCH
GO

exec Ex6 @p = 'гончарное'