-- Создаем статический курсор
DECLARE StaticCursor CURSOR STATIC FOR
SELECT STUDENT.IDSTUDENT, Name FROM STUDENT;

OPEN StaticCursor;

DECLARE @StudentID INT, @StudentName NVARCHAR(100);

FETCH NEXT FROM StaticCursor INTO @StudentID, @StudentName;

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Статический курсор: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;
    FETCH NEXT FROM StaticCursor INTO @StudentID, @StudentName;
END

CLOSE StaticCursor;
DEALLOCATE StaticCursor;
Go



-- Создаем динамический курсор
DECLARE DynamicCursor CURSOR DYNAMIC FOR
SELECT Student.IDSTUDENT, Student.NAME FROM STUDENT;

OPEN DynamicCursor;

DECLARE @StudentID INT, @StudentName NVARCHAR(100);

FETCH NEXT FROM DynamicCursor INTO @StudentID, @StudentName;

INSERT INTO STUDENT (IDGROUP, NAME, BDAY) VALUES (5, 'Сильвестер Валерий Иванович', '1994-07-12')

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Динамический курсор: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;
    FETCH NEXT FROM DynamicCursor INTO @StudentID, @StudentName;
END

CLOSE DynamicCursor;
DEALLOCATE DynamicCursor;
