use master

create table TEMP(GG int, VV char(20), CC varchar(50));

declare @i int = 0;

while @i < 20000
begin
Insert into TEMP(GG,VV) values (floor(1000*Rand()), replicate('строка ', 2));
set @i +=1;
end;
CREATE NONCLUSTERED INDEX IX_TEMP_GG
ON TEMP(GG)
INCLUDE (VV);
--drop index IX_TEMP_GG on TEMP;
SELECT GG, VV FROM TEMP WHERE GG = 15;
select * from TEMP;
select * from TEMP where TEMP.GG = 15;