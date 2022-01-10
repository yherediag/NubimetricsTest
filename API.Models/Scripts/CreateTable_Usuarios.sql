USE [NubimetricsExample]
GO

/****** Object:  Table [dbo].[Usuarios]    Script Date: 1/7/2022 11:03:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](30) NOT NULL,
	[Apellido] [varchar](30) NOT NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](max) NOT NULL,
	[Habilitado] [bit] NOT NULL,
	[FechaAlta] [datetime] NOT NULL,
	[FechaModificado] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((1)) FOR [Habilitado]
GO

ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (getdate()) FOR [FechaAlta]
GO


