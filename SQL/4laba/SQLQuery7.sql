use UNIVER


---- ������� "������"
--CREATE TABLE Products (
--    ProductID INT PRIMARY KEY,
--    ProductName VARCHAR(50)
--);

---- ������� "������"
--CREATE TABLE Orders (
--    OrderID INT PRIMARY KEY,
--    ProductID INT,
--    Quantity INT
--);

---- ������� ������ � ������� "������"
--INSERT INTO Products (ProductID, ProductName)
--VALUES 
--    (1, '������'),
--    (2, '����'),
--    (3, '���');

---- ������� ������ � ������� "������"
--INSERT INTO Orders (OrderID, ProductID, Quantity)
--VALUES 
--    (101, 1, 10),
--    (102, 3, 5),
--    (103, 4, 8);
--Go

---- 5

--select * from Products
--select * from Orders
Go

SELECT 
    Products.ProductID AS ProductID,
    Products.ProductName AS ProductName,
    Orders.OrderID AS OrderID,
    Orders.Quantity AS Quantity
FROM 
    Products
FULL OUTER JOIN 
    Orders
ON 
    Products.ProductID = Orders.ProductID;
Go

SELECT 
    Products.ProductID AS ProductID,
    Products.ProductName AS ProductName
FROM 
    Products
FULL OUTER JOIN 
    Orders
ON 
    Products.ProductID = Orders.ProductID
WHERE 
    Orders.ProductID IS NULL;
Go

SELECT 
    Orders.OrderID AS OrderID,
    Orders.Quantity AS Quantity
FROM 
    Products
FULL OUTER JOIN 
    Orders
ON 
    Products.ProductID = Orders.ProductID
WHERE 
    Products.ProductID IS NULL;
Go

SELECT 
    Products.ProductID AS ProductID,
    Products.ProductName AS ProductName,
    Orders.OrderID AS OrderID,
    Orders.Quantity AS Quantity
FROM 
    Products
FULL OUTER JOIN 
    Orders
ON 
    Products.ProductID = Orders.ProductID
WHERE 
    Products.ProductID IS NOT NULL AND Orders.ProductID IS NOT NULL;
Go

SELECT 
    Products.ProductID AS ProductID,
    Products.ProductName AS ProductName,
    Orders.OrderID AS OrderID,
    Orders.Quantity AS Quantity
FROM 
    Orders
FULL OUTER JOIN 
    Products
ON 
    Products.ProductID = Orders.ProductID;
