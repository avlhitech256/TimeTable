USE [TimeTable]
GO

ALTER TABLE [dbo].[Chair]  WITH CHECK ADD  CONSTRAINT [FK_Chair_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([Id])
GO

ALTER TABLE [dbo].[Chair] CHECK CONSTRAINT [FK_Chair_Faculty]
GO

