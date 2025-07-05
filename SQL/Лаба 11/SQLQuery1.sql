use master

DECLARE @DisciplineList NVARCHAR(MAX) = '';

DECLARE DisciplineCursor CURSOR FOR
SELECT RTRIM(SUBJECT_NAME) FROM SUBJECT;

OPEN DisciplineCursor;

DECLARE @SubjectName NVARCHAR(100);

FETCH NEXT FROM DisciplineCursor INTO @SubjectName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @DisciplineList = @DisciplineList + @SubjectName + ', ';
    FETCH NEXT FROM DisciplineCursor INTO @SubjectName;
END

CLOSE DisciplineCursor;
DEALLOCATE DisciplineCursor;

-- Удаляем последнюю запятую
SET @DisciplineList = LEFT(@DisciplineList, LEN(@DisciplineList) - 2);

SELECT @DisciplineList AS DisciplineNames;
