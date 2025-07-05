
INSERT INTO FACULTY (FACULTY, FACULTY_NAME)
VALUES 
    ('F001', 'Факультет математики'),
    ('F002', 'Факультет физики'),
    ('F003', 'Факультет информатики');

	INSERT INTO PROFESSION (PROFESSION, FACULTY, PROFESSION_NAME, QUALIFICATION)
VALUES 
    ('P001', 'F001', 'Прикладная математика', 'Математик-исследователь'),
    ('P002', 'F002', 'Ядерная физика', 'Физик-ядерщик'),
    ('P003', 'F003', 'Программная инженерия', 'Программист');

	INSERT INTO PULPIT (PULPIT, PULPIT_NAME, FACULTY)
VALUES 
    ('PU001', 'Кафедра высшей математики', 'F001'),
    ('PU002', 'Кафедра теоретической физики', 'F002'),
    ('PU003', 'Кафедра программной инженерии', 'F003');

	INSERT INTO TEACHER (TEACHER, TEACHER_NAME, GENDER, PULPIT)
VALUES 
    ('T001', 'Иванов Иван Иванович', 'м', 'PU001'),
    ('T002', 'Петров Петр Петрович', 'м', 'PU002'),
    ('T003', 'Смирнова Анна Сергеевна', 'ж', 'PU003');

	INSERT INTO SUBJECT (SUBJECT, SUBJECT_NAME, PULPIT)
VALUES 
    ('S001', 'Дифференциальные уравнения', 'PU001'),
    ('S002', 'Квантовая механика', 'PU002'),
    ('S003', 'Алгоритмы и структуры данных', 'PU003');


INSERT INTO AUDITORIUM_TYPE (AUDITORIUM_TYPE, AUDITORIUM_TYPENAME)
VALUES
    ('LECTURE', 'Лекция'),
    ('LAB', 'Лаборатория'),
    ('SEMINAR', 'Семинар'),
    ('CONFERENCE', 'Конференц-зал'),
    ('WORKSHOP', 'Мастерская');

INSERT INTO AUDITORIUM (AUDITORIUM, AUDITORIUM_TYPE, AUDITORIUM_CAPACITY, AUDITORIUM_NAME)
VALUES
    ('A101', 'LECTURE', 100, 'Большая лекционная аудитория'),
    ('A102', 'LAB', 30, 'Компьютерный класс'),
    ('A103', 'SEMINAR', 50, 'Семинарская аудитория'),
    ('A104', 'CONFERENCE', 200, 'Конференц-зал'),
    ('A105', 'WORKSHOP', 40, 'Мастерская');



	INSERT INTO [GROUP] (IDGROUP, FACULTY, PROFESSION, YEAR_FIRST)
VALUES 
    (1, 'F001', 'P001', 2020),
    (2, 'F002', 'P002', 2021),
    (3, 'F003', 'P003', 2022);

	INSERT INTO STUDENT (IDGROUP, NAME, BDAY, INFO)
VALUES 
    (1, 'Алексей Смирнов', '2002-03-14', '<Интересы>Математика</Интересы>'),
    (2, 'Мария Иванова', '2003-05-20', '<Интересы>Физика</Интересы>'),
    (3, 'Дмитрий Кузнецов', '2004-11-01', '<Интересы>Программирование</Интересы>');

	INSERT INTO PROGRESS (SUBJECT, IDSTUDENT, PDATE, NOTE)
VALUES 
    ('S001', 1000, '2023-01-15', 8),
    ('S002', 1001, '2023-02-20', 9),
    ('S003', 1002, '2023-03-10', 10);
GO
