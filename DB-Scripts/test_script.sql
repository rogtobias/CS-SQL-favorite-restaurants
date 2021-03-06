USE [restaurants_test]
GO
/****** Object:  Table [dbo].[cuisine_list]    Script Date: 6/7/2017 10:38:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuisine_list](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[restaurant_list]    Script Date: 6/7/2017 10:38:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[restaurant_list](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[alchohol] [bit] NULL,
	[cuisine_id] [int] NULL
) ON [PRIMARY]

GO
