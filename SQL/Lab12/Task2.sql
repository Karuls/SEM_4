-- ЗАДАНИЕ 2
use UNIVER;
select * from STUDENT

-- выполняется
begin try
	begin tran
		insert into STUDENT(IDGROUP, NAME, BDAY) values(19, 'Петров Петр Петрович', '2006-05-20');
		delete from STUDENT where NAME = 'Бакунович Алина Олеговна';
		Update STUDENT set IDGROUP = 19 where IDSTUDENT = 1079;
		commit tran;
end try
begin catch
	print 'ошибка: ' + case
	when error_number() = 2627 then 'Нарушение уникальности ключа'
	else 'неизвестная ошибка: ' + cast(error_number() as varchar(5)) + error_message()
	end;
	if @@TRANCOUNT > 0 rollback tran;
end catch;
go

select * from STUDENT

delete from STUDENT where Name = 'Петров Петр Петрович';
insert into STUDENT(IDGROUP, NAME, BDAY) values(18, 'Бакунович Алина Олеговна', '1995-08-05');
Update STUDENT set IDGROUP = 18 where IDSTUDENT = 1079;


-- ошибка уникальности 
select * from STUDENT

begin try
	begin tran
		insert into STUDENT(IDGROUP, NAME, BDAY) values(19, 'Петров Петр Петрович', '2006-05-20');
		insert into STUDENT(IDSTUDENT, IDGROUP, NAME, BDAY) values(1000, 19, 'Петров Петр Петрович', '2006-05-20');
		commit tran;
end try
begin catch
	print 'ошибка: '+ CHAR(10) + case
	when error_number() = 2627 then CHAR(9) + 'Нарушение уникальности ключа'
	else CHAR(9) + 'неизвестная ошибка: код - ' + cast(error_number() as varchar(5)) + CHAR(10) + CHAR(9) + error_message()
	end;
	if @@TRANCOUNT > 0 rollback tran;
end catch;
go

select * from STUDENT


-- вставка в несуществующий айдишник
select * from PROGRESS

begin try
	begin tran
		insert into PROGRESS values('СУБД', 1070, '2013-01-11', 5);
		delete from STUDENT where Name = 'Дуброва Павел Игоревич';
		commit tran;
end try
begin catch
	print 'ошибка: '+ CHAR(10) + case
	when error_number() = 2627 then CHAR(9) + 'Нарушение уникальности ключа'
	else CHAR(9) + 'неизвестная ошибка: код - ' + cast(error_number() as varchar(5)) + CHAR(10) + CHAR(9) + error_message()
	end;
	if @@TRANCOUNT > 0 rollback tran;
end catch;
go

select * from PROGRESS
