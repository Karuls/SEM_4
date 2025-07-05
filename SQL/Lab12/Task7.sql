-- Таблица A
set transaction isolation level SERIALIZABLE	-- отсутствие фантомного, неподтвержденного и неповторяющегося чтения
	begin transaction 
		delete SUBJECT where SUBJECT = 'ДПИ';
		INSERT into SUBJECT values('ДПИ', 'Дизайн пользовательских интерфейсов', 'ИСиТ');
        update SUBJECT set SUBJECT_NAME = 'Дизайн пользовательских interface' where  SUBJECT = 'ДПИ';
	    select SUBJECT_NAME,PULPIT from SUBJECT where PULPIT = 'ИСиТ';
	-------------------------- t1 -----------------
	 select SUBJECT_NAME,PULPIT from SUBJECT where PULPIT = 'ИСиТ';
	-------------------------- t2 ------------------ 
	commit; 	

--- Таблица B	
	begin transaction 	  
		delete SUBJECT where SUBJECT = 'ДПИ';
		INSERT into SUBJECT values('ДПИ', 'Дизайн пользовательских интерфейсов','ИСиТ');
        update SUBJECT set SUBJECT_NAME = 'Дизайн пользовательских interface' where  SUBJECT = 'ДПИ';
	    select SUBJECT_NAME from SUBJECT where PULPIT = 'ИСиТ';
     -------------------------- t1 --------------------
     commit; 
     select SUBJECT_NAME,PULPIT from SUBJECT where PULPIT = 'ИСиТ';
     -------------------------- t2 --------------------
	 		
	delete SUBJECT where SUBJECT = 'ДПИ';
