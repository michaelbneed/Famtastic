CREATE TABLE [dbo].[FamilyCollection] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [Title]           VARCHAR (50)   NOT NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [CommunityShared] BIT            NULL,
    [FamilyID]        INT            NULL,
    [UserID]          NVARCHAR (450) NULL,
    [CreatedOn]       DATETIME       NULL,
    [CreatedBy]       VARCHAR (50)   NULL,
    CONSTRAINT [PK_FamilyCollection] PRIMARY KEY CLUSTERED ([ID] ASC)
);

