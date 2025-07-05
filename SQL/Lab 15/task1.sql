-- Создаём таблицу аудита
create table TR_AUDIT
(
	ID int identity(1, 1),										
	STMT varchar(20) check (STMT in ('INS', 'DEL', 'UPD')),		
	TRNAME varchar(50),											
	CC varchar(300)												
)
go

-- Удаляем триггер, если он есть
IF OBJECT_ID('TR_TEACHER_INS', 'TR') IS NOT NULL
    DROP TRIGGER TR_TEACHER_INS;
GO

-- Создаём триггер
create trigger TR_TEACHER_INS on TEACHER after insert
as 
declare @TEACHER char(10), @TEACHER_NAME varchar(100),
		   @GENDER char(1), @PULPIT char(20), @IN varchar(300)
print 'Выполнена операция INSERT'
set @TEACHER = (select TEACHER from INSERTED)
set @TEACHER_NAME = (select TEACHER_NAME from INSERTED)
set @GENDER = (select GENDER from INSERTED)
set @PULPIT = (select PULPIT from INSERTED)
set @IN = ltrim(rtrim(@TEACHER)) + ' ' + ltrim(rtrim(@TEACHER_NAME)) + 
		  ' ' + ltrim(rtrim(@GENDER)) + ' ' + ltrim(rtrim(@PULPIT))
insert into TR_AUDIT (STMT, TRNAME, CC) values ('INS', 'TR_TEACHER_INS', @IN)
return;
go

-- Пример вставки
insert into TEACHER values ('ИВНВ', 'Иванов Иван Иванович', 'м', 'ИСиТ')

select * from TEACHER
select * from TR_AUDIT order by ID

-- Очистка
DROP TABLE TR_AUDIT;
DROP TRIGGER TR_TEACHER_INS;
GO