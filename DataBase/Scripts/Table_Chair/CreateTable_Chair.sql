USE [TimeTable]
GO

/****** Object:  Table [dbo].[Chair]    Script Date: 14.03.2017 21:20:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Chair](
	[Id] [bigint] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[FacultyId] [bigint] NOT NULL,
	[Active] [bit] NOT NULL,
	[Cteated] [datetimeoffset](7) NOT NULL,
	[LastModify] [datetimeoffset](7) NOT NULL,
	[UserModify] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Chair] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Chair]  WITH CHECK ADD  CONSTRAINT [FK_Chair_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([Id])
GO

ALTER TABLE [dbo].[Chair] CHECK CONSTRAINT [FK_Chair_Faculty]
GO

