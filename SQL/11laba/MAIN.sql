use F_MyBase

--1

--Declare Cursor1 Cursor for Select Оборудование.[Название оборудования] from Оборудование;
Declare @temp_string char(20), @string_cursor char(200) = ''; 
open Cursor1
Fetch Cursor1 into @temp_string
print 'go!';
while @@FETCH_STATUS =0
begin
set @string_cursor = RTRIM(@temp_string) + '-' + @string_cursor;
Fetch Cursor1 into @temp_string
end;
print @string_cursor
close Cursor1;

--2

DECLARE LOCAL_CURSOR CURSOR LOCAL FOR
SELECT RTRIM(Ответственный.[Фамилия ответственного]) FROM Ответственный;

DECLARE @WorkersList NVARCHAR(MAX) = '';
DECLARE @WorkerName NVARCHAR(100);
OPEN LOCAL_CURSOR;


FETCH NEXT FROM LOCAL_CURSOR INTO @WorkerName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @WorkersList = @WorkersList + @WorkerName + ', ';
    FETCH NEXT FROM LOCAL_CURSOR INTO @WorkerName;
END

CLOSE LOCAL_CURSOR;
DEALLOCATE LOCAL_CURSOR;

SELECT 'Локальный курсор' AS CursorType, @WorkersList AS WorkersLastNames;
Go




DECLARE GLOBAL_CURSOR CURSOR GLOBAL FOR
SELECT RTRIM(Ответственный.[Фамилия ответственного]) FROM Ответственный;



OPEN GLOBAL_CURSOR;
DECLARE @WorkersListGlobal NVARCHAR(MAX) = '';
DECLARE @WorkerName NVARCHAR(100);
FETCH NEXT FROM GLOBAL_CURSOR INTO @WorkerName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @WorkersListGlobal = @WorkersListGlobal + @WorkerName + ', ';
    FETCH NEXT FROM GLOBAL_CURSOR INTO @WorkerName;
END

CLOSE GLOBAL_CURSOR;
deallocate GLOBAL_CURSOR;
SELECT 'Глобальный курсор' AS CursorType, @WorkersListGlobal AS WoorkersLactNames;


--3
-- Создаем статический курсор
DECLARE StaticCursor CURSOR STATIC FOR
SELECT Оборудование.[Название оборудования], Оборудование.Количество FROM Оборудование;

OPEN StaticCursor;

DECLARE @StanokCount nchar(10), @StanokName NVARCHAR(100);

FETCH NEXT FROM StaticCursor INTO @StanokCount, @StanokName;

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Статический курсор: ' + CAST(@StanokCount AS NVARCHAR) + ' - ' + @StanokName;
    FETCH NEXT FROM StaticCursor INTO @StanokCount, @StanokName;
END

CLOSE StaticCursor;
DEALLOCATE StaticCursor;
Go



-- Создаем динамический курсор
DECLARE DynamicCursor CURSOR STATIC FOR
SELECT Оборудование.[Название оборудования], Оборудование.Количество FROM Оборудование;

OPEN DynamicCursor;

DECLARE @StanokCount nchar(10), @StanokName NVARCHAR(100);

FETCH NEXT FROM DynamicCursor INTO @StanokCount, @StanokName;

INSERT INTO Temp_Table (Stanok, Count, Name) VALUES ('TEST11lab', '4', 'lexa')

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Динамический курсор: ' + CAST(@StanokCount AS NVARCHAR) + ' - ' + @StanokName;
    FETCH NEXT FROM DynamicCursor INTO @StanokCount, @StanokName;
END

CLOSE DynamicCursor;
DEALLOCATE DynamicCursor;

--4

DECLARE DeletedCursor CURSOR SCROLL FOR
SELECT Списанное.[Название оборудования], Списанное.[причина списания] FROM Списанное;

OPEN DeletedCursor;

DECLARE  @DeletedName nchar(20), @DeletedReason nchar(50);

-- Получаем первую запись
FETCH FIRST FROM DeletedCursor INTO @DeletedName, @DeletedReason;
PRINT 'FIRST: ' + CAST(Rtrim(@DeletedName)  AS NVARCHAR) + ' - ' + @DeletedReason;


FETCH NEXT FROM DeletedCursor INTO @DeletedName, @DeletedReason;
PRINT 'NEXT: ' + CAST(Rtrim(@DeletedName)  AS NVARCHAR) + ' - ' + @DeletedReason;

-- Переходим к предыдущей записи
FETCH PRIOR FROM DeletedCursor INTO @DeletedName, @DeletedReason;
PRINT 'PRIOR: ' + CAST(Rtrim(@DeletedName)  AS NVARCHAR) + ' - ' + @DeletedReason;

-- Переходим к последней записи
FETCH LAST FROM DeletedCursor INTO @DeletedName, @DeletedReason;
PRINT 'LAST: ' + CAST(Rtrim(@DeletedName)  AS NVARCHAR) + ' - ' + @DeletedReason;


FETCH ABSOLUTE 3 FROM DeletedCursor INTO @DeletedName, @DeletedReason;
PRINT 'ABSOLUTE 3: ' +CAST(Rtrim(@DeletedName)  AS NVARCHAR) + ' - ' + @DeletedReason;


FETCH RELATIVE 2 FROM DeletedCursor INTO @DeletedName, @DeletedReason;
PRINT 'RELATIVE 2: ' +CAST(Rtrim(@DeletedName)  AS NVARCHAR) + ' - ' + @DeletedReason;

CLOSE DeletedCursor;
DEALLOCATE DeletedCursor;

--5

DECLARE EmployeeCursor CURSOR DYNAMIC  FOR
SELECT Списанное.[Название оборудования], Списанное.[Количество списания] FROM Списанное WHERE Списанное.[Количество списания] > '1';

OPEN EmployeeCursor;

DECLARE @EmployeeName NVARCHAR(10), @EmployeeCount int;

FETCH NEXT FROM EmployeeCursor INTO @EmployeeName, @EmployeeCount;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Обновляем текущую строку
    UPDATE Списанное
    SET Списанное.[Фамилия ответственного] = 'Уволен'
    WHERE CURRENT OF EmployeeCursor;

    FETCH NEXT FROM EmployeeCursor INTO @EmployeeName, @EmployeeCount;
END

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;

SELECT * FROM Списанное;  -- Проверяем изменения
Go

--6

DECLARE @EmployeeName NVARCHAR(10), @EmployeeCount int;

DECLARE EmployeeCursor CURSOR Dynamic FOR
SELECT Списанное.[Название оборудования], Списанное.[Количество списания] FROM Списанное WHERE Списанное.[Количество списания] = 1;

OPEN EmployeeCursor;

FETCH NEXT FROM EmployeeCursor INTO @EmployeeName, @EmployeeCount;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Удаляем текущую строку
    DELETE FROM Списанное WHERE CURRENT OF EmployeeCursor;

    FETCH NEXT FROM EmployeeCursor INTO @EmployeeName, @EmployeeCount;
END

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;

SELECT * FROM Списанное;  -- Проверяем оставшиеся записи
Go