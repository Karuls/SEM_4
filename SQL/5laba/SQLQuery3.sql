use UNIVER
SELECT FACULTY.FACULTY_NAME, PULPIT.PULPIT_NAME, PULPIT.FACULTY
FROM FACULTY Inner Join PULPIT
ON FACULTY.FACULTY = PULPIT.FACULTY
Inner Join PROFESSION
ON PULPIT.FACULTY = PROFESSION.FACULTY
Where(PROFESSION_NAME Like '%технологии%' OR PROFESSION_NAME Like '%технология%')