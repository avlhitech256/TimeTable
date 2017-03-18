USE [TimeTable]
GO

ALTER TABLE [dbo].[ChairToSpecialization]  WITH CHECK ADD  CONSTRAINT [FK_ChairToSpecialization_Specialization] FOREIGN KEY([SpecializationId])
REFERENCES [dbo].[Specialization] ([Id])
GO

ALTER TABLE [dbo].[ChairToSpecialization] CHECK CONSTRAINT [FK_ChairToSpecialization_Specialization]
GO

