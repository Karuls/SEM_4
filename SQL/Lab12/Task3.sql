-- ������� 3

-- ������ �������
select * from STUDENT

declare @control_point varchar(32)
begin try
	begin tran
		insert into STUDENT(IDGROUP, NAME, BDAY) values(19, '������ ���� ��������', '2006-05-20');
		set @control_point = 'p1'; save tran @control_point;
		insert into STUDENT(IDSTUDENT, IDGROUP, NAME, BDAY) values(1000, 19, '������ ���� ��������', '2006-05-20');
		commit tran;
end try
begin catch
	print '������: '+ CHAR(10) + case
	when error_number() = 2627 then CHAR(9) + '��������� ������������ �����'
	else CHAR(9) + '����������� ������: ��� - ' + cast(error_number() as varchar(5)) + CHAR(10) + CHAR(9) + error_message()
	end;
	if @@TRANCOUNT > 0
	begin
		print '����������� � ����������� �����: ' + @control_point;
		rollback tran @control_point;
		commit tran;
	end
end catch;
go

select * from STUDENT

delete from STUDENT where Name = '������ ���� ��������';

-- ������ ��������

select * from PROGRESS

declare @control_point varchar(32)
begin try
	begin tran
		insert into PROGRESS values('����', 1070, '2013-01-11', 5);
		set @control_point = 'p1'; save tran @control_point;
		insert into PROGRESS values('����', 1070, '2013-01-19', 7);
		set @control_point = 'p2'; save tran @control_point;
		delete from STUDENT where Name = '������� ����� ��������';
		commit tran;
end try
begin catch
	print '������: '+ CHAR(10) + case
	when error_number() = 2627 then CHAR(9) + '��������� ������������ �����'
	else CHAR(9) + '����������� ������: ��� - ' + cast(error_number() as varchar(5)) + CHAR(10) + CHAR(9) + error_message()
	end;
	if @@TRANCOUNT > 0
	begin
		print '����������� � ����������� �����: ' + @control_point; 
		rollback tran @control_point;
		commit tran;
	end
end catch;
go

select * from PROGRESS

delete from PROGRESS where IDSTUDENT = 1070 and SUBJECT = '����'
delete from PROGRESS where IDSTUDENT = 1070 and SUBJECT = '����'