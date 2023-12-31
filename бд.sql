USE [master]
GO
/****** Object:  Database [BankDB]    Script Date: 13.04.2022 12:06:52 ******/
CREATE DATABASE [BankDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BankDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\BankDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BankDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\BankDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BankDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BankDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BankDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BankDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BankDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BankDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BankDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BankDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BankDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BankDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BankDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BankDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BankDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BankDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BankDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BankDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BankDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BankDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BankDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BankDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BankDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BankDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BankDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BankDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BankDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BankDB] SET  MULTI_USER 
GO
ALTER DATABASE [BankDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BankDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BankDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BankDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BankDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BankDB] SET QUERY_STORE = OFF
GO
USE [BankDB]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[ID_Service] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[ID_Client] [int] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ID_Service] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[marketPlan]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[marketPlan](
	[ID_Market] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Market_Date] [date] NOT NULL,
	[Sum] [decimal](18, 0) NOT NULL,
	[ID_Service] [int] NOT NULL,
 CONSTRAINT [PK_marketPlan] PRIMARY KEY CLUSTERED 
(
	[ID_Market] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[marketView]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[marketView]
AS
SELECT dbo.marketPlan.ID_Market, dbo.Service.ID_Service, dbo.marketPlan.Title AS [Маркетинговый план], dbo.marketPlan.Market_Date AS [Дата формирования], dbo.marketPlan.Sum AS [Сумма для реализации], 
                  dbo.Service.Title AS Услуга
FROM     dbo.marketPlan INNER JOIN
                  dbo.Service ON dbo.marketPlan.ID_Service = dbo.Service.ID_Service
GO
/****** Object:  Table [dbo].[Client]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ID_Client] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Middle_Name] [varchar](50) NULL,
	[Birthday] [date] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ID_Client] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[serviceView]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[serviceView]
AS
SELECT dbo.Service.ID_Service, dbo.Client.ID_Client, dbo.Service.Title AS Услуга, dbo.Service.Price AS Стоимость, dbo.Client.Surname AS Клиент
FROM     dbo.Client INNER JOIN
                  dbo.Service ON dbo.Client.ID_Client = dbo.Service.ID_Client
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[ID_Employee] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Middle_Name] [varchar](50) NULL,
	[Birthday] [date] NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[ID_Job] [int] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[ID_Employee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Credit_Agreement]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credit_Agreement](
	[ID_Credit_Agreement] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Agreement_Date] [date] NOT NULL,
	[Sum] [decimal](18, 0) NOT NULL,
	[ID_Client] [int] NOT NULL,
	[ID_Employee] [int] NOT NULL,
 CONSTRAINT [PK_Credit_Agreement] PRIMARY KEY CLUSTERED 
(
	[ID_Credit_Agreement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Customer]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Customer]
AS
SELECT dbo.Client.Surname AS [Фамилия клиента], dbo.Client.Name AS [Имя клиента], dbo.Client.Middle_Name AS [Отчество клиента], dbo.Client.Birthday AS [Дата рождения], dbo.Credit_Agreement.Title AS [Кредитный договор], 
                  dbo.Employees.Surname AS Утвердил, dbo.Service.Title AS Услуга
FROM     dbo.Client INNER JOIN
                  dbo.Credit_Agreement ON dbo.Client.ID_Client = dbo.Credit_Agreement.ID_Client INNER JOIN
                  dbo.Employees ON dbo.Credit_Agreement.ID_Employee = dbo.Employees.ID_Employee INNER JOIN
                  dbo.Service ON dbo.Client.ID_Client = dbo.Service.ID_Client
GO
/****** Object:  View [dbo].[Market]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Market]
AS
SELECT dbo.Planned_Service.Title AS [Планируемая услуга], dbo.Planned_Service.Planned_Service_Date AS [Дата реализации], dbo.Planned_Service.Price AS Стоимость, dbo.Planned_Service.Worked_Check AS Реализовано, 
                  dbo.Employees.Surname AS Фамилия
FROM     dbo.Employees INNER JOIN
                  dbo.Planned_Service ON dbo.Employees.ID_Employee = dbo.Planned_Service.ID_Employee
GO
/****** Object:  Table [dbo].[Job]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[ID_Job] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Sum] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[ID_Job] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[employees_job]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[employees_job]
AS
SELECT dbo.Employees.ID_Employee, dbo.Job.ID_Job, dbo.Employees.Surname AS Фамилия, dbo.Employees.Name AS Имя, dbo.Employees.Middle_Name AS Отчество, dbo.Employees.Birthday AS [Дата рождения], 
                  dbo.Employees.Login AS Логин, dbo.Employees.Password AS Пароль, dbo.Job.Title AS Должность
FROM     dbo.Employees INNER JOIN
                  dbo.Job ON dbo.Employees.ID_Job = dbo.Job.ID_Job
GO
/****** Object:  Table [dbo].[Functions]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Functions](
	[ID_Functions] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[ID_Employee] [int] NOT NULL,
 CONSTRAINT [PK_Functions] PRIMARY KEY CLUSTERED 
(
	[ID_Functions] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[functionsView]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[functionsView]
AS
SELECT dbo.Functions.ID_Functions, dbo.Employees.ID_Employee, dbo.Job.ID_Job, dbo.Functions.Title AS Функция, dbo.Employees.Surname AS [Фамилия исполнителя], dbo.Job.Title AS Должность
FROM     dbo.Functions INNER JOIN
                  dbo.Employees ON dbo.Functions.ID_Employee = dbo.Employees.ID_Employee INNER JOIN
                  dbo.Job ON dbo.Employees.ID_Job = dbo.Job.ID_Job
GO
/****** Object:  Table [dbo].[Financial_Plan]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Financial_Plan](
	[ID_Plan] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Plan_Date] [date] NOT NULL,
	[Sum] [decimal](18, 0) NOT NULL,
	[ID_Functions] [int] NOT NULL,
 CONSTRAINT [PK_Financial_Plan] PRIMARY KEY CLUSTERED 
(
	[ID_Plan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[financeView]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[financeView]
AS
SELECT dbo.Financial_Plan.ID_Plan, dbo.Functions.ID_Functions, dbo.Financial_Plan.Title AS Название, dbo.Financial_Plan.Plan_Date AS [Дата формирования], dbo.Financial_Plan.Sum AS [Сумма для реализации], 
                  dbo.Functions.Title AS Функция
FROM     dbo.Financial_Plan INNER JOIN
                  dbo.Functions ON dbo.Financial_Plan.ID_Functions = dbo.Functions.ID_Functions
GO
/****** Object:  View [dbo].[creditView]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[creditView]
AS
SELECT dbo.Client.ID_Client, dbo.Credit_Agreement.ID_Credit_Agreement, dbo.Employees.ID_Employee, dbo.Credit_Agreement.Title AS [Кредитный договор], dbo.Credit_Agreement.Agreement_Date AS [Дата формирования], 
                  dbo.Credit_Agreement.Sum AS [Сумма кредита], dbo.Client.Surname AS [Фамилия клиента], dbo.Employees.Surname AS Утвердил
FROM     dbo.Client INNER JOIN
                  dbo.Credit_Agreement ON dbo.Client.ID_Client = dbo.Credit_Agreement.ID_Client INNER JOIN
                  dbo.Employees ON dbo.Credit_Agreement.ID_Employee = dbo.Employees.ID_Employee
GO
/****** Object:  Table [dbo].[Statement]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statement](
	[ID_Statement] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Statement_Date] [date] NOT NULL,
	[ID_Plan] [int] NOT NULL,
	[ID_Credit] [int] NOT NULL,
 CONSTRAINT [PK_Statement] PRIMARY KEY CLUSTERED 
(
	[ID_Statement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[buhView]    Script Date: 13.04.2022 12:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[buhView]
AS
SELECT dbo.Credit_Agreement.ID_Credit_Agreement, dbo.Statement.ID_Statement, dbo.Financial_Plan.ID_Plan, dbo.Statement.Title AS Отчётность, dbo.Statement.Statement_Date AS [Дата формирования], 
                  dbo.Financial_Plan.Sum AS Расходы, dbo.Credit_Agreement.Sum AS Доходы
FROM     dbo.Credit_Agreement INNER JOIN
                  dbo.Statement ON dbo.Credit_Agreement.ID_Credit_Agreement = dbo.Statement.ID_Credit INNER JOIN
                  dbo.Financial_Plan ON dbo.Statement.ID_Plan = dbo.Financial_Plan.ID_Plan
GO
SET IDENTITY_INSERT [dbo].[Client] ON 

INSERT [dbo].[Client] ([ID_Client], [Surname], [Name], [Middle_Name], [Birthday]) VALUES (1, N'петрова', N'анна', N'ивановна', CAST(N'2003-12-23' AS Date))
INSERT [dbo].[Client] ([ID_Client], [Surname], [Name], [Middle_Name], [Birthday]) VALUES (3, N'васнецов', N'иван', N'петрович', CAST(N'2002-04-04' AS Date))
INSERT [dbo].[Client] ([ID_Client], [Surname], [Name], [Middle_Name], [Birthday]) VALUES (6, N'петрова', N'мария', N'васильевна', CAST(N'2001-09-09' AS Date))
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Credit_Agreement] ON 

INSERT [dbo].[Credit_Agreement] ([ID_Credit_Agreement], [Title], [Agreement_Date], [Sum], [ID_Client], [ID_Employee]) VALUES (1, N'Ипотечный кредит', CAST(N'2020-02-06' AS Date), CAST(1000000 AS Decimal(18, 0)), 1, 7)
INSERT [dbo].[Credit_Agreement] ([ID_Credit_Agreement], [Title], [Agreement_Date], [Sum], [ID_Client], [ID_Employee]) VALUES (2, N'Автокредит', CAST(N'2021-02-06' AS Date), CAST(500000 AS Decimal(18, 0)), 3, 7)
INSERT [dbo].[Credit_Agreement] ([ID_Credit_Agreement], [Title], [Agreement_Date], [Sum], [ID_Client], [ID_Employee]) VALUES (3, N'Краткосрочный', CAST(N'2022-01-01' AS Date), CAST(10000 AS Decimal(18, 0)), 6, 7)
SET IDENTITY_INSERT [dbo].[Credit_Agreement] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([ID_Employee], [Surname], [Name], [Middle_Name], [Birthday], [Login], [Password], [ID_Job]) VALUES (1, N'петров', N'василий', N'иванович', CAST(N'2003-12-23' AS Date), N'admin1@', N'admin1@', 1)
INSERT [dbo].[Employees] ([ID_Employee], [Surname], [Name], [Middle_Name], [Birthday], [Login], [Password], [ID_Job]) VALUES (2, N'ивановна', N'полина', N'алексеевна', CAST(N'2003-12-23' AS Date), N'market1@', N'market1@', 2)
INSERT [dbo].[Employees] ([ID_Employee], [Surname], [Name], [Middle_Name], [Birthday], [Login], [Password], [ID_Job]) VALUES (3, N'круглова', N'анна', N'ивановна', CAST(N'2002-07-09' AS Date), N'clients1@', N'clients1@', 3)
INSERT [dbo].[Employees] ([ID_Employee], [Surname], [Name], [Middle_Name], [Birthday], [Login], [Password], [ID_Job]) VALUES (4, N'молчанова', N'ада', N'сергеевна', CAST(N'2001-09-09' AS Date), N'finance1@', N'finance1@', 4)
INSERT [dbo].[Employees] ([ID_Employee], [Surname], [Name], [Middle_Name], [Birthday], [Login], [Password], [ID_Job]) VALUES (5, N'колос', N'иннокентий', N'владиславович', CAST(N'1999-12-16' AS Date), N'program1@', N'program1@', 5)
INSERT [dbo].[Employees] ([ID_Employee], [Surname], [Name], [Middle_Name], [Birthday], [Login], [Password], [ID_Job]) VALUES (6, N'сверлова', N'мария', N'васильевна', CAST(N'1999-01-16' AS Date), N'buh1@', N'buh1@', 6)
INSERT [dbo].[Employees] ([ID_Employee], [Surname], [Name], [Middle_Name], [Birthday], [Login], [Password], [ID_Job]) VALUES (7, N'горкова', N'алевтина', N'петровна', CAST(N'1991-05-05' AS Date), N'credit1@', N'credit1@', 8)
INSERT [dbo].[Employees] ([ID_Employee], [Surname], [Name], [Middle_Name], [Birthday], [Login], [Password], [ID_Job]) VALUES (9, N'василькова', N'ирина', N'николаевна', CAST(N'2003-12-23' AS Date), N'program1@', N'program1@', 5)
INSERT [dbo].[Employees] ([ID_Employee], [Surname], [Name], [Middle_Name], [Birthday], [Login], [Password], [ID_Job]) VALUES (10, N'майская', N'оксана', N'олеговна', CAST(N'2002-09-09' AS Date), N'program1@', N'program1@', 5)
INSERT [dbo].[Employees] ([ID_Employee], [Surname], [Name], [Middle_Name], [Birthday], [Login], [Password], [ID_Job]) VALUES (12, N'волконский', N'андрей', N'непомню', CAST(N'2003-02-08' AS Date), N'bolkon!1', N'bolkon!1', 5)
INSERT [dbo].[Employees] ([ID_Employee], [Surname], [Name], [Middle_Name], [Birthday], [Login], [Password], [ID_Job]) VALUES (13, N'Фамилия', N'Имя', N'Отчество', CAST(N'2003-12-31' AS Date), N'1p@', N'1p@', 5)
INSERT [dbo].[Employees] ([ID_Employee], [Surname], [Name], [Middle_Name], [Birthday], [Login], [Password], [ID_Job]) VALUES (15, N'майская', N'оксана', N'олеговна', CAST(N'2003-12-31' AS Date), N'program1#', N'program1@', 1)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Financial_Plan] ON 

INSERT [dbo].[Financial_Plan] ([ID_Plan], [Title], [Plan_Date], [Sum], [ID_Functions]) VALUES (1, N'Краткосрочный финансовый план', CAST(N'2021-02-06' AS Date), CAST(100000 AS Decimal(18, 0)), 2)
INSERT [dbo].[Financial_Plan] ([ID_Plan], [Title], [Plan_Date], [Sum], [ID_Functions]) VALUES (2, N'Среднесрочный финансовый план', CAST(N'2018-02-06' AS Date), CAST(100000 AS Decimal(18, 0)), 3)
INSERT [dbo].[Financial_Plan] ([ID_Plan], [Title], [Plan_Date], [Sum], [ID_Functions]) VALUES (3, N'Долгосрочный финансовый план', CAST(N'2010-02-06' AS Date), CAST(100000 AS Decimal(18, 0)), 4)
SET IDENTITY_INSERT [dbo].[Financial_Plan] OFF
GO
SET IDENTITY_INSERT [dbo].[Functions] ON 

INSERT [dbo].[Functions] ([ID_Functions], [Title], [ID_Employee]) VALUES (2, N'Реализация авторизации', 5)
INSERT [dbo].[Functions] ([ID_Functions], [Title], [ID_Employee]) VALUES (3, N'Автоматизация банковских процедур', 9)
INSERT [dbo].[Functions] ([ID_Functions], [Title], [ID_Employee]) VALUES (4, N'Реализация платёжной системы', 10)
INSERT [dbo].[Functions] ([ID_Functions], [Title], [ID_Employee]) VALUES (7, N'Онлайн перевод валюты', 12)
SET IDENTITY_INSERT [dbo].[Functions] OFF
GO
SET IDENTITY_INSERT [dbo].[Job] ON 

INSERT [dbo].[Job] ([ID_Job], [Title], [Sum]) VALUES (1, N'администратор', CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[Job] ([ID_Job], [Title], [Sum]) VALUES (2, N'менеджер отдела маркетинга', CAST(60000 AS Decimal(18, 0)))
INSERT [dbo].[Job] ([ID_Job], [Title], [Sum]) VALUES (3, N'менеджер отдела клиентов', CAST(60000 AS Decimal(18, 0)))
INSERT [dbo].[Job] ([ID_Job], [Title], [Sum]) VALUES (4, N'менеджер финансового отдела', CAST(60000 AS Decimal(18, 0)))
INSERT [dbo].[Job] ([ID_Job], [Title], [Sum]) VALUES (5, N'программист', CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[Job] ([ID_Job], [Title], [Sum]) VALUES (6, N'бухгалтер', CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[Job] ([ID_Job], [Title], [Sum]) VALUES (8, N'менеджер кредитного отдела', CAST(60000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Job] OFF
GO
SET IDENTITY_INSERT [dbo].[marketPlan] ON 

INSERT [dbo].[marketPlan] ([ID_Market], [Title], [Market_Date], [Sum], [ID_Service]) VALUES (1, N'Маркетинговый план №1', CAST(N'2020-02-02' AS Date), CAST(100000 AS Decimal(18, 0)), 1)
INSERT [dbo].[marketPlan] ([ID_Market], [Title], [Market_Date], [Sum], [ID_Service]) VALUES (2, N'Маркетинговый план №2', CAST(N'2021-02-02' AS Date), CAST(100000 AS Decimal(18, 0)), 2)
INSERT [dbo].[marketPlan] ([ID_Market], [Title], [Market_Date], [Sum], [ID_Service]) VALUES (3, N'Маркетинговый план №3', CAST(N'2022-02-02' AS Date), CAST(100000 AS Decimal(18, 0)), 3)
SET IDENTITY_INSERT [dbo].[marketPlan] OFF
GO
SET IDENTITY_INSERT [dbo].[Service] ON 

INSERT [dbo].[Service] ([ID_Service], [Title], [Price], [ID_Client]) VALUES (1, N'Блокировка карты', CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[Service] ([ID_Service], [Title], [Price], [ID_Client]) VALUES (2, N'Разблокировка карты', CAST(0 AS Decimal(18, 0)), 3)
INSERT [dbo].[Service] ([ID_Service], [Title], [Price], [ID_Client]) VALUES (3, N'Продажа акции', CAST(100 AS Decimal(18, 0)), 6)
SET IDENTITY_INSERT [dbo].[Service] OFF
GO
SET IDENTITY_INSERT [dbo].[Statement] ON 

INSERT [dbo].[Statement] ([ID_Statement], [Title], [Statement_Date], [ID_Plan], [ID_Credit]) VALUES (1, N'Финансовая отчётность №1', CAST(N'2020-02-06' AS Date), 1, 1)
INSERT [dbo].[Statement] ([ID_Statement], [Title], [Statement_Date], [ID_Plan], [ID_Credit]) VALUES (2, N'Финансовая отчётность №2', CAST(N'2021-02-06' AS Date), 2, 2)
INSERT [dbo].[Statement] ([ID_Statement], [Title], [Statement_Date], [ID_Plan], [ID_Credit]) VALUES (3, N'Финансовая отчётность ', CAST(N'2022-02-06' AS Date), 3, 3)
INSERT [dbo].[Statement] ([ID_Statement], [Title], [Statement_Date], [ID_Plan], [ID_Credit]) VALUES (6, N'Название отчётности', CAST(N'2022-02-16' AS Date), 1, 1)
SET IDENTITY_INSERT [dbo].[Statement] OFF
GO
ALTER TABLE [dbo].[Credit_Agreement]  WITH CHECK ADD  CONSTRAINT [FK_Credit_Agreement_Client] FOREIGN KEY([ID_Client])
REFERENCES [dbo].[Client] ([ID_Client])
GO
ALTER TABLE [dbo].[Credit_Agreement] CHECK CONSTRAINT [FK_Credit_Agreement_Client]
GO
ALTER TABLE [dbo].[Credit_Agreement]  WITH CHECK ADD  CONSTRAINT [FK_Credit_Agreement_Employees] FOREIGN KEY([ID_Employee])
REFERENCES [dbo].[Employees] ([ID_Employee])
GO
ALTER TABLE [dbo].[Credit_Agreement] CHECK CONSTRAINT [FK_Credit_Agreement_Employees]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Job] FOREIGN KEY([ID_Job])
REFERENCES [dbo].[Job] ([ID_Job])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Job]
GO
ALTER TABLE [dbo].[Financial_Plan]  WITH CHECK ADD  CONSTRAINT [FK_Financial_Plan_Functions] FOREIGN KEY([ID_Functions])
REFERENCES [dbo].[Functions] ([ID_Functions])
GO
ALTER TABLE [dbo].[Financial_Plan] CHECK CONSTRAINT [FK_Financial_Plan_Functions]
GO
ALTER TABLE [dbo].[Functions]  WITH CHECK ADD  CONSTRAINT [FK_Functions_Employees] FOREIGN KEY([ID_Employee])
REFERENCES [dbo].[Employees] ([ID_Employee])
GO
ALTER TABLE [dbo].[Functions] CHECK CONSTRAINT [FK_Functions_Employees]
GO
ALTER TABLE [dbo].[marketPlan]  WITH CHECK ADD  CONSTRAINT [FK_marketPlan_Service] FOREIGN KEY([ID_Service])
REFERENCES [dbo].[Service] ([ID_Service])
GO
ALTER TABLE [dbo].[marketPlan] CHECK CONSTRAINT [FK_marketPlan_Service]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_Client] FOREIGN KEY([ID_Client])
REFERENCES [dbo].[Client] ([ID_Client])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_Client]
GO
ALTER TABLE [dbo].[Statement]  WITH CHECK ADD  CONSTRAINT [FK_Statement_Credit_Agreement1] FOREIGN KEY([ID_Credit])
REFERENCES [dbo].[Credit_Agreement] ([ID_Credit_Agreement])
GO
ALTER TABLE [dbo].[Statement] CHECK CONSTRAINT [FK_Statement_Credit_Agreement1]
GO
ALTER TABLE [dbo].[Statement]  WITH CHECK ADD  CONSTRAINT [FK_Statement_Financial_Plan] FOREIGN KEY([ID_Plan])
REFERENCES [dbo].[Financial_Plan] ([ID_Plan])
GO
ALTER TABLE [dbo].[Statement] CHECK CONSTRAINT [FK_Statement_Financial_Plan]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Credit_Agreement"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 284
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Financial_Plan"
            Begin Extent = 
               Top = 17
               Left = 608
               Bottom = 180
               Right = 809
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Statement"
            Begin Extent = 
               Top = 38
               Left = 334
               Bottom = 201
               Right = 535
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'buhView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'buhView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Client"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Credit_Agreement"
            Begin Extent = 
               Top = 70
               Left = 277
               Bottom = 233
               Right = 513
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employees"
            Begin Extent = 
               Top = 22
               Left = 544
               Bottom = 185
               Right = 745
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 12
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'creditView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'creditView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[46] 4[14] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Client"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Credit_Agreement"
            Begin Extent = 
               Top = 7
               Left = 297
               Bottom = 170
               Right = 533
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Employees"
            Begin Extent = 
               Top = 92
               Left = 605
               Bottom = 255
               Right = 806
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Service"
            Begin Extent = 
               Top = 196
               Left = 302
               Bottom = 359
               Right = 503
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1608
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Customer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'   End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Customer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Customer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Employees"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Job"
            Begin Extent = 
               Top = 7
               Left = 297
               Bottom = 148
               Right = 498
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'employees_job'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'employees_job'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[10] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Financial_Plan"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Functions"
            Begin Extent = 
               Top = 7
               Left = 297
               Bottom = 148
               Right = 498
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'financeView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'financeView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Functions"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 148
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employees"
            Begin Extent = 
               Top = 7
               Left = 297
               Bottom = 170
               Right = 498
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Job"
            Begin Extent = 
               Top = 7
               Left = 546
               Bottom = 148
               Right = 747
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'functionsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'functionsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Employees"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Planned_Service"
            Begin Extent = 
               Top = 7
               Left = 546
               Bottom = 170
               Right = 785
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1920
         Width = 1452
         Width = 1200
         Width = 1464
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Market'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Market'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "marketPlan"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Service"
            Begin Extent = 
               Top = 7
               Left = 297
               Bottom = 170
               Right = 498
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'marketView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'marketView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Client"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Service"
            Begin Extent = 
               Top = 7
               Left = 297
               Bottom = 170
               Right = 498
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'serviceView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'serviceView'
GO
USE [master]
GO
ALTER DATABASE [BankDB] SET  READ_WRITE 
GO
