use F_MyBase

--1
Select ������������.[�������� ������������], ���������.[���� ��������]
From ������������,���������
Where ������������.[�������� ������������] = ���������.[�������� ������������]
and
���������.[������� ��������������] IN (SELECT [������� ��������������] FROM �������������
WHERE �������������.�������� LIKE '%����%')

--2
Select ������������.[�������� ������������], ���������.[���� ��������]
From ������������
inner join ���������
ON
���������.[�������� ������������] = ������������.[�������� ������������]
Where ���������.[������� ��������������] IN (SELECT [������� ��������������] FROM �������������
WHERE �������������.[������� ��������������] LIKE '%��%')

--3
Select ������������.[�������� ������������], ���������.[���� ��������]
From ������������
inner join ���������
ON
���������.[�������� ������������] = ������������.[�������� ������������]
inner join �������������
ON
�������������.[������� ��������������] = ���������.[������� ��������������]
where(�������������.[������� ��������������] LIKE '%��%')

--4
Select ������������.[�������� ������������], ������������.����������, ���������.[������� ��������]
From ������������, ���������
where ���������.[������� ��������������] = (Select top(1) [������� ��������������] FROM �������������
WHERE �������������.[������� ��������������] LIKE '%��%'
order by �������������.[���� ������ �� ������] desc)

--5
use F_MyBase	
Select ������������.[�������� ������������]
From ������������
where not exists(Select *from ���������
where ���������.[�������� ������������] = ������������.[�������� ������������])

--6
use F_MyBase
Select top 1
(SELECT AVG(���������.[���������� ��������]) from ��������� where ���������.[������� ��������] = '������')[��������� ������������],
(SELECT AVG(���������.[���������� ��������]) from ��������� where ���������.[������� ��������] = '������')[�� ������]
From ���������
 
 --7
 Select ���������.[�������� ������������], ���������.[���� ��������],���������.[���������� ��������]
 from ���������
 where ���������.[���������� ��������]
 >= all(Select ���������.[���������� ��������] 
 from ��������� where ���������.[������� ��������������] like '%��%');

 --8
 Select �������������.[������� ��������������], �������������.���, �������������.[���� ������ �� ������]
 from �������������
 where �������������.[���� ������ �� ������] > any
 (Select �������������.[���� ������ �� ������] from ������������� 
 where �������������.[������� ��������������] like '%��%')