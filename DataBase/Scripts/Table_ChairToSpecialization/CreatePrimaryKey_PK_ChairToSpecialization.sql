USE [TimeTable]
GO

/****** Object:  Index [PK_ChairToSpecialization]    Script Date: 14.03.2017 21:43:03 ******/
ALTER TABLE [dbo].[ChairToSpecialization] ADD  CONSTRAINT [PK_ChairToSpecialization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

