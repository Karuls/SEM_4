--ex.1
use master;

set nocount on						-- ������������ ��� ���������� ��������� � ���������� ������������ �����, 
									-- ������� ������������ ��� ���������� �������.

if exists (select * from  SYS.OBJECTS where OBJECT_ID = object_id (N'DBO.NewTable') )
begin
	drop table NewTable;
end;

declare @c int, @flag char = 'c';	-- commit ��� rollback?
SET IMPLICIT_TRANSACTIONS  ON		-- �����. ����� ������� ����������

create table NewTable				-- ������ ���������� 
(
	i int identity(1,1),
	word varchar(50) not null
);

declare @tableName NVARCHAR(128)
set @tableName = 'NewTable'

insert newTable (word) values ('������1'), ('������2'), ('������3'), ('������4');

set @c = (select count(*) from NewTable);

print '���������� ����� � ������� NewTable: ' + cast( @c as varchar(4));
if @flag = 'c'
begin
print 'commit'
	commit;							-- ���������� ����������: �������� 
end;

else
begin
print 'rollback'
rollback;							-- ���������� ����������: ����� 
end;

SET IMPLICIT_TRANSACTIONS  OFF		-- ������. ����� ������� ����������

if exists (select * from  SYS.OBJECTS where OBJECT_ID = object_id (N'DBO.NewTable') )
begin
	print '������� � ��������� ' + @tableName + ' ����������'
end;
else
begin
	print '������� � ��������� ' + @tableName + ' �� ����������'
end;

--ex.2
use F_MyBase;
begin try
	begin tran						-- ������ ����� ���������� (����� ������ �������� � �������)
		delete ��������  where ��������.������ = 4;
		insert into �������� values(11,'�������� ��������� ��������','2006-04-04', '��-227', '5');
		update �������� set ��������.������ = '15' where ��������.���='�������� ��������� ��������';
	commit tran;
end try

begin catch
	print '������: ' + cast(error_number() as varchar(5)) + ' ' + error_message()
	if @@TRANCOUNT > 0 rollback tran;
end catch

--ex.3
use F_MyBase;
declare @savepoint varchar(30);
begin try
	begin tran
		delete �������� where ��������.��� = '�������� ��������� ��������';									
		set @savepoint = 'save1'; save transaction @savepoint; -- �� - 1

		insert into �������� values('','�������� ���� ��������','14-08-2025', '��-228', '2');					
		set @savepoint = 'save2'; save transaction @savepoint; -- �� - 2

		update �������� set ��������.������ = '5' where ��������.��� = '�������� ���� ��������';		
		set @savepoint = 'save3'; save transaction @savepoint; -- �� - 3
	commit tran;
end try

begin catch
	print '������: ' + cast(error_number() as varchar(5)) + ' ' + error_message()
	if @@TRANCOUNT > 0
		begin
			print '����������� �����: ' + @savepoint;
			rollback tran @savepoint;
			commit tran;
		end;
end catch;

--insert into AUDITORIUM values('301-1','��-�', '15', '301-1');

--ex.4
use UNIVER;

-- ������� A
	set transaction isolation level READ UNCOMMITTED --��������� ����������������, ��������������� � ��������� ������
	begin transaction 
	-------------------------- t1 ------------------
	select @@SPID 'SID', 'insert AUDITORIUM' '���������', * from SUBJECT where SUBJECT = '���';
																	             
	select @@SPID 'SID', 'update AUDITORIUM'  '���������',  AUDITORIUM_NAME, 
                      AUDITORIUM_TYPE,AUDITORIUM_CAPACITY from AUDITORIUM   where  AUDITORIUM_NAME='301-1';
	commit; 
	-------------------------- t2 -----------------

--- ������� B	
	begin transaction 
	select @@SPID 'SID'; -- SPID - ���������� ��������� ������������� ��������, ����������� �������� �������� �����������
	INSERT into SUBJECT values('���','������ ���������������� �����������','����');   
	update AUDITORIUM set AUDITORIUM_CAPACITY = '15' where AUDITORIUM_NAME = '301-1';	
	-------------------------- t1 --------------------
	-------------------------- t2 --------------------
	rollback;

	delete SUBJECT where SUBJECT = '���';

--ex.5
use UNIVER;

-- ������� A
    set transaction isolation level READ COMMITTED	-- �� ��������� ����������������� ������, 
													-- �� ��� ���� �������� ��������������� � ������-��� ������
	begin transaction
	select count(*) from AUDITORIUM where AUDITORIUM_CAPACITY = '30';
	-------------------------- t1 ------------------ 
	-------------------------- t2 -----------------
	select @@SPID 'SID', 'update AUDITORIUM'  '���������',  AUDITORIUM_NAME, 
                      AUDITORIUM_TYPE,AUDITORIUM_CAPACITY from AUDITORIUM   where  AUDITORIUM_NAME='301-1';
	commit; 

	--- ������� B	
	begin transaction 	

	-------------------------- t1 --------------------
    select @@SPID 'SID' update AUDITORIUM set AUDITORIUM_CAPACITY = '30' where AUDITORIUM_NAME='301-1';	

      commit; 
	-------------------------- t2 --------------------	

--ex.6
use UNIVER;
-- ������� �
    set transaction isolation level  REPEATABLE READ	-- �� ��������� ����������������� ������ � ���������������� ������, 
														-- �� ��� ���� �������� ��������� ������
	begin transaction 
	select SUBJECT_NAME from SUBJECT where PULPIT = '��';
	-------------------------- t1 ------------------ 
	-------------------------- t2 -----------------
	select  case
          when SUBJECT = '���' then 'insert  SUBJECT'  else ' ' 
end '���������', SUBJECT_NAME from SUBJECT  where PULPIT = '��';
	commit; 

	--- ������� B	
	begin transaction 	  
	-------------------------- t1 --------------------
          	INSERT into SUBJECT values('���','������ ���������������� �����������','����');   
          commit; 
	-------------------------- t2 --------------------

delete SUBJECT where SUBJECT = '���';

--ex.7
-- ������� A
set transaction isolation level SERIALIZABLE	-- ���������� ����������, ����������������� � ���������������� ������
	begin transaction 
		delete SUBJECT where SUBJECT = '���';
		INSERT into SUBJECT values('���', '������ ���������������� �����������', '����');
        update SUBJECT set SUBJECT_NAME = '������ ���������������� interface' where  SUBJECT = '���';
	    select SUBJECT_NAME,PULPIT from SUBJECT where PULPIT = '����';
	-------------------------- t1 -----------------
	 select SUBJECT_NAME,PULPIT from SUBJECT where PULPIT = '����';
	-------------------------- t2 ------------------ 
	commit; 	

--- ������� B	
	begin transaction 	  
		delete SUBJECT where SUBJECT = '���';
		INSERT into SUBJECT values('���', '������ ���������������� �����������','����');
        update SUBJECT set SUBJECT_NAME = '������ ���������������� interface' where  SUBJECT = '���';
	    select SUBJECT_NAME from SUBJECT where PULPIT = '����';
     -------------------------- t1 --------------------
     commit; 
     select SUBJECT_NAME,PULPIT from SUBJECT where PULPIT = '����';
     -------------------------- t2 --------------------
	 		
	delete SUBJECT where SUBJECT = '���';

--ex.8
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