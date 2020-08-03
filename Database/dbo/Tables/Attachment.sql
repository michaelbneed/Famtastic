CREATE TABLE [dbo].[Attachment] (
    [ID]              INT IDENTITY (1, 1) NOT NULL,
    [NoteID]          INT NULL,
    [FamilyMessageID] INT NULL,
    [ChatMessageID]   INT NULL,
    [RecipeID]        INT NULL,
    CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED ([ID] ASC)
);

