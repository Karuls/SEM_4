use lab3

DECLARE @JobList NVARCHAR(MAX) = '';

DECLARE JobCursor CURSOR FOR
SELECT RTRIM(��������_���������) FROM ���������;

OPEN JobCursor;

DECLARE @JobName NVARCHAR(100);

FETCH NEXT FROM JobCursor INTO @JobName;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- ��������� ������ � ����������� ����� �������
    SET @JobList = @JobList + @JobName + ', ';
    FETCH NEXT FROM JobCursor INTO @JobName;
END

CLOSE JobCursor;
DEALLOCATE JobCursor;

-- ������� ��������� �������
SET @JobList = LEFT(@JobList, LEN(@JobList) - 2);

SELECT @JobList AS JobTitles;
Go




USE lab3;

DECLARE @JobList NVARCHAR(MAX) = '';

DECLARE LocalCursor CURSOR LOCAL FOR
SELECT RTRIM(��������_���������) FROM ���������;

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

SELECT '��������� ������' AS CursorType, @JobList AS JobTitles;
Go





USE lab3;

DECLARE StaticCursor CURSOR STATIC FOR
SELECT ID_����������, �������_���������� FROM ���������;

OPEN StaticCursor;

DECLARE @EmployeeID NVARCHAR(10), @EmployeeName NVARCHAR(100);

FETCH NEXT FROM StaticCursor INTO @EmployeeID, @EmployeeName;

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT '����������� ������: ' + @EmployeeID + ' - ' + @EmployeeName;
    FETCH NEXT FROM StaticCursor INTO @EmployeeID, @EmployeeName;
END

CLOSE StaticCursor;
DEALLOCATE StaticCursor;
Go




USE lab3;

DECLARE DynamicCursor CURSOR DYNAMIC FOR
SELECT ID_����������, �������_���������� FROM ���������;

OPEN DynamicCursor;

DECLARE @EmployeeID NVARCHAR(10), @EmployeeName NVARCHAR(100);

FETCH NEXT FROM DynamicCursor INTO @EmployeeID, @EmployeeName;

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT '������������ ������: ' + @EmployeeID + ' - ' + @EmployeeName;
    FETCH NEXT FROM DynamicCursor INTO @EmployeeID, @EmployeeName;
END

CLOSE DynamicCursor;
DEALLOCATE DynamicCursor;
Go






USE lab3;

DECLARE EmployeeCursor CURSOR SCROLL FOR
SELECT ID_����������, �������_���������� FROM ���������;

OPEN EmployeeCursor;

DECLARE @EmployeeID NVARCHAR(10), @EmployeeName NVARCHAR(100);

-- ����������� � ������ ������
FETCH FIRST FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
PRINT 'FIRST: ' + @EmployeeID + ' - ' + @EmployeeName;

-- ����������� � ��������� ������
FETCH NEXT FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
PRINT 'NEXT: ' + @EmployeeID + ' - ' + @EmployeeName;

-- ����������� � ���������� ������
FETCH PRIOR FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
PRINT 'PRIOR: ' + @EmployeeID + ' - ' + @EmployeeName;

-- ����������� � ��������� ������
FETCH LAST FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
PRINT 'LAST: ' + @EmployeeID + ' - ' + @EmployeeName;

-- ������� � 3-� ������ (��������� � 1)
FETCH ABSOLUTE 3 FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
PRINT 'ABSOLUTE 3: ' + @EmployeeID + ' - ' + @EmployeeName;

-- ����������� �� 2 ������ ������ �� ������� �������
FETCH RELATIVE 2 FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
PRINT 'RELATIVE 2: ' + @EmployeeID + ' - ' + @EmployeeName;

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;
Go






DECLARE EmployeeCursor CURSOR SCROLL FOR
SELECT ID_����������, �������_���������� FROM ��������� WHERE ID_���������� > '005';

OPEN EmployeeCursor;

DECLARE @EmployeeID NVARCHAR(10), @EmployeeName NVARCHAR(100);

FETCH NEXT FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- ��������� ������� ������
    UPDATE ���������
    SET ��� = '������'
    WHERE CURRENT OF EmployeeCursor;

    FETCH NEXT FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
END

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;

SELECT * FROM ���������;  -- ��������� ���������
Go


DECLARE @EmployeeID NVARCHAR(10), @EmployeeName NVARCHAR(100);

DECLARE EmployeeCursor CURSOR SCROLL FOR
SELECT ID_����������, �������_���������� FROM ��������� WHERE ��� = '������';

OPEN EmployeeCursor;

FETCH NEXT FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- ������� ������� ������
    DELETE FROM ��������� WHERE CURRENT OF EmployeeCursor;

    FETCH NEXT FROM EmployeeCursor INTO @EmployeeID, @EmployeeName;
END

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;

SELECT * FROM ���������;  -- ��������� ���������� ������
Go