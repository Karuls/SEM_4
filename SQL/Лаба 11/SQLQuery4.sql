DECLARE StudentCursor CURSOR SCROLL FOR
SELECT Student.IDSTUDENT, Student.Name FROM STUDENT;

OPEN StudentCursor;

DECLARE @StudentID INT, @StudentName NVARCHAR(100);

-- �������� ������ ������
FETCH FIRST FROM StudentCursor INTO @StudentID, @StudentName;
PRINT 'FIRST: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;

-- ��������� � ��������� ������
FETCH NEXT FROM StudentCursor INTO @StudentID, @StudentName;
PRINT 'NEXT: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;

-- ��������� � ���������� ������
FETCH PRIOR FROM StudentCursor INTO @StudentID, @StudentName;
PRINT 'PRIOR: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;

-- ��������� � ��������� ������
FETCH LAST FROM StudentCursor INTO @StudentID, @StudentName;
PRINT 'LAST: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;

-- ��������� � 3-� ������
FETCH ABSOLUTE 3 FROM StudentCursor INTO @StudentID, @StudentName;
PRINT 'ABSOLUTE 3: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;

-- ��������� �� 2 ������ ����� �� ������� �������
FETCH RELATIVE 2 FROM StudentCursor INTO @StudentID, @StudentName;
PRINT 'RELATIVE 2: ' + CAST(@StudentID AS NVARCHAR) + ' - ' + @StudentName;

CLOSE StudentCursor;
DEALLOCATE StudentCursor;
