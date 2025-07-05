USE UNIVER
GO

CREATE PROC SUBJECT_REPORT @p CHAR(10)
AS
BEGIN TRY
    -- Проверяем существование дисциплин для кафедры
    IF NOT EXISTS (SELECT 1 FROM SUBJECT WHERE PULPIT = @p)
        RAISERROR('Ошибка в параметрах', 16, 1)
    
    -- Объявляем переменные
    DECLARE @subject_list VARCHAR(500) = ''
    DECLARE @count INT = 0
    
    -- Формируем строку с дисциплинами через запятую
    SELECT @subject_list = @subject_list + RTRIM(SUBJECT) + ', ',
           @count = @count + 1
    FROM SUBJECT 
    WHERE PULPIT = @p
    
    -- Убираем последнюю запятую и пробел
    SET @subject_list = LEFT(@subject_list, LEN(@subject_list) - 1)
    
    -- Выводим отчет
    PRINT 'Дисциплины кафедры ' + @p + ': ' + @subject_list
    
    -- Возвращаем количество дисциплин
    RETURN @count
    
END TRY
BEGIN CATCH
    PRINT 'Ошибка в параметрах'
    PRINT 'Сообщение: ' + ERROR_MESSAGE()
    RETURN 0
END CATCH
GO