Create Database Xpos_315

CREATE TABLE [dbo].[TblCategory](
    [Id] [int] IDENTITY(1,1) Primary Key NOT NULL,
    [NamaCategory] [varchar](100) NOT NULL,
    [Description] [varchar](MAX) NOT NULL,
    [IsDelete] [bit] NULL,
    [CreateBy] [int] NULL,
    [CreateDate] [datetime] NULL,
    [UpdateBy] [int] NULL,
    [UpdateDate] [datetime] NULL)

CREATE TABLE [dbo].[TblVariant](
    [Id] [int] IDENTITY(1,1) Primary Key NOT NULL,
	[IdCategory] [int] NOT NULL,
    [NameVariant] [varchar](100) NOT NULL,
    [Description] [varchar](MAX) NOT NULL,
    [IsDelete] [bit] NULL,
    [CreateBy] [int] NULL,
    [CreateDate] [datetime] NULL,
    [UpdateBy] [int] NULL,
    [UpdateDate] [datetime] NULL)

CREATE TABLE [dbo].[TblProduct](
    [Id] [int] IDENTITY(1,1) Primary Key NOT NULL,
    [NameProduct] [varchar](100) NOT NULL,
    [Price] [decimal](18, 0) NOT NULL,
    [Stock] [int] NOT NULL,
    [IdVariant] [int] NOT NULL,
    [Image] [varchar](max) NULL,
    [IsDelete] [bit] NULL,
    [CreateBy] [int] NOT NULL,
    [CreateDate] [date] NOT NULL,
    [UpdateBy] [int] NULL,
    [UpdateDate] [date] NULL)


