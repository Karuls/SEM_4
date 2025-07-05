using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.ViewModel
{
    public static class DataBaseCheck
    {

        public static bool DatabaseExists()
        {
            string masterConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=master;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();
                string query = "SELECT database_id FROM sys.databases WHERE name = 'CarSharing'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    return command.ExecuteScalar() != null;
                }
            }
        }

        public static void MyDataBase()
        {
            CreateDatabase();
            CreateTables();

        }
        public static void CreateDatabase()
        {
            string masterConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=master;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();
                string query = "CREATE DATABASE CarSharing";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void CreateTables()
        {
            string connectionString = GlobalDataForXAML.ConnectionStringToDB;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            CREATE TABLE [dbo].[Users] (
    [Id]               INT            NOT NULL,
    [Login]            NVARCHAR (50)  NOT NULL,
    [Email]            NVARCHAR (50)  NOT NULL,
    [Password]         NVARCHAR (50)  NOT NULL,
    [DateTime]         DATETIME2 (7)  NOT NULL,
    [FavorutiesCarsId] NVARCHAR (MAX) NULL,
    [Role]             NCHAR (10)     DEFAULT (user_name()) NULL,
    [ImagePath]        NVARCHAR (MAX) NULL,
    [Orders]           NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);



           CREATE TABLE [dbo].[Products] (
    [id]                INT            NOT NULL,
    [CarName]           NVARCHAR (50)  NOT NULL,
    [Пробег]            NVARCHAR (50)  NULL,
    [Цвет]              NCHAR (10)     NULL,
    [Количество]        INT            NULL,
    [Цена]              INT            NULL,
    [ImagePath]         NVARCHAR (MAX) NULL,
    [Допустимый_пробег] NVARCHAR (50)  NULL,
    [Features]          VARCHAR (MAX)  NULL,
    [Объем_Двигателя]   NVARCHAR (50)  NULL,
    [transmission]      NVARCHAR (50)  NULL,
    [Top_speed]         INT            NULL,
    [IsFavoruties]      INT            NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


CREATE TABLE [dbo].[Orders] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [id_user]   INT           NULL,
    [id_car]    INT           NULL,
    [imagepath] VARCHAR (MAX) NULL,
    [date]      DATE          NULL,
    [coast]     INT           NULL,
    [hours]     INT           NULL,
    [Index]     INT           DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Feedbacks] (
    [Id]         INT           NOT NULL,
    [feedback]   VARCHAR (MAX) NOT NULL,
    [id_user]    INT           NOT NULL,
    [AvatarPath] VARCHAR (MAX) NULL,
    [Login_user] VARCHAR (MAX) NULL,
    [Date]       DATETIME2 (7) NULL,
    [id_car]     INT           NULL,
    CONSTRAINT [PK_Feedbacks] PRIMARY KEY CLUSTERED ([Id] ASC)
);
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }





    }
}
