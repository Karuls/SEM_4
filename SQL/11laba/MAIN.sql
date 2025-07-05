use F_MyBase

--1

--Declare Cursor1 Cursor for Select ������������.[�������� ������������] from ������������;
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
SELECT RTRIM(�������������.[������� ��������������]) FROM �������������;

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

SELECT '��������� ������' AS CursorType, @WorkersList AS WorkersLastNames;
Go




DECLARE GLOBAL_CURSOR CURSOR GLOBAL FOR
SELECT RTRIM(�������������.[������� ��������������]) FROM �������������;



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
SELECT '���������� ������' AS CursorType, @WorkersListGlobal AS WoorkersLactNames;


--3
-- ������� ����������� ������
DECLARE StaticCursor CURSOR STATIC FOR
SELECT ������������.[�������� ������������], ������������.���������� FROM ������������;

OPEN StaticCursor;

DECLARE @StanokCount nchar(10), @StanokName NVARCHAR(100);

FETCH NEXT FROM StaticCursor INTO @StanokCount, @StanokName;

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT '����������� ������: ' + CAST(@StanokCount AS NVARCHAR) + ' - ' + @StanokName;
    FETCH NEXT FROM StaticCursor INTO @StanokCount, @StanokName;
END

CLOSE StaticCursor;
DEALLOCATE StaticCursor;
Go



-- ������� ������������ ������
DECLARE DynamicCursor CURSOR STATIC FOR
SELECT ������������.[�������� ������������], ������������.���������� FROM ������������;

OPEN DynamicCursor;

DECLARE @StanokCount nchar(10), @StanokName NVARCHAR(100);

FETCH NEXT FROM DynamicCursor INTO @StanokCount, @StanokName;

INSERT INTO Temp_Table (Stanok, Count, Name) VALUES ('TEST11lab', '4', 'lexa')

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT '������������ ������: ' + CAST(@StanokCount AS NVARCHAR) + ' - ' + @StanokName;
    FETCH NEXT FROM DynamicCursor INTO @StanokCount, @StanokName;
END

CLOSE DynamicCursor;
DEALLOCATE DynamicCursor;

--4

DECLARE DeletedCursor CURSOR SCROLL FOR
SELECT ���������.[�������� ������������], ���������.[������� ��������] FROM ���������;

OPEN DeletedCursor;

DECLARE  @DeletedName nchar(20), @DeletedReason nchar(50);

-- �������� ������ ������
FETCH FIRST FROM DeletedCursor INTO @DeletedName, @DeletedReason;
PRINT 'FIRST: ' + CAST(Rtrim(@DeletedName)  AS NVARCHAR) + ' - ' + @DeletedReason;


FETCH NEXT FROM DeletedCursor INTO @DeletedName, @DeletedReason;
PRINT 'NEXT: ' + CAST(Rtrim(@DeletedName)  AS NVARCHAR) + ' - ' + @DeletedReason;

-- ��������� � ���������� ������
FETCH PRIOR FROM DeletedCursor INTO @DeletedName, @DeletedReason;
PRINT 'PRIOR: ' + CAST(Rtrim(@DeletedName)  AS NVARCHAR) + ' - ' + @DeletedReason;

-- ��������� � ��������� ������
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
SELECT ���������.[�������� ������������], ���������.[���������� ��������] FROM ��������� WHERE ���������.[���������� ��������] > '1';

OPEN EmployeeCursor;

DECLARE @EmployeeName NVARCHAR(10), @EmployeeCount int;

FETCH NEXT FROM EmployeeCursor INTO @EmployeeName, @EmployeeCount;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- ��������� ������� ������
    UPDATE ���������
    SET ���������.[������� ��������������] = '������'
    WHERE CURRENT OF EmployeeCursor;

    FETCH NEXT FROM EmployeeCursor INTO @EmployeeName, @EmployeeCount;
END

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;

SELECT * FROM ���������;  -- ��������� ���������
Go

--6

DECLARE @EmployeeName NVARCHAR(10), @EmployeeCount int;

DECLARE EmployeeCursor CURSOR Dynamic FOR
SELECT ���������.[�������� ������������], ���������.[���������� ��������] FROM ��������� WHERE ���������.[���������� ��������] = 1;

OPEN EmployeeCursor;

FETCH NEXT FROM EmployeeCursor INTO @EmployeeName, @EmployeeCount;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- ������� ������� ������
    DELETE FROM ��������� WHERE CURRENT OF EmployeeCursor;

    FETCH NEXT FROM EmployeeCursor INTO @EmployeeName, @EmployeeCount;
END

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;

SELECT * FROM ���������;  -- ��������� ���������� ������
Go