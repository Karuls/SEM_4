use lab3

DECLARE @JobList NVARCHAR(MAX) = '';

DECLARE JobCursor CURSOR FOR
SELECT RTRIM(Название_должности) FROM Должность;

OPEN JobCursor;

DECLARE @JobName NVARCHAR(100);

FETCH NEXT FROM JobCursor INTO @JobName;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Формируем строку с должностями через запятую
    SET @JobList = @JobList + @JobName + ', ';
    FETCH NEXT FROM JobCursor INTO @JobName;
END

CLOSE JobCursor;
DEALLOCATE JobCursor;

-- Удаляем последнюю запятую
SET @JobList = LEFT(@JobList, LEN(@JobList) - 2);

SELECT @JobList AS JobTitles;
Go




USE lab3;

DECLARE @JobList NVARCHAR(MAX) = '';

DECLARE LocalCursor CURSOR LOCAL FOR
SELECT RTRIM(Название_должности) FROM Должность;

OPEN LocalCursor;

DECLARE @JobName NVARCHAR(100);

FETCH NEXT FROM LocalCursor INTO @JobName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @JobList = @JobList + @JobName + ', ';
    FETCH NEXT FROM LocalCursor INTO @JobName;
END

CLOSE LocalCursor;
DEALLOCATE LocalCursor;

SELECT 'Локальный курсор' AS CursorType, @JobList AS JobTitles;
Go





USE lab3;

DECLARE StaticCursor CURSOR STATIC FOR
SELECT ID_сотрудника, Фамилия_сотрудника FROM Сотрудник;

OPEN StaticCursor;

DECLARE @EmployeeID NVARCHAR(10), @EmployeeName NVARCHAR(100);

FETCH NEXT FROM StaticCursor INTO @EmployeeID, @EmployeeName;

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Статический курсор: ' + @EmployeeID + ' - ' + @EmployeeName;
    FETCH NEXT FROM StaticCursor INTO @EmployeeID, @EmployeeName;
END

CLOSE StaticCursor;
DEALLOCATE StaticCursor;
Go




USE lab3;

DECLARE DynamicCursor CURSOR DYNAMIC FOR
SELECT ID_сотрудника, Фамилия_сотрудника FROM Сотрудник;

OPEN DynamicCursor;

DECLARE @EmployeeID NVARCHAR(10), @EmployeeName NVARCHAR(100);

FETCH NEXT FROM DynamicCursor INTO @EmployeeID, @EmployeeName;

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Динамический курсор: ' + @EmployeeID + ' - ' + @EmployeeName;
    FETCH NEXT FROM DynamicCursor INTO @EmployeeID, @EmployeeName;
END

CLOSE DynamicCursor;
DEALLOCATE DynamicCursor;
Go






USE lab3;

DECLARE EmployeeCursor CURSOR SCROLL FOR
SELECT ID_сотрудника, Фамилия_сотрудника FROM Сотрудник;

OPEN EmployeeCursor;

DECLARE @EmployeeID NVARCHAR(10), @EmployeeName NVARCHAR(100);

-- Перемещение к первой строке
FETCH FIRST FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
PRINT 'FIRST: ' + @EmployeeID + ' - ' + @EmployeeName;

-- Перемещение к следующей строке
FETCH NEXT FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
PRINT 'NEXT: ' + @EmployeeID + ' - ' + @EmployeeName;

-- Перемещение к предыдущей строке
FETCH PRIOR FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
PRINT 'PRIOR: ' + @EmployeeID + ' - ' + @EmployeeName;

-- Перемещение к последней строке
FETCH LAST FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
PRINT 'LAST: ' + @EmployeeID + ' - ' + @EmployeeName;

-- Переход к 3-й строке (нумерация с 1)
FETCH ABSOLUTE 3 FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
PRINT 'ABSOLUTE 3: ' + @EmployeeID + ' - ' + @EmployeeName;

-- Перемещение на 2 строки вперед от текущей позиции
FETCH RELATIVE 2 FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
PRINT 'RELATIVE 2: ' + @EmployeeID + ' - ' + @EmployeeName;

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;
Go






DECLARE EmployeeCursor CURSOR SCROLL FOR
SELECT ID_сотрудника, Фамилия_сотрудника FROM Сотрудник WHERE ID_сотрудника > '005';

OPEN EmployeeCursor;

DECLARE @EmployeeID NVARCHAR(10), @EmployeeName NVARCHAR(100);

FETCH NEXT FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Обновляем текущую строку
    UPDATE Сотрудник
    SET Пол = 'Уволен'
    WHERE CURRENT OF EmployeeCursor;

    FETCH NEXT FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
END

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;

SELECT * FROM Сотрудник;  -- Проверяем изменения
Go


DECLARE @EmployeeID NVARCHAR(10), @EmployeeName NVARCHAR(100);

DECLARE EmployeeCursor CURSOR SCROLL FOR
SELECT ID_сотрудника, Фамилия_сотрудника FROM Сотрудник WHERE Пол = 'Уволен';

OPEN EmployeeCursor;

FETCH NEXT FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Удаляем текущую строку
    DELETE FROM Сотрудник WHERE CURRENT OF EmployeeCursor;

    FETCH NEXT FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
END

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;

SELECT * FROM Сотрудник;  -- Проверяем оставшиеся записи
Go