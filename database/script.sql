USE [master]
GO
/****** Object:  Database [Bitzen]    Script Date: 20/11/2023 15:14:43 ******/
CREATE DATABASE [Bitzen]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Bitzen', FILENAME = N'C:\Users\Tellus\Bitzen.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Bitzen_log', FILENAME = N'C:\Users\Tellus\Bitzen_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Bitzen] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Bitzen].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Bitzen] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Bitzen] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Bitzen] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Bitzen] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Bitzen] SET ARITHABORT OFF 
GO
ALTER DATABASE [Bitzen] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Bitzen] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Bitzen] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Bitzen] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Bitzen] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Bitzen] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Bitzen] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Bitzen] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Bitzen] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Bitzen] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Bitzen] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Bitzen] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Bitzen] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Bitzen] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Bitzen] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Bitzen] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Bitzen] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Bitzen] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Bitzen] SET  MULTI_USER 
GO
ALTER DATABASE [Bitzen] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Bitzen] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Bitzen] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Bitzen] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Bitzen] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Bitzen] SET QUERY_STORE = OFF
GO
USE [Bitzen]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Bitzen]
GO
/****** Object:  Table [dbo].[Abastecimentos]    Script Date: 20/11/2023 15:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Abastecimentos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VeiculoId] [int] NOT NULL,
	[MotoristaResponsavelId] [int] NOT NULL,
	[CombustivelId] [int] NOT NULL,
	[Data] [char](10) NOT NULL,
	[QuantidadeAbastecida] [int] NOT NULL,
	[TotalAbastecimento] [decimal](18, 2) NOT NULL,
	[CombustivelPreco] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Abastecimentos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Combustiveis]    Script Date: 20/11/2023 15:14:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Combustiveis](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](20) NOT NULL,
	[Preco] [decimal](18, 2) NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
 CONSTRAINT [PK_Combustiveis] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motoristas]    Script Date: 20/11/2023 15:14:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motoristas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[CPF] [char](14) NOT NULL,
	[NumeroCNH] [char](11) NOT NULL,
	[CategoriaCNH] [char](1) NOT NULL,
	[DataNascimento] [char](10) NOT NULL,
	[Status] [bit] NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
	[DataExclusao] [datetime] NULL,
 CONSTRAINT [PK_Motoristas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 20/11/2023 15:14:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Token] [varchar](50) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[DataExpiracao] [datetime] NOT NULL,
 CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 20/11/2023 15:14:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Senha] [varchar](60) NOT NULL,
	[Status] [bit] NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Veiculos]    Script Date: 20/11/2023 15:14:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Veiculos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Placa] [char](7) NOT NULL,
	[NomeVeiculo] [varchar](50) NOT NULL,
	[CombustivelId] [int] NOT NULL,
	[Fabricante] [varchar](255) NOT NULL,
	[AnoFabricacao] [int] NOT NULL,
	[CapacidadeMaximaTanque] [int] NOT NULL,
	[Observacoes] [text] NULL,
	[DataCadastro] [datetime] NOT NULL,
	[DataExclusao] [datetime] NULL,
 CONSTRAINT [PK__Veiculos__3214EC07ACEF8D52] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Abastecimentos]  WITH CHECK ADD  CONSTRAINT [FK_Abastecimentos_Combustiveis] FOREIGN KEY([CombustivelId])
REFERENCES [dbo].[Combustiveis] ([Id])
GO
ALTER TABLE [dbo].[Abastecimentos] CHECK CONSTRAINT [FK_Abastecimentos_Combustiveis]
GO
ALTER TABLE [dbo].[Abastecimentos]  WITH CHECK ADD  CONSTRAINT [FK_Abastecimentos_Motoristas] FOREIGN KEY([MotoristaResponsavelId])
REFERENCES [dbo].[Motoristas] ([Id])
GO
ALTER TABLE [dbo].[Abastecimentos] CHECK CONSTRAINT [FK_Abastecimentos_Motoristas]
GO
ALTER TABLE [dbo].[Abastecimentos]  WITH CHECK ADD  CONSTRAINT [FK_Abastecimentos_Veiculos] FOREIGN KEY([VeiculoId])
REFERENCES [dbo].[Veiculos] ([Id])
GO
ALTER TABLE [dbo].[Abastecimentos] CHECK CONSTRAINT [FK_Abastecimentos_Veiculos]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_Usuarios]
GO
ALTER TABLE [dbo].[Veiculos]  WITH CHECK ADD  CONSTRAINT [FK_Veiculos_Combustiveis] FOREIGN KEY([CombustivelId])
REFERENCES [dbo].[Combustiveis] ([Id])
GO
ALTER TABLE [dbo].[Veiculos] CHECK CONSTRAINT [FK_Veiculos_Combustiveis]
GO
USE [master]
GO
ALTER DATABASE [Bitzen] SET  READ_WRITE 
GO
