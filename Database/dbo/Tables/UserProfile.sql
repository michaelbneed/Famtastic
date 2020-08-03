CREATE TABLE [dbo].[UserProfile] (
    [ID]                 INT             IDENTITY (1, 1) NOT NULL,
    [UserID]             NVARCHAR (450)  NOT NULL,
    [FamilyID]           INT             NULL,
    [Email]              NVARCHAR (256)  NULL,
    [Profile]            NVARCHAR (256)  NULL,
    [FirstName]          NVARCHAR (256)  NULL,
    [LastName]           NVARCHAR (256)  NULL,
    [Picture]            VARBINARY (MAX) NULL,
    [PictureContentType] VARCHAR (50)    NULL,
    [Blurb]              NVARCHAR (MAX)  NULL,
    [CreatedOn]          DATETIME        NULL,
    [CreatedBy]          NVARCHAR (256)  NULL,
    [UpdatedOn]          DATETIME        NULL,
    [UpdatedBy]          NVARCHAR (256)  NULL,
    CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserProfile_Family] FOREIGN KEY ([FamilyID]) REFERENCES [dbo].[Family] ([ID]) NOT FOR REPLICATION
);


GO
ALTER TABLE [dbo].[UserProfile] NOCHECK CONSTRAINT [FK_UserProfile_Family];

