USE [master]
GO
/****** Object:  Database [TimeTable]    Script Date: 04.05.2017 23:19:14 ******/
CREATE DATABASE [TimeTable]
 CONTAINMENT = PARTIAL
 ON  PRIMARY 
( NAME = N'TimeTable', FILENAME = N'C:\DataBase\MSSQL12.SQLSERVERIT1\MSSQL\DATA\TimeTable.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TimeTable_log', FILENAME = N'C:\DataBase\MSSQL12.SQLSERVERIT1\MSSQL\DATA\TimeTable_log.ldf' , SIZE = 2560KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TimeTable] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TimeTable].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TimeTable] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TimeTable] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TimeTable] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TimeTable] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TimeTable] SET ARITHABORT OFF 
GO
ALTER DATABASE [TimeTable] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TimeTable] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TimeTable] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TimeTable] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TimeTable] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TimeTable] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TimeTable] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TimeTable] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TimeTable] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TimeTable] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TimeTable] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TimeTable] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TimeTable] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TimeTable] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TimeTable] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TimeTable] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TimeTable] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TimeTable] SET RECOVERY FULL 
GO
ALTER DATABASE [TimeTable] SET  MULTI_USER 
GO
ALTER DATABASE [TimeTable] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TimeTable] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TimeTable] SET DEFAULT_FULLTEXT_LANGUAGE = 1049 
GO
ALTER DATABASE [TimeTable] SET DEFAULT_LANGUAGE = 1049 
GO
ALTER DATABASE [TimeTable] SET NESTED_TRIGGERS = ON 
GO
ALTER DATABASE [TimeTable] SET TRANSFORM_NOISE_WORDS = OFF 
GO
ALTER DATABASE [TimeTable] SET TWO_DIGIT_YEAR_CUTOFF = 2049 
GO
ALTER DATABASE [TimeTable] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TimeTable] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [TimeTable] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TimeTable', N'ON'
GO
USE [TimeTable]
GO
/****** Object:  Table [dbo].[Chair]    Script Date: 04.05.2017 23:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chair](
	[Id] [bigint] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[FacultyId] [bigint] NULL,
	[Active] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[LastModify] [datetimeoffset](7) NOT NULL,
	[UserModify] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Chair] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChairToSpecialization]    Script Date: 04.05.2017 23:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChairToSpecialization](
	[Id] [bigint] NOT NULL,
	[ChairId] [bigint] NOT NULL,
	[SpecializationId] [bigint] NOT NULL,
	[Active] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[LastModify] [datetimeoffset](7) NOT NULL,
	[UserModify] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ChairToSpecialization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 04.05.2017 23:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Position] [nvarchar](255) NOT NULL,
	[Active] [bit] NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](255) NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[LastModify] [datetimeoffset](7) NOT NULL,
	[UserModify] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 04.05.2017 23:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculty](
	[Id] [bigint] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[HighSchoolId] [bigint] NULL,
	[Active] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[LastModify] [datetimeoffset](7) NOT NULL,
	[UserModify] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Faculty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HighSchool]    Script Date: 04.05.2017 23:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HighSchool](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Active] [bit] NOT NULL,
	[Rector] [bigint] NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[LastModify] [datetimeoffset](7) NOT NULL,
	[UserModify] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_HighSchool] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 04.05.2017 23:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[Id] [bigint] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[SpecialtyId] [bigint] NULL,
	[Active] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[LastModify] [datetimeoffset](7) NOT NULL,
	[UserModify] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Specialization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Specialty]    Script Date: 04.05.2017 23:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialty](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Active] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[LastModify] [datetimeoffset](7) NOT NULL,
	[UserModify] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Specialty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Chair_Code]    Script Date: 04.05.2017 23:19:16 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Chair_Code] ON [dbo].[Chair]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Chair_Name]    Script Date: 04.05.2017 23:19:16 ******/
CREATE NONCLUSTERED INDEX [IX_Chair_Name] ON [dbo].[Chair]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Employee_Code]    Script Date: 04.05.2017 23:19:16 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Employee_Code] ON [dbo].[Employee]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Faculty_Code]    Script Date: 04.05.2017 23:19:16 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Faculty_Code] ON [dbo].[Faculty]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Faculty_Name]    Script Date: 04.05.2017 23:19:16 ******/
CREATE NONCLUSTERED INDEX [IX_Faculty_Name] ON [dbo].[Faculty]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_HighSchool_Code]    Script Date: 04.05.2017 23:19:16 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_HighSchool_Code] ON [dbo].[HighSchool]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_HighSchool_Name]    Script Date: 04.05.2017 23:19:16 ******/
CREATE NONCLUSTERED INDEX [IX_HighSchool_Name] ON [dbo].[HighSchool]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Specialty_Code]    Script Date: 04.05.2017 23:19:16 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Specialty_Code] ON [dbo].[Specialty]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Specialty_Name]    Script Date: 04.05.2017 23:19:16 ******/
CREATE NONCLUSTERED INDEX [IX_Specialty_Name] ON [dbo].[Specialty]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Chair]  WITH CHECK ADD  CONSTRAINT [FK_Chair_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([Id])
GO
ALTER TABLE [dbo].[Chair] CHECK CONSTRAINT [FK_Chair_Faculty]
GO
ALTER TABLE [dbo].[ChairToSpecialization]  WITH CHECK ADD  CONSTRAINT [FK_ChairToSpecialization_Chair] FOREIGN KEY([ChairId])
REFERENCES [dbo].[Chair] ([Id])
GO
ALTER TABLE [dbo].[ChairToSpecialization] CHECK CONSTRAINT [FK_ChairToSpecialization_Chair]
GO
ALTER TABLE [dbo].[ChairToSpecialization]  WITH CHECK ADD  CONSTRAINT [FK_ChairToSpecialization_Specialization] FOREIGN KEY([SpecializationId])
REFERENCES [dbo].[Specialization] ([Id])
GO
ALTER TABLE [dbo].[ChairToSpecialization] CHECK CONSTRAINT [FK_ChairToSpecialization_Specialization]
GO
ALTER TABLE [dbo].[Faculty]  WITH CHECK ADD  CONSTRAINT [FK_Faculty_HighSchool] FOREIGN KEY([HighSchoolId])
REFERENCES [dbo].[HighSchool] ([Id])
GO
ALTER TABLE [dbo].[Faculty] CHECK CONSTRAINT [FK_Faculty_HighSchool]
GO
ALTER TABLE [dbo].[HighSchool]  WITH CHECK ADD  CONSTRAINT [FK_HighSchool_Employee] FOREIGN KEY([Rector])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[HighSchool] CHECK CONSTRAINT [FK_HighSchool_Employee]
GO
ALTER TABLE [dbo].[Specialization]  WITH CHECK ADD  CONSTRAINT [FK_Specialization_Specialty] FOREIGN KEY([SpecialtyId])
REFERENCES [dbo].[Specialty] ([Id])
GO
ALTER TABLE [dbo].[Specialization] CHECK CONSTRAINT [FK_Specialization_Specialty]
GO
USE [master]
GO
ALTER DATABASE [TimeTable] SET  READ_WRITE 
GO
