USE [TimeTable]
GO

/****** Object:  Table [dbo].[ChairToSpecialization]    Script Date: 14.03.2017 21:40:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ChairToSpecialization](
	[Id] [bigint] NOT NULL,
	[ChairId] [bigint] NOT NULL,
	[SpecializationId] [bigint] NOT NULL,
	[Active] [bit] NOT NULL,
	[Cteated] [datetimeoffset](7) NOT NULL,
	[LastModify] [datetimeoffset](7) NOT NULL,
	[UserModify] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ChairToSpecialization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

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

