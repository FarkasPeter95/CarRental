USE [master]
GO
CREATE DATABASE [CarRental]

ALTER DATABASE [CarRental] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CarRental].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CarRental] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CarRental] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CarRental] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CarRental] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CarRental] SET ARITHABORT OFF 
GO
ALTER DATABASE [CarRental] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CarRental] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CarRental] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CarRental] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CarRental] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CarRental] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CarRental] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CarRental] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CarRental] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CarRental] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CarRental] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CarRental] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CarRental] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CarRental] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CarRental] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CarRental] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CarRental] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CarRental] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CarRental] SET  MULTI_USER 
GO
ALTER DATABASE [CarRental] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CarRental] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CarRental] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CarRental] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CarRental] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CarRental] SET QUERY_STORE = OFF
GO
USE [CarRental]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rental](
	[RentalID] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [int] NOT NULL,
	[CarID] [int] NOT NULL,
	[DateTime] [datetime] NULL,
	[Status] [bit] NULL,
	[DateOut] [datetime] NULL,
	[KmsOut] [int] NULL,
	[KmsIn] [int] NULL,
	[KmsDriven] [int] NULL,
	[DateIn] [datetime] NULL,
	[RentType] [nchar](10) NULL,
	[Duration] [int] NULL,
	[Cost] [int] NULL,
	[Discount] [int] NULL,
	[Total] [int] NULL,
	[Advance] [int] NULL,
	[Balance] [int] NULL,
	[IssueBy] [int] NULL,
 CONSTRAINT [PK_Rental] PRIMARY KEY CLUSTERED 
(
	[RentalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](40) NOT NULL,
	[FullName] [nvarchar](40) NULL,
	[PasswordHash] [binary](64) NOT NULL,
	[Salt] [uniqueidentifier] NULL,
	[PhoneNumber] [char](12) NULL,
 CONSTRAINT [PK_User2] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ClientID] [int] IDENTITY(1,1) NOT NULL,
	[IdCardNumber] [char](8) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[ZipCode] [char](4) NOT NULL,
	[City] [nvarchar](30) NOT NULL,
	[Adress] [nvarchar](50) NULL,
	[PhoneNumber] [char](12) NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[CarID] [int] IDENTITY(1,1) NOT NULL,
	[LicensePlate] [nvarchar](7) NULL,
	[Manufacturer] [nvarchar](50) NULL,
	[Model] [nvarchar](50) NULL,
	[Year] [int] NOT NULL,
	[KmClock] [int] NOT NULL,
	[Fuel] [nvarchar](50) NULL,
	[Color] [nvarchar](50) NOT NULL,
	[Seats] [int] NULL,
	[VIN] [nvarchar](17) NULL,
	[RentalPricePerDay] [int] NOT NULL,
	[RentalPricePerHour] [int] NULL,
	[Available] [bit] NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED 
(
	[CarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[RentalsView]
AS
SELECT        dbo.Rental.RentalID, dbo.Client.IdCardNumber, dbo.Client.Name, dbo.Client.ZipCode, dbo.Client.Adress, dbo.Client.City, dbo.Client.PhoneNumber, dbo.Car.LicensePlate, dbo.Car.Manufacturer, dbo.Car.Model, 
                         dbo.Rental.DateTime, CASE WHEN dbo.Rental.Status = '0' THEN 'Függőben' ELSE 'Lezárt' END AS Status, dbo.Rental.DateOut, dbo.Rental.KmsOut, dbo.Rental.KmsIn, dbo.Rental.KmsDriven, dbo.Rental.DateIn, 
                         dbo.Rental.RentType, dbo.Rental.Duration, dbo.Rental.Cost, dbo.Rental.Discount, dbo.Rental.Advance, dbo.Rental.Total, dbo.Rental.Balance, dbo.[User].FullName, dbo.Car.VIN, dbo.Car.Color, dbo.Car.Fuel, dbo.Car.Seats, 
                         dbo.Car.Year, dbo.Car.RentalPricePerDay, dbo.Car.RentalPricePerHour
FROM            dbo.Car INNER JOIN
                         dbo.Rental ON dbo.Car.CarID = dbo.Rental.CarID INNER JOIN
                         dbo.Client ON dbo.Rental.ClientID = dbo.Client.ClientID INNER JOIN
                         dbo.[User] ON dbo.Rental.IssueBy = dbo.[User].UserID
GO
SET IDENTITY_INSERT [dbo].[Car] ON 
GO
INSERT [dbo].[Car] ([CarID], [LicensePlate], [Manufacturer], [Model], [Year], [KmClock], [Fuel], [Color], [Seats], [VIN], [RentalPricePerDay], [RentalPricePerHour], [Available], [Image]) VALUES (1, N'NSF-123', N'BMW', N'M3', 1998, 125452, N'Diesel', N'fekete', 5, N'KH123548542489524', 35000, 8000, 0, N'')
GO
INSERT [dbo].[Car] ([CarID], [LicensePlate], [Manufacturer], [Model], [Year], [KmClock], [Fuel], [Color], [Seats], [VIN], [RentalPricePerDay], [RentalPricePerHour], [Available], [Image]) VALUES (2, N'TER-001', N'BMW', N'M4', 2017, 65757, N'Benzin', N'ezüst', 4, N'PK483548542489524', 20000, 7000, 0, N'C:/CarRentalImages/2.jpg')
GO
INSERT [dbo].[Car] ([CarID], [LicensePlate], [Manufacturer], [Model], [Year], [KmClock], [Fuel], [Color], [Seats], [VIN], [RentalPricePerDay], [RentalPricePerHour], [Available], [Image]) VALUES (3, N'SDF-489', N'Audi', N'A8', 2008, 12600, N'Benzin', N'fehér', 5, N'RG983548542489524', 28000, 7500, 1, N'')
GO
INSERT [dbo].[Car] ([CarID], [LicensePlate], [Manufacturer], [Model], [Year], [KmClock], [Fuel], [Color], [Seats], [VIN], [RentalPricePerDay], [RentalPricePerHour], [Available], [Image]) VALUES (4, N'GLE-222', N'Volkswagen', N'Golf', 1991, 89685, N'Diesel', N'zöld', 5, N'WE896548542489524', 8500, 2000, 1, N'C:/CarRentalImages/4.jpg')
GO
INSERT [dbo].[Car] ([CarID], [LicensePlate], [Manufacturer], [Model], [Year], [KmClock], [Fuel], [Color], [Seats], [VIN], [RentalPricePerDay], [RentalPricePerHour], [Available], [Image]) VALUES (5, N'LHK-850', N'Toyota', N'Prius', 2012, 65120, N'Hibrid', N'fehér', 5, N'VB456548542489524', 35000, 4000, 1, N'')
GO
INSERT [dbo].[Car] ([CarID], [LicensePlate], [Manufacturer], [Model], [Year], [KmClock], [Fuel], [Color], [Seats], [VIN], [RentalPricePerDay], [RentalPricePerHour], [Available], [Image]) VALUES (10, N'LJK-687', N'Ferrari', N'LaFerrari', 2013, 46874, N'Benzin', N'piros', 2, N'WE851478542489524', 50000, 13000, 1, N'')
GO
INSERT [dbo].[Car] ([CarID], [LicensePlate], [Manufacturer], [Model], [Year], [KmClock], [Fuel], [Color], [Seats], [VIN], [RentalPricePerDay], [RentalPricePerHour], [Available], [Image]) VALUES (12, N'IKL-687', N'Suzuki', N'Vitara', 2016, 356874, N'Benzin', N'kék', 7, N'SD946748542489524', 18500, 1900, 1, N'')
GO
INSERT [dbo].[Car] ([CarID], [LicensePlate], [Manufacturer], [Model], [Year], [KmClock], [Fuel], [Color], [Seats], [VIN], [RentalPricePerDay], [RentalPricePerHour], [Available], [Image]) VALUES (15, N'DFG-687', N'Opel', N'Astra', 1995, 66120, N'Diesel', N'sárga', 5, N'PL565235485424895', 4000, 1000, 1, N'')
GO
INSERT [dbo].[Car] ([CarID], [LicensePlate], [Manufacturer], [Model], [Year], [KmClock], [Fuel], [Color], [Seats], [VIN], [RentalPricePerDay], [RentalPricePerHour], [Available], [Image]) VALUES (16, N'KSA-658', N'Skoda', N'Fabia', 2017, 357500, N'Benzin', N'fehér', 5, N'TD785248542489524', 16000, 6000, 0, N'C:/CarRentalImages/16.jpg')
GO
INSERT [dbo].[Car] ([CarID], [LicensePlate], [Manufacturer], [Model], [Year], [KmClock], [Fuel], [Color], [Seats], [VIN], [RentalPricePerDay], [RentalPricePerHour], [Available], [Image]) VALUES (17, N'DFG-901', N'Volkswagen', N'Up', 2015, 9875, N'Elektromos', N'fehér', 4, N'UP562354854248952', 12000, 4500, 1, N'')
GO
SET IDENTITY_INSERT [dbo].[Car] OFF
GO
SET IDENTITY_INSERT [dbo].[Client] ON 
GO
INSERT [dbo].[Client] ([ClientID], [IdCardNumber], [Name], [BirthDate], [ZipCode], [City], [Adress], [PhoneNumber]) VALUES (1, N'123654SA', N'Kiss István', CAST(N'1985-06-11' AS Date), N'9700', N'Szombathely', N'Kossuth u. 56.', N'+36705684524')
GO
INSERT [dbo].[Client] ([ClientID], [IdCardNumber], [Name], [BirthDate], [ZipCode], [City], [Adress], [PhoneNumber]) VALUES (2, N'BH456789', N'Tóth János', CAST(N'1966-02-08' AS Date), N'1136', N'Budapest', N'Petőfi u. 22.', N'+36301111111')
GO
INSERT [dbo].[Client] ([ClientID], [IdCardNumber], [Name], [BirthDate], [ZipCode], [City], [Adress], [PhoneNumber]) VALUES (3, N'968455KJ', N'Kovács Ferenc', CAST(N'1979-03-22' AS Date), N'6580', N'Keszthely', N'Virág u. 12.', N'+36207421587')
GO
INSERT [dbo].[Client] ([ClientID], [IdCardNumber], [Name], [BirthDate], [ZipCode], [City], [Adress], [PhoneNumber]) VALUES (4, N'465657FD', N'Horváth József', CAST(N'1960-11-15' AS Date), N'4568', N'Pécs', N'Kerék u 5.', N'+36705566878')
GO
INSERT [dbo].[Client] ([ClientID], [IdCardNumber], [Name], [BirthDate], [ZipCode], [City], [Adress], [PhoneNumber]) VALUES (13, N'123256DF', N'Bíró Géza', CAST(N'1990-10-11' AS Date), N'3548', N'Csorna', N'Vasút u. 2.', N'+36307894565')
GO
INSERT [dbo].[Client] ([ClientID], [IdCardNumber], [Name], [BirthDate], [ZipCode], [City], [Adress], [PhoneNumber]) VALUES (20, N'456887FG', N'Kovács Roland', CAST(N'1972-08-19' AS Date), N'9795', N'Ják', N'Dózsa u. 8.', N'+36706542859')
GO
INSERT [dbo].[Client] ([ClientID], [IdCardNumber], [Name], [BirthDate], [ZipCode], [City], [Adress], [PhoneNumber]) VALUES (59, N'PL562214', N'Horváth Dezső', CAST(N'1990-06-13' AS Date), N'8567', N'Miskolc', N'Petőfi u. 11.', N'+36306598745')
GO
INSERT [dbo].[Client] ([ClientID], [IdCardNumber], [Name], [BirthDate], [ZipCode], [City], [Adress], [PhoneNumber]) VALUES (67, N'RT456544', N'Kiss István', CAST(N'1979-03-21' AS Date), N'4562', N'Sopron', N'Rózsa u. 22.', N'+36208795425')
GO
INSERT [dbo].[Client] ([ClientID], [IdCardNumber], [Name], [BirthDate], [ZipCode], [City], [Adress], [PhoneNumber]) VALUES (82, N'RT456545', N'Fehér Balázs', CAST(N'1990-02-02' AS Date), N'4564', N'Szeged', N'Kossuth. u. 11.', N'+36304565445')
GO
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Rental] ON 
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (100, 4, 3, CAST(N'2019-09-15T11:12:00.000' AS DateTime), 0, CAST(N'2019-09-15T11:19:00.000' AS DateTime), 1234, 1290, 56, CAST(N'2019-09-16T12:11:00.000' AS DateTime), N'Day       ', 1, 84000, 1000, 83000, 40000, 0, 2)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (101, 1, 15, CAST(N'2019-10-11T15:20:00.000' AS DateTime), 1, CAST(N'2019-10-11T15:55:00.000' AS DateTime), 54000, 54120, 120, CAST(N'2019-10-11T18:55:00.000' AS DateTime), N'Hour      ', 3, 12000, 2000, 10000, 5000, 0, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (140, 2, 10, CAST(N'2019-10-20T22:33:52.957' AS DateTime), 0, CAST(N'2019-10-20T00:00:00.000' AS DateTime), 46874, 46974, 100, CAST(N'2019-10-23T22:30:55.867' AS DateTime), N'Day       ', 3, 150000, 1000, 149000, 5000, 144000, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (141, 13, 4, CAST(N'2019-10-20T22:40:37.740' AS DateTime), 0, CAST(N'2019-10-22T00:00:00.000' AS DateTime), 89658, 89858, 200, CAST(N'2019-10-20T22:37:19.353' AS DateTime), N'Day       ', 5, 42500, 1000, 41500, 10000, 31500, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (142, 3, 15, CAST(N'2019-10-20T22:50:17.007' AS DateTime), 0, CAST(N'2019-10-19T00:00:00.000' AS DateTime), 65765, 66775, 1010, CAST(N'2019-10-20T22:48:54.743' AS DateTime), N'Hour      ', 4, 4000, 2000, 2000, 5000, -3000, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (143, 67, 16, CAST(N'2019-10-20T22:57:38.830' AS DateTime), 0, CAST(N'2019-10-23T00:00:00.000' AS DateTime), 357482, 357502, 20, CAST(N'2019-10-20T22:56:42.223' AS DateTime), N'Day       ', 10, 160000, 2000, 158000, 5000, 153000, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (145, 3, 2, CAST(N'2019-10-21T20:04:33.537' AS DateTime), 0, CAST(N'2019-10-21T20:04:16.507' AS DateTime), 65757, NULL, NULL, NULL, N'Day       ', 3, 60000, 1000, 59000, 5000, 54000, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (148, 2, 3, CAST(N'2019-10-22T21:56:54.463' AS DateTime), 0, CAST(N'2019-10-22T21:56:41.110' AS DateTime), 12545, NULL, NULL, NULL, N'Day       ', 3, 84000, 1000, 83000, 2000, 81000, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (149, 3, 4, CAST(N'2019-10-22T22:22:16.227' AS DateTime), 0, CAST(N'2019-10-22T22:21:48.337' AS DateTime), 89658, NULL, NULL, NULL, N'Day       ', 3, 25500, 1000, 24500, 10000, 14500, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (150, 3, 5, CAST(N'2019-10-27T18:45:39.643' AS DateTime), 0, CAST(N'2019-10-22T22:24:54.967' AS DateTime), 65052, 65120, 68, CAST(N'2019-10-27T18:24:43.000' AS DateTime), N'Day       ', 5, 175000, 5000, 170000, 170000, 0, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (151, 4, 4, CAST(N'2019-10-22T22:30:28.423' AS DateTime), 0, CAST(N'2019-10-22T22:30:06.063' AS DateTime), 89658, NULL, NULL, NULL, N'Day       ', 4, 34000, 2000, 32000, 15000, 17000, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (152, 4, 4, CAST(N'2019-10-23T21:37:35.457' AS DateTime), 0, CAST(N'2019-10-23T21:37:03.797' AS DateTime), 89658, 89685, 27, CAST(N'2019-10-24T06:38:24.000' AS DateTime), N'Hour      ', 10, 20000, 1000, 19000, 19000, 0, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (153, 3, 12, CAST(N'2019-10-24T20:37:11.503' AS DateTime), 0, CAST(N'2019-10-24T20:36:23.913' AS DateTime), 356874, NULL, NULL, NULL, N'Day       ', 5, 92500, 2000, 90500, 10000, 80500, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (154, 2, 15, CAST(N'2019-10-26T18:08:28.350' AS DateTime), 0, CAST(N'2019-10-26T18:07:20.693' AS DateTime), 65765, NULL, NULL, NULL, N'Day       ', 3, 12000, 500, 11500, 1000, 10500, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (155, 3, 3, CAST(N'2019-10-26T22:09:22.143' AS DateTime), 0, CAST(N'2019-10-26T22:08:56.343' AS DateTime), 12545, 12855, 310, CAST(N'2019-10-29T22:09:38.000' AS DateTime), N'Day       ', 4, 112000, 5000, 107000, 107000, 0, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (156, 4, 16, CAST(N'2019-10-26T22:32:48.177' AS DateTime), 0, CAST(N'2019-10-26T22:32:20.567' AS DateTime), 357482, 357500, 18, CAST(N'2019-10-29T22:35:30.000' AS DateTime), N'Day       ', 4, 64000, 2000, 62000, 62000, 0, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (157, 3, 15, CAST(N'2019-10-26T22:43:34.937' AS DateTime), 0, CAST(N'2019-10-26T22:43:10.840' AS DateTime), 65765, 66120, 355, CAST(N'2019-10-31T22:47:52.000' AS DateTime), N'Day       ', 6, 24000, 3000, 21000, 21000, 0, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (158, 1, 3, CAST(N'2019-10-27T00:48:25.933' AS DateTime), 0, CAST(N'2019-10-27T00:47:27.823' AS DateTime), 12545, 12600, 55, CAST(N'2019-10-31T00:49:18.000' AS DateTime), N'Day       ', 5, 140000, 3000, 137000, 137000, 0, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (159, 4, 1, CAST(N'2019-10-27T18:16:45.727' AS DateTime), 0, CAST(N'2019-10-27T18:16:27.363' AS DateTime), 125452, NULL, NULL, NULL, N'Day       ', 2, 70000, 5000, 65000, 10000, 55000, 1)
GO
INSERT [dbo].[Rental] ([RentalID], [ClientID], [CarID], [DateTime], [Status], [DateOut], [KmsOut], [KmsIn], [KmsDriven], [DateIn], [RentType], [Duration], [Cost], [Discount], [Total], [Advance], [Balance], [IssueBy]) VALUES (1152, 4, 2, CAST(N'2019-11-02T22:01:55.650' AS DateTime), 0, CAST(N'2019-11-02T22:01:42.333' AS DateTime), 65757, NULL, NULL, NULL, N'Day       ', 3, 60000, 1000, 59000, 50000, 9000, 1)
GO
SET IDENTITY_INSERT [dbo].[Rental] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserID], [UserName], [FullName], [PasswordHash], [Salt], [PhoneNumber]) VALUES (1, N'fpeter', N'Farkas Péter', 0x922F3183C376BDD51BCACFE0BFAA962C84222B18EF6347FEF010900C03171F75101BD05883BAC4904931C17B2311FCB0AFF8C804170AB443AF5223C2F5C7D3CB, N'452e66c7-28c4-4714-b91b-4cb91dbb97c6', N'12213       ')
GO
INSERT [dbo].[User] ([UserID], [UserName], [FullName], [PasswordHash], [Salt], [PhoneNumber]) VALUES (2, N'kbela', N'Kiss Béla', 0xE15CE7CEDCA20DF03EAD436F5FD8D0C3A5D88193D94B398715A3F016CD6A273C329BA9C90E8904B64A81982C7F687D0CF093623BA604106A3EA88F2D556BC370, N'7ecc19d0-58ea-4389-8b33-327361d7af66', N'546456      ')
GO
INSERT [dbo].[User] ([UserID], [UserName], [FullName], [PasswordHash], [Salt], [PhoneNumber]) VALUES (3, N'tkata', N'Takács Kata', 0x7E65D3B7EBA57103F5428AA2EAA5514014B29A2051ADC40FC52F9106C3F23B163B251C839616BFFB657CCA5C4157C9EDEDC59D4ADFDBB6105C18056DFE1CC618, N'cb91cdb1-acfa-4c5e-a727-ab736d6d99ab', N'456123      ')
GO
INSERT [dbo].[User] ([UserID], [UserName], [FullName], [PasswordHash], [Salt], [PhoneNumber]) VALUES (4, N'vargaf', N'Varga Ferenc', 0x177562FBC7239BA1DEF744DADC7C3049B62340B4309A0D486E8623D7C09BCE3C8BAD03AF014FCA63DEE94F44B961C7CFF8A0568C66AD8D3A05DB6B106AF3BED9, N'cb66a955-60b4-4e06-b11c-c3ecd647278f', NULL)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [UC_User] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Rental]  WITH CHECK ADD  CONSTRAINT [FK_Rental_Car] FOREIGN KEY([CarID])
REFERENCES [dbo].[Car] ([CarID])
GO
ALTER TABLE [dbo].[Rental] CHECK CONSTRAINT [FK_Rental_Car]
GO
ALTER TABLE [dbo].[Rental]  WITH CHECK ADD  CONSTRAINT [FK_Rental_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Client] ([ClientID])
GO
ALTER TABLE [dbo].[Rental] CHECK CONSTRAINT [FK_Rental_Client]
GO
ALTER TABLE [dbo].[Rental]  WITH CHECK ADD  CONSTRAINT [FK_Rental_User] FOREIGN KEY([IssueBy])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Rental] CHECK CONSTRAINT [FK_Rental_User]
GO
ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [CK_Car_LicensePlate] CHECK  (([LicensePlate] like '[A-Z][A-Z][A-Z][-][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [CK_Car_LicensePlate]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [CK_Client_BirthDate] CHECK  ((dateadd(year,(18),[BirthDate])<=getdate()))
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [CK_Client_BirthDate]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [CK_Client_IdCardNumber] CHECK  (([IdCardNumber] like '[A-Z][A-Z][0-9][0-9][0-9][0-9][0-9][0-9]' OR [IdCardNumber] like '[0-9][0-9][0-9][0-9][0-9][0-9][A-Z][A-Z]'))
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [CK_Client_IdCardNumber]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [CK_Client_ZipCode] CHECK  (([ZipCode] like '[1-9][0-9][0-9][0-9]' OR [ZipCode] IS NULL))
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [CK_Client_ZipCode]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RentalByID]

	@RentalID int AS
SELECT        dbo.Rental.RentalID, dbo.Client.IdCardNumber, dbo.Client.Name, dbo.Client.ZipCode, dbo.Client.Adress, dbo.Client.City, dbo.Client.PhoneNumber, dbo.Car.LicensePlate, dbo.Car.Manufacturer, dbo.Car.Model, 
                         dbo.Rental.DateTime, CASE WHEN dbo.Rental.Status = '0' THEN 'Függőben' ELSE 'Lezárt' END AS Status, dbo.Rental.DateOut, dbo.Rental.KmsOut, dbo.Rental.KmsIn, dbo.Rental.KmsDriven, dbo.Rental.DateIn, 
                         dbo.Rental.RentType, dbo.Rental.Duration, dbo.Rental.Cost, dbo.Rental.Discount, dbo.Rental.Advance, dbo.Rental.Total, dbo.Rental.Balance, dbo.[User].FullName, dbo.Car.VIN, dbo.Car.Color, dbo.Car.Fuel, dbo.Car.Seats, 
                         dbo.Car.Year, dbo.Car.RentalPricePerDay, dbo.Car.RentalPricePerHour						 
FROM            dbo.Car INNER JOIN
                         dbo.Rental ON dbo.Car.CarID = dbo.Rental.CarID INNER JOIN
                         dbo.Client ON dbo.Rental.ClientID = dbo.Client.ClientID INNER JOIN
                         dbo.[User] ON dbo.Rental.IssueBy = dbo.[User].UserID
WHERE RentalID=@RentalID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAddUser]
    @pUserName NVARCHAR(50), 
    @pPassword NVARCHAR(50),
    @pFullName NVARCHAR(40) = NULL, 
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

        INSERT INTO dbo.[User] (UserName, PasswordHash, Salt, FullName)
        VALUES(@pUserName, HASHBYTES('SHA2_512', @pPassword+CAST(@salt AS NVARCHAR(36))), @salt, @pFullName)

       SET @responseMessage='Success'

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Client', @level2type=N'CONSTRAINT',@level2name=N'CK_Client_ZipCode'
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
         Begin Table = "Car"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "Rental"
            Begin Extent = 
               Top = 166
               Left = 212
               Bottom = 296
               Right = 382
            End
            DisplayFlags = 280
            TopColumn = 14
         End
         Begin Table = "Client"
            Begin Extent = 
               Top = 42
               Left = 457
               Bottom = 172
               Right = 627
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 6
               Left = 268
               Bottom = 136
               Right = 438
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
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1695
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RentalsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RentalsView'
GO
USE [master]
GO
ALTER DATABASE [CarRental] SET  READ_WRITE 
GO
