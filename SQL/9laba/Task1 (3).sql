use UNIVER;

DECLARE @c char(1) = 'F', 
        @vc varchar(4) = 'Vadim',
        @dt datetime,
        @t time(0),
        @i int,
        @si smallint,
        @ti tinyint,  
        @num numeric(12, 5);


SET @i = (SELECT SUM(NOTE) FROM PROGRESS); 
SET @dt = GETDATE();
SET @t = '12:21:45';

SELECT @si = MIN(NOTE), @num = AVG(NOTE) FROM PROGRESS;
SET @ti = CASE 
               WHEN @i + @si > 255 THEN 255 
               ELSE @i + @si 
           END;

PRINT 'Переменная c: ' + @c;
PRINT 'Переменная vc: ' + @vc;
PRINT 'Переменная dt: ' + CAST(@dt AS varchar(20));
PRINT 'Переменная t: ' + CAST(@t AS varchar(20));


SELECT @i AS TotalNotes, @si AS MinNote, @ti AS TotalSum, @num AS AvgNote;