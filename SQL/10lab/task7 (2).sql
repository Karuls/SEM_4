USE Grin_My_Base;
GO

/* 1. Простой индекс на Quantity */
CREATE INDEX #index1 ON Orders(Quantity ASC);
GO

SELECT * 
FROM Orders 
WHERE Quantity BETWEEN 1 AND 20 
ORDER BY Quantity;
GO

CHECKPOINT;
DBCC DROPCLEANBUFFERS;
GO

DROP INDEX #index1 ON Orders;
GO

/* 2. Составной индекс */
CREATE INDEX #index2 ON Orders(Quantity, ProductID);
GO

SELECT * 
FROM Orders 
WHERE Quantity BETWEEN 1 AND 20 
ORDER BY Quantity;
GO

CHECKPOINT;
DBCC DROPCLEANBUFFERS;
GO
DROP INDEX #index2 ON Orders;  
GO

/* 3. Индекс с INCLUDE */
CREATE INDEX #index3 ON Orders(Quantity) INCLUDE(ProductID);
GO

SELECT * 
FROM Orders 
WHERE Quantity BETWEEN 1 AND 20 
ORDER BY Quantity;
GO

CHECKPOINT;
DBCC DROPCLEANBUFFERS;
GO

DROP INDEX #index3 ON Orders;  
GO

/* 4. Фильтрованный индекс */
CREATE INDEX #index4 ON Orders(Quantity) WHERE Quantity >= 1 AND Quantity <= 20;
GO

SELECT * 
FROM Orders 
WHERE Quantity BETWEEN 150 AND 2000 
ORDER BY Quantity;
GO

CHECKPOINT;
DBCC DROPCLEANBUFFERS;
GO

DROP INDEX #index4 ON Orders; 
GO
