
INSERT INTO FACULTY (FACULTY, FACULTY_NAME)
VALUES 
    ('F001', '��������� ����������'),
    ('F002', '��������� ������'),
    ('F003', '��������� �����������');

	INSERT INTO PROFESSION (PROFESSION, FACULTY, PROFESSION_NAME, QUALIFICATION)
VALUES 
    ('P001', 'F001', '���������� ����������', '���������-�������������'),
    ('P002', 'F002', '������� ������', '�����-�������'),
    ('P003', 'F003', '����������� ���������', '�����������');

	INSERT INTO PULPIT (PULPIT, PULPIT_NAME, FACULTY)
VALUES 
    ('PU001', '������� ������ ����������', 'F001'),
    ('PU002', '������� ������������� ������', 'F002'),
    ('PU003', '������� ����������� ���������', 'F003');

	INSERT INTO TEACHER (TEACHER, TEACHER_NAME, GENDER, PULPIT)
VALUES 
    ('T001', '������ ���� ��������', '�', 'PU001'),
    ('T002', '������ ���� ��������', '�', 'PU002'),
    ('T003', '�������� ���� ���������', '�', 'PU003');

	INSERT INTO SUBJECT (SUBJECT, SUBJECT_NAME, PULPIT)
VALUES 
    ('S001', '���������������� ���������', 'PU001'),
    ('S002', '��������� ��������', 'PU002'),
    ('S003', '��������� � ��������� ������', 'PU003');


INSERT INTO AUDITORIUM_TYPE (AUDITORIUM_TYPE, AUDITORIUM_TYPENAME)
VALUES
    ('LECTURE', '������'),
    ('LAB', '�����������'),
    ('SEMINAR', '�������'),
    ('CONFERENCE', '���������-���'),
    ('WORKSHOP', '����������');

INSERT INTO AUDITORIUM (AUDITORIUM, AUDITORIUM_TYPE, AUDITORIUM_CAPACITY, AUDITORIUM_NAME)
VALUES
    ('A101', 'LECTURE', 100, '������� ���������� ���������'),
    ('A102', 'LAB', 30, '������������ �����'),
    ('A103', 'SEMINAR', 50, '����������� ���������'),
    ('A104', 'CONFERENCE', 200, '���������-���'),
    ('A105', 'WORKSHOP', 40, '����������');



	INSERT INTO [GROUP] (IDGROUP, FACULTY, PROFESSION, YEAR_FIRST)
VALUES 
    (1, 'F001', 'P001', 2020),
    (2, 'F002', 'P002', 2021),
    (3, 'F003', 'P003', 2022);

	INSERT INTO STUDENT (IDGROUP, NAME, BDAY, INFO)
VALUES 
    (1, '������� �������', '2002-03-14', '<��������>����������</��������>'),
    (2, '����� �������', '2003-05-20', '<��������>������</��������>'),
    (3, '������� ��������', '2004-11-01', '<��������>����������������</��������>');

	INSERT INTO PROGRESS (SUBJECT, IDSTUDENT, PDATE, NOTE)
VALUES 
    ('S001', 1000, '2023-01-15', 8),
    ('S002', 1001, '2023-02-20', 9),
    ('S003', 1002, '2023-03-10', 10);
GO
