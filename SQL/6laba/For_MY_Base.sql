--1
use F_MyBase
Select * from ������������
SELECT ������������.[��� ������������],
max(������������.����������) [���� ���],
count(*) [���������� ������������]
From ������������ inner join ���������
on ������������.[�������� ������������] = ���������.[�������� ������������]
Group By ������������.[��� ������������]

--2
use F_MyBase
select*
from(select Case when  ������������.[���� �����������] Like '%2023%' then '������'
       when  ������������.[���� �����������] Like '%2024%' then '������'
       when  ������������.[���� �����������] Like '%2025%'then '�����'
         else '����'
         end[���������], COUNT(*)[����������]
from ������������ group by Case when  ������������.[���� �����������] Like '%2023%' then '������'
       when  ������������.[���� �����������] Like '%2024%' then '������'
       when  ������������.[���� �����������] Like '%2025%'then '�����'
             else '����'
              end) as T
              order by case[���������]
             when  '%2023%' then '������'
             when  '%2024%' then '������'
             when  '%2025%' then '�����'
             else '����'
             end
--3
use F_MyBase
Select ������������.[��� ������������],
�������������.[������� ��������������],
round(avg(cast(���������.[���������� ��������] as float)),2) [������ ���������� ��������]
From ������������
inner join ��������� on ���������.[�������� ������������] = ������������.[�������� ������������]
inner join ������������� on �������������.[������� ��������������] = ���������.[������� ��������������]
Where ������������.���������� > 2
Group by ������������.[��� ������������],
�������������.[������� ��������������],
���������.[���������� ��������]
order by ���������.[���������� ��������] desc

--4
use F_MyBase
Select ������������.[��� ������������],
�������������.[������� ��������������],
round(avg(cast(���������.[���������� ��������] as float)),2) [������ ���������� ��������]
From ������������
inner join ��������� on ���������.[�������� ������������] = ������������.[�������� ������������]
inner join ������������� on �������������.[������� ��������������] = ���������.[������� ��������������]
Where ������������.[��� ������������] like '%���%'
Group by ������������.[��� ������������],
�������������.[������� ��������������],
���������.[���������� ��������]
order by ���������.[���������� ��������] desc

--5
use F_MyBase
Select ������������.[�������� ������������], SUM(���������.[���������� ��������]) ����������_����������
From ������������
inner join ��������� on ���������.[�������� ������������] = ������������.[�������� ������������]
where ������������.[��� ������������] in ('���������','���������')
Group by ������������.[�������� ������������]

--6
use F_MyBase
SELECT ������������.[��� ������������], COUNT(���������.[�������� ������������]) AS [���������� ����������]
FROM ������������
LEFT JOIN ��������� ON ���������.[�������� ������������] = ������������.[�������� ������������]
WHERE ���������.[���� ��������] > '2021-12-12'
GROUP BY ������������.[��� ������������]
HAVING ������������.[��� ������������] = '���������' OR ������������.[��� ������������] = '������';
