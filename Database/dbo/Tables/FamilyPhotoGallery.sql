CREATE TABLE [dbo].[FamilyPhotoGallery] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (50)  NULL,
    [Description] VARCHAR (MAX) NULL,
    [FamilyID]    INT           NULL,
    [CreatedOn]   DATETIME      NULL,
    [CreatedBy]   VARCHAR (50)  NULL,
    CONSTRAINT [PK_FamilyPhotoGallery] PRIMARY KEY CLUSTERED ([ID] ASC)
);

