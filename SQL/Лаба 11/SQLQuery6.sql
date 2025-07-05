-- Объявление переменных для курсора
DECLARE @Subject CHAR(10)
DECLARE @IdStudent INT
DECLARE @PDate DATE
DECLARE @Note INT
DECLARE @StudentName NVARCHAR(100)
DECLARE @GroupId INT
DECLARE @FacultyName VARCHAR(50)

-- Создание курсора для выборки плохих оценок с информацией о студентах
DECLARE delete_cursor CURSOR FOR
    SELECT 
        p.SUBJECT, 
        p.IDSTUDENT, 
        p.PDATE, 
        p.NOTE,
        s.NAME,
        s.IDGROUP,
        f.FACULTY_NAME
    FROM 
        PROGRESS p
        JOIN STUDENT s ON p.IDSTUDENT = s.IDSTUDENT
        JOIN GROUPS g ON s.IDGROUP = g.IDGROUP
        JOIN FACULTY f ON g.FACULTY = f.FACULTY
    WHERE 
        p.NOTE < 4

-- Открытие курсора
OPEN delete_cursor

-- Получение первой записи
FETCH NEXT FROM delete_cursor INTO @Subject, @IdStudent, @PDate, @Note, @StudentName, @GroupId, @FacultyName

-- Цикл обработки записей
WHILE @@FETCH_STATUS = 0
BEGIN
    -- Удаление записи
    DELETE FROM PROGRESS
    WHERE SUBJECT = @Subject
    AND IDSTUDENT = @IdStudent
    AND PDATE = @PDate
    
    -- Вывод информации об удаленной записи
    PRINT CONCAT('Удалена оценка ', @Note, ' для студента ', @StudentName, 
                ' (ID:', @IdStudent, ', Группа:', @GroupId, ', Факультет:', @FacultyName, 
                ') по предмету ', @Subject, ' от ', CONVERT(VARCHAR, @PDate, 104))
    
    -- Получение следующей записи
    FETCH NEXT FROM delete_cursor INTO @Subject, @IdStudent, @PDate, @Note, @StudentName, @GroupId, @FacultyName
END

-- Закрытие и освобождение курсора
CLOSE delete_cursor
DEALLOCATE delete_cursor

select * from Progress

PRINT 'Удаление оценок ниже 4 завершено'












-- Объявление переменных
DECLARE @StudentId INT = 1005  -- Конкретный ID студента
DECLARE @Subj CHAR(10)
DECLARE @StudId INT
DECLARE @ExamDate DATE
DECLARE @Grade INT
DECLARE @SubjName VARCHAR(100)

-- Создание курсора для выборки оценок студента
DECLARE update_cursor CURSOR FOR
    SELECT 
        p.SUBJECT,
        p.IDSTUDENT,
        p.PDATE,
        p.NOTE,
        s.SUBJECT_NAME
    FROM 
        PROGRESS p
        JOIN SUBJECT s ON p.SUBJECT = s.SUBJECT
    WHERE 
        p.IDSTUDENT = @StudentId
        AND p.NOTE < 10  -- Не увеличиваем если уже 10

-- Открытие курсора
OPEN update_cursor

-- Получение первой записи
FETCH NEXT FROM update_cursor INTO @Subj, @StudId, @ExamDate, @Grade, @SubjName

-- Цикл обработки записей
WHILE @@FETCH_STATUS = 0
BEGIN
    -- Обновление оценки
    UPDATE PROGRESS
    SET NOTE = @Grade + 1
    WHERE SUBJECT = @Subj
    AND IDSTUDENT = @StudId
    AND PDATE = @ExamDate
    
    -- Вывод информации об изменении
    PRINT CONCAT('Студент ID:', @StudId, 
                ' - оценка по предмету "', @SubjName, 
                '" увеличена с ', @Grade, ' до ', @Grade + 1)
    
    -- Получение следующей записи
    FETCH NEXT FROM update_cursor INTO @Subj, @StudId, @ExamDate, @Grade, @SubjName
END

-- Закрытие и освобождение курсора
CLOSE update_cursor
DEALLOCATE update_cursor

select * from Progress

PRINT CONCAT('Обновление оценок для студента ID:', @StudentId, ' завершено')