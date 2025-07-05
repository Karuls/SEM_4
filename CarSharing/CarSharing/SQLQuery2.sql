CREATE TABLE [dbo].[CarInfo] (
    [id] INT  PRIMARY KEY, -- Автоинкрементируемый уникальный идентификатор
    [CarName] NVARCHAR (50) NOT NULL,
    [Пробег] NVARCHAR (50) NULL,
    [Цвет] NCHAR (10) NULL,
    [Количество] INT NULL,
    [Цена] NVARCHAR (50) NULL,
    [ImagePath] NVARCHAR (50) NULL,
    [Допустимый пробег] NVARCHAR (50) NULL,
    [Features] VARCHAR (MAX) NULL,
    [Объем двигателя] NVARCHAR (50) NULL,
    [transmission] NVARCHAR (50) NULL
);
