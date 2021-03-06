USE [master]
GO
/****** Object:  Database [TimeSheetManagementSystemDB_Vue]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE DATABASE [TimeSheetManagementSystemDB_Vue]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TimeSheetManagementSystemDB_Vue', FILENAME = N'D:\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\TimeSheetManagementSystemDB_Vue.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TimeSheetManagementSystemDB_Vue_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\TimeSheetManagementSystemDB_Vue_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TimeSheetManagementSystemDB_Vue].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET ARITHABORT OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET  MULTI_USER 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET QUERY_STORE = OFF
GO
USE [TimeSheetManagementSystemDB_Vue]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountDetail]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountDetail](
	[AccountDetailId] [int] IDENTITY(1,1) NOT NULL,
	[DayOfWeekNumber] [int] NOT NULL,
	[StartTimeInMinutes] [int] NOT NULL,
	[EndTimeInMinutes] [int] NOT NULL,
	[EffectiveStartDate] [datetime2](7) NOT NULL,
	[EffectiveEndDate] [datetime2](7) NULL,
	[IsVisible] [bit] NOT NULL,
	[CustomerAccountId] [int] NOT NULL,
 CONSTRAINT [PrimaryKey_AccountDetailId] PRIMARY KEY CLUSTERED 
(
	[AccountDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountRate]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountRate](
	[AccountRateId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerAccountId] [int] NOT NULL,
	[RatePerHour] [decimal](6, 2) NOT NULL,
	[EffectiveStartDate] [datetime2](7) NOT NULL,
	[EffectiveEndDate] [datetime2](7) NULL,
 CONSTRAINT [PrimaryKey_AccountRateId] PRIMARY KEY CLUSTERED 
(
	[AccountRateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FullName] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerAccount]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerAccount](
	[CustomerAccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [varchar](100) NOT NULL,
	[Comments] [nvarchar](4000) NULL,
	[IsVisible] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[UpdatedById] [int] NOT NULL,
 CONSTRAINT [PrimaryKey_CustomerAccountId] PRIMARY KEY CLUSTERED 
(
	[CustomerAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InstructorAccount]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstructorAccount](
	[InstructorAccountId] [int] IDENTITY(1,1) NOT NULL,
	[InstructorId] [int] NOT NULL,
	[CustomerAccountId] [int] NOT NULL,
	[Comments] [nvarchar](4000) NULL,
	[WageRate] [decimal](5, 2) NOT NULL,
	[EffectiveStartDate] [datetime2](7) NOT NULL,
	[EffectiveEndDate] [datetime2](7) NULL,
	[IsCurrent] [bit] NOT NULL,
 CONSTRAINT [PrimaryKey_InstructorAccounttId] PRIMARY KEY CLUSTERED 
(
	[InstructorAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SessionSynopsis]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionSynopsis](
	[SessionSynopsisId] [int] IDENTITY(1,1) NOT NULL,
	[SessionSynopsisName] [varchar](100) NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[IsVisible] [bit] NOT NULL,
 CONSTRAINT [PrimaryKey_SessionSynopsisId] PRIMARY KEY CLUSTERED 
(
	[SessionSynopsisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSheet]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSheet](
	[TimeSheetId] [int] IDENTITY(1,1) NOT NULL,
	[MonthAndYear] [datetime2](7) NOT NULL,
	[RatePerHour] [decimal](6, 2) NOT NULL,
	[InstructorId] [int] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[VerifiedAndSubmittedAt] [datetime2](7) NULL,
	[CheckedById] [int] NOT NULL,
	[ApprovedById] [int] NOT NULL,
	[ApprovedAt] [datetime2](7) NULL,
 CONSTRAINT [PrimaryKey_TimeSheetId] PRIMARY KEY CLUSTERED 
(
	[TimeSheetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSheetDetail]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSheetDetail](
	[TimeSheetDetailId] [int] IDENTITY(1,1) NOT NULL,
	[TimeSheetId] [int] NOT NULL,
	[AccountDetailId] [int] NOT NULL,
	[SessionSynopsisNames] [varchar](300) NOT NULL,
	[TimeInInMinutes] [int] NOT NULL,
	[TimeOutInMinutes] [int] NOT NULL,
	[DateOfLesson] [datetime2](7) NOT NULL,
	[IsReplacementInstructor] [bit] NOT NULL,
	[WageRatePerHour] [decimal](6, 2) NOT NULL,
	[OfficialTimeInMinutes] [int] NOT NULL,
	[OfficialTimeOutMinutes] [int] NOT NULL,
 CONSTRAINT [PrimaryKey_TimeSheetDetailId] PRIMARY KEY CLUSTERED 
(
	[TimeSheetDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSheetDetailSignature]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSheetDetailSignature](
	[TimeSheetDetailSignatureId] [int] IDENTITY(1,1) NOT NULL,
	[Signature] [varbinary](max) NULL,
	[TimeSheetIDetailId] [int] NOT NULL,
 CONSTRAINT [PrimaryKey_TimeSheetSignatureId] PRIMARY KEY CLUSTERED 
(
	[TimeSheetDetailSignatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 1/2/2019 12:58:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](200) NOT NULL,
	[FullName] [varchar](200) NOT NULL,
	[Username] [varchar](20) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[Roles] [varchar](200) NOT NULL,
 CONSTRAINT [PrimaryKey_UserInfoId] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20181230175949_setupdb', N'2.1.4-rtm-31024')
SET IDENTITY_INSERT [dbo].[AccountDetail] ON 

INSERT [dbo].[AccountDetail] ([AccountDetailId], [DayOfWeekNumber], [StartTimeInMinutes], [EndTimeInMinutes], [EffectiveStartDate], [EffectiveEndDate], [IsVisible], [CustomerAccountId]) VALUES (92, 3, 900, 1200, CAST(N'2019-01-31T00:00:00.0000000' AS DateTime2), NULL, 1, 1)
INSERT [dbo].[AccountDetail] ([AccountDetailId], [DayOfWeekNumber], [StartTimeInMinutes], [EndTimeInMinutes], [EffectiveStartDate], [EffectiveEndDate], [IsVisible], [CustomerAccountId]) VALUES (93, 3, 960, 1200, CAST(N'2019-01-30T00:00:00.0000000' AS DateTime2), CAST(N'2019-02-21T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[AccountDetail] ([AccountDetailId], [DayOfWeekNumber], [StartTimeInMinutes], [EndTimeInMinutes], [EffectiveStartDate], [EffectiveEndDate], [IsVisible], [CustomerAccountId]) VALUES (94, 2, 480, 600, CAST(N'2019-02-01T00:00:00.0000000' AS DateTime2), CAST(N'2019-02-28T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[AccountDetail] ([AccountDetailId], [DayOfWeekNumber], [StartTimeInMinutes], [EndTimeInMinutes], [EffectiveStartDate], [EffectiveEndDate], [IsVisible], [CustomerAccountId]) VALUES (95, 4, 600, 720, CAST(N'2019-02-21T00:00:00.0000000' AS DateTime2), CAST(N'2019-03-28T00:00:00.0000000' AS DateTime2), 1, 4)
INSERT [dbo].[AccountDetail] ([AccountDetailId], [DayOfWeekNumber], [StartTimeInMinutes], [EndTimeInMinutes], [EffectiveStartDate], [EffectiveEndDate], [IsVisible], [CustomerAccountId]) VALUES (96, 5, 480, 720, CAST(N'2019-02-01T00:00:00.0000000' AS DateTime2), CAST(N'2019-02-20T00:00:00.0000000' AS DateTime2), 1, 5)
INSERT [dbo].[AccountDetail] ([AccountDetailId], [DayOfWeekNumber], [StartTimeInMinutes], [EndTimeInMinutes], [EffectiveStartDate], [EffectiveEndDate], [IsVisible], [CustomerAccountId]) VALUES (97, 2, 1080, 1200, CAST(N'2019-01-31T00:00:00.0000000' AS DateTime2), CAST(N'2019-02-08T00:00:00.0000000' AS DateTime2), 1, 4)
SET IDENTITY_INSERT [dbo].[AccountDetail] OFF
SET IDENTITY_INSERT [dbo].[AccountRate] ON 

INSERT [dbo].[AccountRate] ([AccountRateId], [CustomerAccountId], [RatePerHour], [EffectiveStartDate], [EffectiveEndDate]) VALUES (1, 1, CAST(80.00 AS Decimal(6, 2)), CAST(N'2019-01-11T00:00:00.0000000' AS DateTime2), CAST(N'2019-01-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[AccountRate] ([AccountRateId], [CustomerAccountId], [RatePerHour], [EffectiveStartDate], [EffectiveEndDate]) VALUES (5, 3, CAST(70.00 AS Decimal(6, 2)), CAST(N'2019-01-30T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[AccountRate] ([AccountRateId], [CustomerAccountId], [RatePerHour], [EffectiveStartDate], [EffectiveEndDate]) VALUES (6, 1, CAST(90.00 AS Decimal(6, 2)), CAST(N'2019-01-30T00:00:00.0000000' AS DateTime2), CAST(N'2019-01-31T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[AccountRate] ([AccountRateId], [CustomerAccountId], [RatePerHour], [EffectiveStartDate], [EffectiveEndDate]) VALUES (8, 1, CAST(100.00 AS Decimal(6, 2)), CAST(N'2019-02-02T00:00:00.0000000' AS DateTime2), CAST(N'2019-02-28T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[AccountRate] ([AccountRateId], [CustomerAccountId], [RatePerHour], [EffectiveStartDate], [EffectiveEndDate]) VALUES (9, 4, CAST(70.00 AS Decimal(6, 2)), CAST(N'2019-02-01T00:00:00.0000000' AS DateTime2), CAST(N'2019-02-21T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[AccountRate] ([AccountRateId], [CustomerAccountId], [RatePerHour], [EffectiveStartDate], [EffectiveEndDate]) VALUES (10, 5, CAST(75.00 AS Decimal(6, 2)), CAST(N'2019-01-31T00:00:00.0000000' AS DateTime2), CAST(N'2019-02-28T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[AccountRate] OFF
SET IDENTITY_INSERT [dbo].[CustomerAccount] ON 

INSERT [dbo].[CustomerAccount] ([CustomerAccountId], [AccountName], [Comments], [IsVisible], [CreatedAt], [CreatedById], [UpdatedAt], [UpdatedById]) VALUES (1, N'Sch AB', N'Training and prac', 1, CAST(N'2019-01-09T11:45:37.9707745' AS DateTime2), 1, CAST(N'2019-01-31T01:55:36.1361252' AS DateTime2), 1)
INSERT [dbo].[CustomerAccount] ([CustomerAccountId], [AccountName], [Comments], [IsVisible], [CreatedAt], [CreatedById], [UpdatedAt], [UpdatedById]) VALUES (3, N'invisible sch', N'welp', 0, CAST(N'2019-01-30T08:38:06.9103414' AS DateTime2), 1, CAST(N'2019-01-31T00:43:42.6947141' AS DateTime2), 1)
INSERT [dbo].[CustomerAccount] ([CustomerAccountId], [AccountName], [Comments], [IsVisible], [CreatedAt], [CreatedById], [UpdatedAt], [UpdatedById]) VALUES (4, N'Sch B', N'meow', 1, CAST(N'2019-01-31T13:27:40.6179154' AS DateTime2), 1, CAST(N'2019-02-01T00:23:03.6141221' AS DateTime2), 1)
INSERT [dbo].[CustomerAccount] ([CustomerAccountId], [AccountName], [Comments], [IsVisible], [CreatedAt], [CreatedById], [UpdatedAt], [UpdatedById]) VALUES (5, N'Sch C', N'"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."', 1, CAST(N'2019-01-31T13:29:19.9921945' AS DateTime2), 1, CAST(N'2019-01-31T22:09:40.4867321' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[CustomerAccount] OFF
SET IDENTITY_INSERT [dbo].[SessionSynopsis] ON 

INSERT [dbo].[SessionSynopsis] ([SessionSynopsisId], [SessionSynopsisName], [CreatedById], [UpdatedById], [IsVisible]) VALUES (6, N'Practice', 1, 1, 1)
INSERT [dbo].[SessionSynopsis] ([SessionSynopsisId], [SessionSynopsisName], [CreatedById], [UpdatedById], [IsVisible]) VALUES (8, N'Performance', 1, 1, 0)
SET IDENTITY_INSERT [dbo].[SessionSynopsis] OFF
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([Id], [FirstName], [FullName], [Username], [PasswordHash], [PasswordSalt], [Roles]) VALUES (1, N'admin', N'adminuser', N'admin', 0x0970A7AF190113B7708B69238DE41F36B95D8A29F31A5BE76F97F2254DCD3BE7C7413457139F4C6507F70C62DEB8FA1380DB6816E0C0A5704B7B9A0E71727EEE, 0x92AF200B4E97C90527298A2A90C42A8BCCE6211192E6C72A502A056B303B887763DA05EDEB1E85792B45EFFA8B1BDE7D23C7681A95F98E2F0C83B5E6A177A91BB1E47288943F46A44EC79371782A8E1FCAD2E615AAE518F8B6202F30B4B84871C06FF3A9CF79A5C3BF16BDC2BF30CB3F331C64716F00307AC2680FD64A1D35A1, N'Admin')
INSERT [dbo].[UserInfo] ([Id], [FirstName], [FullName], [Username], [PasswordHash], [PasswordSalt], [Roles]) VALUES (2, N'tester', N'testing', N'test', 0xA51135B11D03AD31C48EEDFA833647EE674ADC273414899A55347025DA18F9C59590F5674B5D1B082A66CE44802CC1B75256733187AE396C1A9E169C852AD649, 0x22BA577C5907D765DB74FFC83E7C2AD360086A11DC347494C37500FB7F817AD8D598AE45D00CADA4B43CDE0EE03679FA1E1446B7DDE0126D165F94CF78A65A9055E278D0468864F3EA0CD31455FD8ADA5C2422F6849FC1DC33C515F0941F497634C28CC6C4CA4FAE0609B2DF14125FC54E3A59052532E9A2597EC3B0426A16A8, N'Instructor')
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
/****** Object:  Index [IX_AccountDetail_CustomerAccountId]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_AccountDetail_CustomerAccountId] ON [dbo].[AccountDetail]
(
	[CustomerAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AccountRate_CustomerAccountId]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_AccountRate_CustomerAccountId] ON [dbo].[AccountRate]
(
	[CustomerAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CustomerAccount_AccountName_UniqueConstraint]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [CustomerAccount_AccountName_UniqueConstraint] ON [dbo].[CustomerAccount]
(
	[AccountName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomerAccount_CreatedById]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_CustomerAccount_CreatedById] ON [dbo].[CustomerAccount]
(
	[CreatedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomerAccount_UpdatedById]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_CustomerAccount_UpdatedById] ON [dbo].[CustomerAccount]
(
	[UpdatedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_InstructorAccount_CustomerAccountId]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_InstructorAccount_CustomerAccountId] ON [dbo].[InstructorAccount]
(
	[CustomerAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_InstructorAccount_InstructorId]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_InstructorAccount_InstructorId] ON [dbo].[InstructorAccount]
(
	[InstructorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SessionSynopsis_CreatedById]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_SessionSynopsis_CreatedById] ON [dbo].[SessionSynopsis]
(
	[CreatedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SessionSynopsis_UpdatedById]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_SessionSynopsis_UpdatedById] ON [dbo].[SessionSynopsis]
(
	[UpdatedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [SessionSynopsis_SessionSynopsisName_UniqueConstraint]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [SessionSynopsis_SessionSynopsisName_UniqueConstraint] ON [dbo].[SessionSynopsis]
(
	[SessionSynopsisName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheet_ApprovedById]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheet_ApprovedById] ON [dbo].[TimeSheet]
(
	[ApprovedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheet_CreatedById]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheet_CreatedById] ON [dbo].[TimeSheet]
(
	[CreatedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheet_InstructorId]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheet_InstructorId] ON [dbo].[TimeSheet]
(
	[InstructorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheet_UpdatedById]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheet_UpdatedById] ON [dbo].[TimeSheet]
(
	[UpdatedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheetDetail_AccountDetailId]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheetDetail_AccountDetailId] ON [dbo].[TimeSheetDetail]
(
	[AccountDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheetDetail_TimeSheetId]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheetDetail_TimeSheetId] ON [dbo].[TimeSheetDetail]
(
	[TimeSheetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheetDetailSignature_TimeSheetIDetailId]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_TimeSheetDetailSignature_TimeSheetIDetailId] ON [dbo].[TimeSheetDetailSignature]
(
	[TimeSheetIDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserInfo_LoginUserName_UniqueConstraint]    Script Date: 1/2/2019 12:58:29 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserInfo_LoginUserName_UniqueConstraint] ON [dbo].[UserInfo]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerAccount] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[CustomerAccount] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[InstructorAccount] ADD  DEFAULT ((1)) FOR [IsCurrent]
GO
ALTER TABLE [dbo].[TimeSheet] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TimeSheet] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[TimeSheetDetail] ADD  DEFAULT ((0)) FOR [IsReplacementInstructor]
GO
ALTER TABLE [dbo].[AccountDetail]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetail_CustomerAccount_CustomerAccountId] FOREIGN KEY([CustomerAccountId])
REFERENCES [dbo].[CustomerAccount] ([CustomerAccountId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccountDetail] CHECK CONSTRAINT [FK_AccountDetail_CustomerAccount_CustomerAccountId]
GO
ALTER TABLE [dbo].[AccountRate]  WITH CHECK ADD  CONSTRAINT [FK_AccountRate_CustomerAccount_CustomerAccountId] FOREIGN KEY([CustomerAccountId])
REFERENCES [dbo].[CustomerAccount] ([CustomerAccountId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccountRate] CHECK CONSTRAINT [FK_AccountRate_CustomerAccount_CustomerAccountId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CustomerAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAccount_UserInfo_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[CustomerAccount] CHECK CONSTRAINT [FK_CustomerAccount_UserInfo_CreatedById]
GO
ALTER TABLE [dbo].[CustomerAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAccount_UserInfo_UpdatedById] FOREIGN KEY([UpdatedById])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[CustomerAccount] CHECK CONSTRAINT [FK_CustomerAccount_UserInfo_UpdatedById]
GO
ALTER TABLE [dbo].[InstructorAccount]  WITH CHECK ADD  CONSTRAINT [FK_InstructorAccount_CustomerAccount_CustomerAccountId] FOREIGN KEY([CustomerAccountId])
REFERENCES [dbo].[CustomerAccount] ([CustomerAccountId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InstructorAccount] CHECK CONSTRAINT [FK_InstructorAccount_CustomerAccount_CustomerAccountId]
GO
ALTER TABLE [dbo].[InstructorAccount]  WITH CHECK ADD  CONSTRAINT [FK_InstructorAccount_UserInfo_InstructorId] FOREIGN KEY([InstructorId])
REFERENCES [dbo].[UserInfo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InstructorAccount] CHECK CONSTRAINT [FK_InstructorAccount_UserInfo_InstructorId]
GO
ALTER TABLE [dbo].[SessionSynopsis]  WITH CHECK ADD  CONSTRAINT [FK_SessionSynopsis_UserInfo_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[SessionSynopsis] CHECK CONSTRAINT [FK_SessionSynopsis_UserInfo_CreatedById]
GO
ALTER TABLE [dbo].[SessionSynopsis]  WITH CHECK ADD  CONSTRAINT [FK_SessionSynopsis_UserInfo_UpdatedById] FOREIGN KEY([UpdatedById])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[SessionSynopsis] CHECK CONSTRAINT [FK_SessionSynopsis_UserInfo_UpdatedById]
GO
ALTER TABLE [dbo].[TimeSheet]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheet_UserInfo_ApprovedById] FOREIGN KEY([ApprovedById])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[TimeSheet] CHECK CONSTRAINT [FK_TimeSheet_UserInfo_ApprovedById]
GO
ALTER TABLE [dbo].[TimeSheet]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheet_UserInfo_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[TimeSheet] CHECK CONSTRAINT [FK_TimeSheet_UserInfo_CreatedById]
GO
ALTER TABLE [dbo].[TimeSheet]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheet_UserInfo_InstructorId] FOREIGN KEY([InstructorId])
REFERENCES [dbo].[UserInfo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeSheet] CHECK CONSTRAINT [FK_TimeSheet_UserInfo_InstructorId]
GO
ALTER TABLE [dbo].[TimeSheet]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheet_UserInfo_UpdatedById] FOREIGN KEY([UpdatedById])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[TimeSheet] CHECK CONSTRAINT [FK_TimeSheet_UserInfo_UpdatedById]
GO
ALTER TABLE [dbo].[TimeSheetDetail]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheetDetail_AccountDetail_AccountDetailId] FOREIGN KEY([AccountDetailId])
REFERENCES [dbo].[AccountDetail] ([AccountDetailId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeSheetDetail] CHECK CONSTRAINT [FK_TimeSheetDetail_AccountDetail_AccountDetailId]
GO
ALTER TABLE [dbo].[TimeSheetDetail]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheetDetail_TimeSheet_TimeSheetId] FOREIGN KEY([TimeSheetId])
REFERENCES [dbo].[TimeSheet] ([TimeSheetId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeSheetDetail] CHECK CONSTRAINT [FK_TimeSheetDetail_TimeSheet_TimeSheetId]
GO
ALTER TABLE [dbo].[TimeSheetDetailSignature]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheetDetailSignature_TimeSheetDetail_TimeSheetIDetailId] FOREIGN KEY([TimeSheetIDetailId])
REFERENCES [dbo].[TimeSheetDetail] ([TimeSheetDetailId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeSheetDetailSignature] CHECK CONSTRAINT [FK_TimeSheetDetailSignature_TimeSheetDetail_TimeSheetIDetailId]
GO
USE [master]
GO
ALTER DATABASE [TimeSheetManagementSystemDB_Vue] SET  READ_WRITE 
GO
