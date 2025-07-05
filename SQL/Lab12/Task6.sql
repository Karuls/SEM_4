use UNIVER;
-- Таблица А
    set transaction isolation level  REPEATABLE READ	-- не допускает неподтвержденного чтения и неповторяющегося чтения, 
														-- но при этом возможно фантомное чтение
	begin transaction 
	select SUBJECT_NAME from SUBJECT where PULPIT = 'ТЛ';
	-------------------------- t1 ------------------ 
	-------------------------- t2 -----------------
	select  case
          when SUBJECT = 'ДПИ' then 'insert  SUBJECT'  else ' ' 
end 'результат', SUBJECT_NAME from SUBJECT  where PULPIT = 'ТЛ';
	commit; 

	--- Таблица B	
	begin transaction 	  
	-------------------------- t1 --------------------
          	INSERT into SUBJECT values('ДПИ','Дизайн пользовательских интерфейсов','ИСиТ');   
          commit; 
	-------------------------- t2 --------------------

delete SUBJECT where SUBJECT = 'ДПИ';