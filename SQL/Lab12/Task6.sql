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