CREATE TABLE #STUDENT (
    StudentID INT PRIMARY KEY,
    StudentName NVARCHAR(100),
    Status NVARCHAR(50) DEFAULT 'Active'
);

INSERT INTO #STUDENT (StudentID, StudentName, Status) VALUES
(1, '���� ������', 'Active'),
(2, '���� ��������', 'Active'),
(3, '����� ��������', 'Inactive'),
(4, '��������� ������', 'Active'),
(5, '������� �������', 'Graduated'),
(6, '����� �������', 'Active'),
(7, '������� �������', 'Inactive'),
(8, '������� ������', 'Active');


DECLARE StudentCursor CURSOR SCROLL FOR
SELECT StudentID, StudentName FROM #STUDENT WHERE StudentID > 5;

OPEN StudentCursor;

DECLARE @StudentID INT, @StudentName NVARCHAR(100);

FETCH NEXT FROM StudentCursor INTO @StudentID, @StudentName;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- ��������� ������� ������ � �������
    UPDATE #STUDENT
    SET Status = 'Graduated'
    WHERE CURRENT OF StudentCursor;

    FETCH NEXT FROM StudentCursor INTO @StudentID, @StudentName;
END

CLOSE StudentCursor;
DEALLOCATE StudentCursor;

SELECT * FROM #STUDENT;  -- ��������� ���������




DECLARE StudentCursor CURSOR SCROLL FOR
SELECT StudentID, StudentName FROM #STUDENT WHERE Status = 'Inactive';

OPEN StudentCursor;

FETCH NEXT FROM StudentCursor INTO @StudentID, @StudentName;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- ������� ������� ������ �������
    DELETE FROM #STUDENT WHERE CURRENT OF StudentCursor;

    FETCH NEXT FROM StudentCursor INTO @StudentID, @StudentName;
END

CLOSE StudentCursor;
DEALLOCATE StudentCursor;

SELECT * FROM #STUDENT; -- ��������� ���������� ������
drop table #Student