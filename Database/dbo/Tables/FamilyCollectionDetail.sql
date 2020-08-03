CREATE TABLE [dbo].[FamilyCollectionDetail] (
    [ID]                 INT            NOT NULL,
    [Item]               VARCHAR (50)   NULL,
    [Advanced]           BIT            NULL,
    [Description]        VARCHAR (MAX)  NULL,
    [Quantity]           INT            NULL,
    [Amount]             FLOAT (53)     NULL,
    [FamilyCollectionID] INT            NULL,
    [UserID]             NVARCHAR (450) NULL,
    [CreatedOn]          DATETIME       NULL,
    [CreatedBy]          VARCHAR (50)   NULL,
    CONSTRAINT [PK_FamilyCollectionDetail] PRIMARY KEY CLUSTERED ([ID] ASC)
);

