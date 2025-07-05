-- Изменяем процедуру PSUBJECT без выходного параметра
ALTER PROCEDURE PSUBJECT @p VARCHAR(20) = NULL
AS 
BEGIN
	SET NOCOUNT ON;
	
	-- Формируем результирующий набор для всех кафедр, кроме заданной
	SELECT s.SUBJECT AS Код, 
		   s.SUBJECT_NAME AS Дисциплина, 
		   s.PULPIT AS Кафедра 
	FROM SUBJECT s
	WHERE s.PULPIT != @p OR @p IS NULL
END
GO

-- Создаем временную локальную таблицу
CREATE TABLE #SUBJECTS
(
	Код VARCHAR(10) PRIMARY KEY,
	Дисциплина VARCHAR(50) NOT NULL,
	Кафедра VARCHAR(10) NOT NULL
)

-- Заполняем временную таблицу с помощью INSERT...EXECUTE
INSERT INTO #SUBJECTS 
EXEC PSUBJECT @p = 'ИСиТ'

-- Проверяем содержимое временной таблицы
SELECT * FROM #SUBJECTS
PRINT 'Количество записей во временной таблице: ' + CAST(@@ROWCOUNT AS VARCHAR(10))

-- Очистка
DROP TABLE #SUBJECTS
GO