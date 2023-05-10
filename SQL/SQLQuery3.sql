CREATE TABLE [dbo].[TblUser](
    [Id] [int] Primary Key IDENTITY(1,1) NOT NULL,
    [Email] [varchar](50) NOT NULL,
    [Password] [varchar](100) NOT NULL,
    [IdRole] [int] NOT NULL,
    [IsDelete] [bit] NOT NULL,
    [CreateBy] [int] NOT NULL,
    [CreateDate] [datetime2](7) NULL,
    [UpdateBy] [int] NULL,
    [UpdateDate] [datetime2](7) NULL
)

CREATE TABLE [dbo].[TblCustomer](
    [Id] [int] Primary Key IDENTITY(1,1) NOT NULL,
    [IdUser] [int] NOT NULL,
    [NameCustomer] [varchar](50) NOT NULL,
    [Address] [varchar](max) NOT NULL,
    [Phone] [varchar](15) NOT NULL,
    [IsDelete] [bit] NOT NULL,
    [CreateBy] [int] NOT NULL,
    [CreateDate] [datetime2](7) NULL,
    [UpdateBy] [int] NULL,
    [UpdateDate] [datetime2](7) NULL)

CREATE TABLE [dbo].[TblRole](
    [Id] [int] Primary Key 
IDENTITY(1,1) NOT NULL,
    [RoleName] [varchar](80) NULL,
    [IsDelete] [bit] NULL,
    [CreatedBy] [int] NOT NULL,
    [CreatedDate] [datetime] NOT NULL,
    [UpdatedBy] [int] NULL,
    [UpdatedDate] [datetime] NULL)

	select * from TblRole