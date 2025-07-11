use  UNIVER
 -- ������� FACULTY
CREATE TABLE FACULTY (
    FACULTY CHAR(10) PRIMARY KEY NOT NULL,
    FACULTY_NAME VARCHAR(50) DEFAULT '����������'
);

-- ������� PROFESSION
CREATE TABLE PROFESSION (
    PROFESSION CHAR(20) PRIMARY KEY NOT NULL,
    FACULTY CHAR(10) NOT NULL,
    PROFESSION_NAME VARCHAR(100) NOT NULL,
    QUALIFICATION VARCHAR(50),
    FOREIGN KEY (FACULTY) REFERENCES FACULTY(FACULTY)
);

-- ������� PULPIT
CREATE TABLE PULPIT (
    PULPIT CHAR(20) PRIMARY KEY NOT NULL,
    PULPIT_NAME VARCHAR(100) NOT NULL,
    FACULTY CHAR(10) NOT NULL,
    FOREIGN KEY (FACULTY) REFERENCES FACULTY(FACULTY)
);

-- ������� TEACHER
CREATE TABLE TEACHER (
    TEACHER CHAR(10) PRIMARY KEY NOT NULL,
    TEACHER_NAME VARCHAR(100) NOT NULL,
    GENDER CHAR(1) CHECK (GENDER IN ('�', '�')),
    PULPIT CHAR(20) NOT NULL,
    FOREIGN KEY (PULPIT) REFERENCES PULPIT(PULPIT)
);

-- ������� SUBJECT
CREATE TABLE SUBJECT (
    SUBJECT CHAR(10) PRIMARY KEY NOT NULL,
    SUBJECT_NAME VARCHAR(100) NOT NULL UNIQUE,
    PULPIT CHAR(20) NOT NULL,
    FOREIGN KEY (PULPIT) REFERENCES PULPIT(PULPIT)
);

-- ������� AUDITORIUM_TYPE
CREATE TABLE AUDITORIUM_TYPE (
    AUDITORIUM_TYPE CHAR(10) PRIMARY KEY NOT NULL,
	AUDITORIUM_TYPENAME VARCHAR(30) NOT NULL
);

-- ������� AUDITORIUM
CREATE TABLE AUDITORIUM (
    AUDITORIUM CHAR(20) PRIMARY KEY NOT NULL,
    AUDITORIUM_TYPE CHAR(10) NOT NULL,
    AUDITORIUM_CAPACITY INT DEFAULT 1 CHECK (AUDITORIUM_CAPACITY BETWEEN 1 AND 300),
    AUDITORIUM_NAME VARCHAR(50),
    FOREIGN KEY (AUDITORIUM_TYPE) REFERENCES AUDITORIUM_TYPE(AUDITORIUM_TYPE)
);

-- ������� [GROUP] (������������ ������)
CREATE TABLE [GROUP] (
    IDGROUP INT PRIMARY KEY NOT NULL,
    FACULTY CHAR(10) NOT NULL,
    PROFESSION CHAR(20) NOT NULL,
    YEAR_FIRST SMALLINT CHECK (YEAR_FIRST < YEAR(GETDATE()) + 2),
    COURSE AS (YEAR(GETDATE()) - YEAR_FIRST + 1),
    FOREIGN KEY (FACULTY) REFERENCES FACULTY(FACULTY),
    FOREIGN KEY (PROFESSION) REFERENCES PROFESSION(PROFESSION)
);

-- ������� STUDENT
CREATE TABLE STUDENT (
    IDSTUDENT INT PRIMARY KEY IDENTITY(1000,1),
    IDGROUP INT NOT NULL,
    NAME NVARCHAR(100) NOT NULL,
    BDAY DATE,
    STAMP TIMESTAMP,
    INFO XML DEFAULT NULL,
    FOTO VARBINARY(MAX) DEFAULT NULL,
    FOREIGN KEY (IDGROUP) REFERENCES [GROUP](IDGROUP)
);

-- ������� PROGRESS
CREATE TABLE PROGRESS (
    SUBJECT CHAR(10),
    IDSTUDENT INT,
    PDATE DATE,
    NOTE INT CHECK (NOTE BETWEEN 1 AND 10),
    FOREIGN KEY (SUBJECT) REFERENCES SUBJECT(SUBJECT),
    FOREIGN KEY (IDSTUDENT) REFERENCES STUDENT(IDSTUDENT),
    PRIMARY KEY (SUBJECT, IDSTUDENT, PDATE)
);
GO