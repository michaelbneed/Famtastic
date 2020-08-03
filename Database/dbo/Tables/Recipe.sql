CREATE TABLE [dbo].[Recipe] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [Name]            VARCHAR (50)  NULL,
    [Description]     VARCHAR (50)  NULL,
    [Ingredients]     VARCHAR (MAX) NULL,
    [Instructions]    VARCHAR (MAX) NULL,
    [Comments]        VARCHAR (MAX) NULL,
    [ShareToFam]      BIT           NULL,
    [CommunityShared] BIT           NULL,
    [FamilyID]        INT           NULL,
    [UserProfileID]   INT           NULL,
    [CreatedOn]       DATETIME      NULL,
    [CreatedBy]       VARCHAR (50)  NULL,
    CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Recipe_UserProfile] FOREIGN KEY ([UserProfileID]) REFERENCES [dbo].[UserProfile] ([ID]) NOT FOR REPLICATION
);


GO
ALTER TABLE [dbo].[Recipe] NOCHECK CONSTRAINT [FK_Recipe_UserProfile];

