delete SUBJECT where SUBJECT = '���';

select * from PULPIT

begin tran
update PULPIT set PULPIT_NAME = '������ � ���������� �������������' where PULPIT.FACULTY = '����'
	begin tran 
	update PULPIT set PULPIT_NAME = '���' where PULPIT.FACULTY = '����'
	commit;
	select * from PULPIT

rollback
select * from PULPIT