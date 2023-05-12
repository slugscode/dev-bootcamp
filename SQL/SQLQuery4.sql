CREATE TABLE [dbo].[TblMenu](
    [Id] [int] primary key IDENTITY(1,1) NOT NULL,
    [MenuName] [varchar](80) NOT NULL,
    [MenuAction] [varchar](80) NOT NULL,
    [MenuController] [varchar](80) NOT NULL,
    [MenuIcon] [varchar](80) NULL,
    [MenuSorting] [int] NULL,
    IsParent bit,
    [MenuParent] [int] NULL,
    [IsDelete] [bit],
    [CreatedBy] int not NULL,
    [CreatedDate] datetime not NULL,
    [UpdatedBy] int NULL,
    [UpdatedDate] datetime NULL)

CREATE TABLE [dbo].[TblMenuAccess](
    [Id] [int] primary key IDENTITY(1,1) NOT NULL,
    [IdRole] [int] NULL,
    [IdMenu] [int] NULL,
    [IsDelete] [bit] NULL,
    [CreatedBy] int not NULL,
    [CreatedDate] datetime not NULL,
    [UpdatedBy] int NULL,
    [UpdatedDate] datetime NULL)

	select * from TblMenu
	select * from TblMenuAccess
