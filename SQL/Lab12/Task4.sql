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