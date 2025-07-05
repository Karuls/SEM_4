CREATE TABLE #STUDENT (
    StudentID INT PRIMARY KEY,
    StudentName NVARCHAR(100),
    Status NVARCHAR(50) DEFAULT 'Active'
);

INSERT INTO #STUDENT (StudentID, StudentName, Status) VALUES
(1, 'Иван Петров', 'Active'),
(2, 'Анна Смирнова', 'Active'),
(3, 'Павел Кузнецов', 'Inactive'),
(4, 'Екатерина Орлова', 'Active'),
(5, 'Дмитрий Соколов', 'Graduated'),
(6, 'Мария Иванова', 'Active'),
(7, 'Алексей Федоров', 'Inactive'),
(8, 'Наталья Белова', 'Active');


DECLARE StudentCursor CURSOR SCROLL FOR
SELECT StudentID, StudentName FROM #STUDENT WHERE StudentID > 5;

OPEN StudentCursor;

DECLARE @StudentID INT, @StudentName NVARCHAR(100);

FETCH NEXT FROM StudentCursor INTO @StudentID, @StudentName;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Обновляем текущую строку в курсоре
    UPDATE #STUDENT
    SET Status = 'Graduated'
    WHERE CURRENT OF StudentCursor;

    FETCH NEXT FROM StudentCursor INTO @StudentID, @StudentName;
END

CLOSE StudentCursor;
DEALLOCATE StudentCursor;

SELECT * FROM #STUDENT;  -- Проверяем изменения




DECLARE StudentCursor CURSOR SCROLL FOR
SELECT StudentID, StudentName FROM #STUDENT WHERE Status = 'Inactive';

OPEN StudentCursor;

FETCH NEXT FROM StudentCursor INTO @StudentID, @StudentName;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Удаляем текущую строку курсора
    DELETE FROM #STUDENT WHERE CURRENT OF StudentCursor;

    FETCH NEXT FROM StudentCursor INTO @StudentID, @StudentName;
END

CLOSE StudentCursor;
DEALLOCATE StudentCursor;

SELECT * FROM #STUDENT; -- Проверяем оставшиеся записи
drop table #Student