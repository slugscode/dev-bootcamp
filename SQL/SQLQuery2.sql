CREATE TABLE [dbo].[TblOrderDetail](
    [Id] [int] Primary Key IDENTITY(1,1) NOT NULL,
    [IdHeader] [int] NOT NULL,
    [IdProduct] [int] NOT NULL,
    [Qty] [int] NOT NULL,
    [SubTotal] [decimal](18, 2) NOT NULL,
    [IsDelete] [bit] NULL,
    [CreateBy] [int] NOT NULL,
    [CreateDate] [date] NOT NULL,
    [UpdateBy] [int] NULL,
    [UpdateDate] [date] NULL)

CREATE TABLE [dbo].[TblOrderHeader](
    [Id] [int] Primary Key IDENTITY(1,1) NOT NULL,
    [CodeTransaction] [nvarchar](20) NOT NULL,
    [IdCustomer] [int] NOT NULL,
    [Amount] [decimal](18, 2) NOT NULL,
    [TotalQty] [int] NOT NULL,
    [IsCheckout] [bit] NOT NULL,
    [IsDelete] [bit] NULL,
    [CreateBy] [int] NOT NULL,
    [CreateDate] [date] NOT NULL,
    [UpdateBy] [int] NULL,
    [UpdateDate] [date] NULL)