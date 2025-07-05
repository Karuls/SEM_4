use UNIVER;

select PROGRESS.IDSTUDENT, PROGRESS.SUBJECT, 
case
	when AVG(PROGRESS.NOTE) = 4 then '4'
	when AVG(PROGRESS.NOTE) between 5 and 6 then 'удовлетворительно'
	when AVG(PROGRESS.NOTE) between 7 and 8 then 'хорошо'
	when AVG(PROGRESS.NOTE) between 9 and 10 then 'отлично'
	else 'не удолетворительно'
end

from PROGRESS
group by IDSTUDENT, SUBJECT
select * from PROGRESS

