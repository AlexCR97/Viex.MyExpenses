USE [master]
GO
/****** Object:  Database [MyExpenses]    Script Date: 7/25/2021 9:20:41 PM ******/
CREATE DATABASE [MyExpenses]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyExpenses', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MyExpenses.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MyExpenses_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MyExpenses_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MyExpenses] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyExpenses].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyExpenses] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyExpenses] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyExpenses] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyExpenses] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyExpenses] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyExpenses] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MyExpenses] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyExpenses] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyExpenses] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyExpenses] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyExpenses] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyExpenses] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyExpenses] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyExpenses] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyExpenses] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MyExpenses] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyExpenses] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyExpenses] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyExpenses] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyExpenses] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyExpenses] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [MyExpenses] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyExpenses] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MyExpenses] SET  MULTI_USER 
GO
ALTER DATABASE [MyExpenses] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyExpenses] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyExpenses] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyExpenses] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyExpenses] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyExpenses] SET QUERY_STORE = OFF
GO
USE [MyExpenses]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/25/2021 9:20:42 PM ******/
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
/****** Object:  Table [dbo].[CategoryDescriptors]    Script Date: 7/25/2021 9:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryDescriptors](
	[CategoryDescriptorId] [bigint] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateUpdated] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_CategoryDescriptors] PRIMARY KEY CLUSTERED 
(
	[CategoryDescriptorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionEntries]    Script Date: 7/25/2021 9:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionEntries](
	[TransactionEntryId] [bigint] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CategoryId] [bigint] NULL,
	[TypeId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateUpdated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TransactionEntries] PRIMARY KEY CLUSTERED 
(
	[TransactionEntryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionTypeDescriptors]    Script Date: 7/25/2021 9:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionTypeDescriptors](
	[TransactionTypeDescriptorId] [bigint] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateUpdated] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_TransactionTypeDescriptors] PRIMARY KEY CLUSTERED 
(
	[TransactionTypeDescriptorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/25/2021 9:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateUpdated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_TransactionEntries_CategoryId]    Script Date: 7/25/2021 9:20:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_TransactionEntries_CategoryId] ON [dbo].[TransactionEntries]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TransactionEntries_TypeId]    Script Date: 7/25/2021 9:20:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_TransactionEntries_TypeId] ON [dbo].[TransactionEntries]
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TransactionEntries_UserId]    Script Date: 7/25/2021 9:20:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_TransactionEntries_UserId] ON [dbo].[TransactionEntries]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TransactionEntries]  WITH CHECK ADD  CONSTRAINT [FK_TransactionEntries_CategoryDescriptors_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[CategoryDescriptors] ([CategoryDescriptorId])
GO
ALTER TABLE [dbo].[TransactionEntries] CHECK CONSTRAINT [FK_TransactionEntries_CategoryDescriptors_CategoryId]
GO
ALTER TABLE [dbo].[TransactionEntries]  WITH CHECK ADD  CONSTRAINT [FK_TransactionEntries_TransactionTypeDescriptors_TypeId] FOREIGN KEY([TypeId])
REFERENCES [dbo].[TransactionTypeDescriptors] ([TransactionTypeDescriptorId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TransactionEntries] CHECK CONSTRAINT [FK_TransactionEntries_TransactionTypeDescriptors_TypeId]
GO
ALTER TABLE [dbo].[TransactionEntries]  WITH CHECK ADD  CONSTRAINT [FK_TransactionEntries_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TransactionEntries] CHECK CONSTRAINT [FK_TransactionEntries_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [MyExpenses] SET  READ_WRITE 
GO
