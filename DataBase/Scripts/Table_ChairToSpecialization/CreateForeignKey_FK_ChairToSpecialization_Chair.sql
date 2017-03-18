USE [TimeTable]
GO

ALTER TABLE [dbo].[ChairToSpecialization]  WITH CHECK ADD  CONSTRAINT [FK_ChairToSpecialization_Chair] FOREIGN KEY([ChairId])
REFERENCES [dbo].[Chair] ([Id])
GO

ALTER TABLE [dbo].[ChairToSpecialization] CHECK CONSTRAINT [FK_ChairToSpecialization_Chair]
GO

