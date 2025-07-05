delete SUBJECT where SUBJECT = 'ДПИ';

select * from PULPIT

begin tran
update PULPIT set PULPIT_NAME = 'Лесных и технологии лесозаготовок' where PULPIT.FACULTY = 'ТТЛП'
	begin tran 
	update PULPIT set PULPIT_NAME = 'Лес' where PULPIT.FACULTY = 'ТТЛП'
	commit;
	select * from PULPIT

rollback
select * from PULPIT