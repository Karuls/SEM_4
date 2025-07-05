-- ���������� ���������� ��� �������
DECLARE @Subject CHAR(10)
DECLARE @IdStudent INT
DECLARE @PDate DATE
DECLARE @Note INT
DECLARE @StudentName NVARCHAR(100)
DECLARE @GroupId INT
DECLARE @FacultyName VARCHAR(50)

-- �������� ������� ��� ������� ������ ������ � ����������� � ���������
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

-- �������� �������
OPEN delete_cursor

-- ��������� ������ ������
FETCH NEXT FROM delete_cursor INTO @Subject, @IdStudent, @PDate, @Note, @StudentName, @GroupId, @FacultyName

-- ���� ��������� �������
WHILE @@FETCH_STATUS = 0
BEGIN
    -- �������� ������
    DELETE FROM PROGRESS
    WHERE SUBJECT = @Subject
    AND IDSTUDENT = @IdStudent
    AND PDATE = @PDate
    
    -- ����� ���������� �� ��������� ������
    PRINT CONCAT('������� ������ ', @Note, ' ��� �������� ', @StudentName, 
                ' (ID:', @IdStudent, ', ������:', @GroupId, ', ���������:', @FacultyName, 
                ') �� �������� ', @Subject, ' �� ', CONVERT(VARCHAR, @PDate, 104))
    
    -- ��������� ��������� ������
    FETCH NEXT FROM delete_cursor INTO @Subject, @IdStudent, @PDate, @Note, @StudentName, @GroupId, @FacultyName
END

-- �������� � ������������ �������
CLOSE delete_cursor
DEALLOCATE delete_cursor

select * from Progress

PRINT '�������� ������ ���� 4 ���������'












-- ���������� ����������
DECLARE @StudentId INT = 1005  -- ���������� ID ��������
DECLARE @Subj CHAR(10)
DECLARE @StudId INT
DECLARE @ExamDate DATE
DECLARE @Grade INT
DECLARE @SubjName VARCHAR(100)

-- �������� ������� ��� ������� ������ ��������
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
        AND p.NOTE < 10  -- �� ����������� ���� ��� 10

-- �������� �������
OPEN update_cursor

-- ��������� ������ ������
FETCH NEXT FROM update_cursor INTO @Subj, @StudId, @ExamDate, @Grade, @SubjName

-- ���� ��������� �������
WHILE @@FETCH_STATUS = 0
BEGIN
    -- ���������� ������
    UPDATE PROGRESS
    SET NOTE = @Grade + 1
    WHERE SUBJECT = @Subj
    AND IDSTUDENT = @StudId
    AND PDATE = @ExamDate
    
    -- ����� ���������� �� ���������
    PRINT CONCAT('������� ID:', @StudId, 
                ' - ������ �� �������� "', @SubjName, 
                '" ��������� � ', @Grade, ' �� ', @Grade + 1)
    
    -- ��������� ��������� ������
    FETCH NEXT FROM update_cursor INTO @Subj, @StudId, @ExamDate, @Grade, @SubjName
END

-- �������� � ������������ �������
CLOSE update_cursor
DEALLOCATE update_cursor

select * from Progress

PRINT CONCAT('���������� ������ ��� �������� ID:', @StudentId, ' ���������')