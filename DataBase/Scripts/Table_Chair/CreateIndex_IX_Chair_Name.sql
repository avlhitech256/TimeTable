USE [TimeTable]
GO

/****** Object:  Index [IX_Chair_Name]    Script Date: 14.03.2017 21:31:51 ******/
CREATE NONCLUSTERED INDEX [IX_Chair_Name] ON [dbo].[Chair]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

