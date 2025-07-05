-- ЗАДАНИЕ 1
Drop Table if exists NewT 
set implicit_transactions on
declare @flag char = 'с'
Create table NewT(Key_ int primary key, Word varchar(10));
insert into NewT(Key_ , Word)
values(1, 'one'),
	  (2, 'two'),
	  (3, 'three'),
	  (4, 'four'),
	  (5, 'five')
if @flag = 'с' commit;
else rollback;
select * from NewT;
Set implicit_transactions off
go