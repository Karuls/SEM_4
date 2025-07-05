use F_MyBase

go
--create PROCEDURE GETCOUNT
--as
--begin
--declare @count int = (select COUNT(*) from ������������)
--select @count �����
--return @count + 1;
--end;

declare @call int = 0 ;
exec @call = GETCOUNT;
print 'fe' + cast(@call as varchar(10));
--2

declare @i int = 0, @type varchar(20) = '���������', @c int = 0;
exec @i = GETCOUNT @type, @count1 = @c output
print '���-�� ���������� ���� '+ @type + ': ' +cast(@c as varchar(10));

--3
CREATE TABLE #Zk (
   [�������� ������������] [nchar](10) NULL,
	[��� ������������] [nchar](10) NULL,
	[���� �����������] [date] NULL,
	[����������] [nchar](10) NOT NULL,
	[������������� ���������] [nchar](10) NULL
);


--ALTER PROCEDURE GETCOUNT @p VARCHAR(20)
--AS
--BEGIN
--    DECLARE @k INT = (SELECT COUNT(*) FROM ������������);
--    SELECT * FROM ������������ WHERE ������������.[��� ������������] = @p;
--END;

INSERT #Zk EXEC GETCOUNT @p = '���������';
INSERT #Zk EXEC GETCOUNT @p = '��������';

Select * from #Zk

--4
CREATE PROCEDURE EX4 
    @a NCHAR(10), 
    @b NCHAR(10), 
    @c DATE, 
    @d NCHAR(10), 
    @f NCHAR(10)
AS
BEGIN
    BEGIN TRY
        INSERT INTO #Zk VALUES (@a, @b, @c, @d, @f);
    END TRY
    BEGIN CATCH 
        PRINT '����� ������      : ' + CAST(ERROR_NUMBER() AS VARCHAR(6));
        PRINT '���������         : ' + ERROR_MESSAGE();
        PRINT '�������           : ' + CAST(ERROR_SEVERITY() AS VARCHAR(6));
        PRINT '��������� (State) : ' + CAST(ERROR_STATE() AS VARCHAR(6));
        PRINT '������ � �������  : ' + CAST(ERROR_LINE() AS VARCHAR(6));
        RETURN -1;
    END CATCH
END;
--drop procedure EX4
 DECLARE @a NCHAR(10), 
         @b NCHAR(10), 
         @c DATE, 
         @d NCHAR(10), 
         @f NCHAR(10)
exec EX4 @a = '������16', @b = '���������', @c = '03-03-2222', @d = '11', @f = '���';
select * from #Zk
--5

CREATE PROC TypeOfMashine @p CHAR(10)
AS
BEGIN TRY
    -- ��������� �������������
    IF NOT EXISTS (SELECT 1 FROM ������������ WHERE ������������.[��� ������������] = @p)
        RAISERROR('������ � ����������', 16, 1)
    
    -- ��������� ����������
    DECLARE @subject_list VARCHAR(500) = ''
    DECLARE @count INT = 0
    
    -- ��������� ������ �  ����� �������
    SELECT @subject_list = @subject_list + RTRIM([�������� ������������]) + ', ',
           @count = @count + 1
    FROM ������������ 
    WHERE ������������.[��� ������������] = @p
    
    -- ������� ��������� ������� � ������
    SET @subject_list = LEFT(@subject_list, LEN(@subject_list) - 1)
    
    -- ������� �����
    PRINT '������ '+ @p + ': ' + @subject_list
    
    -- ���������� ���������� 
    RETURN @count
    
END TRY
BEGIN CATCH
    PRINT '������ � ����������'
    PRINT '���������: ' + ERROR_MESSAGE()
    RETURN 0
END CATCH
GO

exec TypeOfMashine @p = '���������'

--6 
Create PROC EX6 @p CHAR(10)
AS
BEGIN TRY
    set transaction isolation level SERIALIZABLE;          
    begin tran
	insert into #Zk([�������� ������������],[��� ������������], [���� �����������],	[����������], [������������� ���������])
	values('������22', '���������', '03-03-2222', '11', 'EFE');
	exec TypeOfMashine @p 
	select * from #Zk
	commit tran;
    
END TRY
BEGIN CATCH
    PRINT '������ � ����������'
    
     if (@@trancount > 0) 
	 begin
	 rollback tran ; 
	 end;
     return -1;	

END CATCH
GO

exec Ex6 @p = '���������'