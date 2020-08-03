CREATE TABLE [dbo].[ConversationFamily] (
    [ID]            INT          IDENTITY (1, 1) NOT NULL,
    [Topic]         VARCHAR (50) NULL,
    [FamilyID]      INT          NULL,
    [UserProfileID] INT          NULL,
    [CreatedOn]     DATETIME     NULL,
    [CreatedBy]     VARCHAR (50) NULL,
    CONSTRAINT [PK_Conversation] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ConversationFamily_Family] FOREIGN KEY ([FamilyID]) REFERENCES [dbo].[Family] ([ID]) NOT FOR REPLICATION
);


GO
ALTER TABLE [dbo].[ConversationFamily] NOCHECK CONSTRAINT [FK_ConversationFamily_Family];

