CREATE TABLE [Computers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ComputerTypeId] [int] NOT NULL,
	[ProcessorId] [int] NOT NULL,
	[MemorycardId] [int] NOT NULL,
	[VideocardId] [int] NOT NULL,
	[Price] [money] NOT NULL
)
GO

CREATE TABLE [ComputerTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](20) NULL
)
GO

CREATE TABLE [Manufacturers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NULL
)
GO

CREATE TABLE [Memorycards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ManufacturerId] [int] NOT NULL,
	[MhZ] [float] NOT NULL,
	[Capacity] [nvarchar](15) NULL,
	[Price] [money] NOT NULL
)
GO

CREATE TABLE [Processors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ManufacturerId] [int] NOT NULL,
	[Model] [nvarchar](50) NULL,
	[MhZ] [float] NOT NULL,
	[Price] [money] NOT NULL
)
GO

CREATE TABLE [Videocards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ManufacturerId] [int] NOT NULL,
	[Model] [nvarchar](50) NULL,
	[Memory] [nvarchar](15) NULL,
	[Price] [money] NOT NULL
)
GO