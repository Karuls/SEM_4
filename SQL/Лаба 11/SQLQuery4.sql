DECLARE StudentCursor CURSOR SCROLL FOR
SELECT Student.IDSTUDENT, Student.Name FROM STUDENT;

OPEN StudentCursor;

DECLARE @StudentID INT, @StudentName NVARCHAR(100);

-- Получаем первую запись
FETCH FIRST FROM StudentCursor INTO @StudentID, @StudentName;
PRINT 'FIRST: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;

-- Переходим к следующей записи
FETCH NEXT FROM StudentCursor INTO @StudentID, @StudentName;
PRINT 'NEXT: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;

-- Переходим к предыдущей записи
FETCH PRIOR FROM StudentCursor INTO @StudentID, @StudentName;
PRINT 'PRIOR: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;

-- Переходим к последней записи
FETCH LAST FROM StudentCursor INTO @StudentID, @StudentName;
PRINT 'LAST: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;

-- Переходим к 3-й записи
FETCH ABSOLUTE 3 FROM StudentCursor INTO @StudentID, @StudentName;
PRINT 'ABSOLUTE 3: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;

-- Переходим на 2 строки вперёд от текущей позиции
FETCH RELATIVE 2 FROM StudentCursor INTO @StudentID, @StudentName;
PRINT 'RELATIVE 2: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;

CLOSE StudentCursor;
DEALLOCATE StudentCursor;
