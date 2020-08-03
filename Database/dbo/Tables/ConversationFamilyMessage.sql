CREATE TABLE [dbo].[ConversationFamilyMessage] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [MessageText]    VARCHAR (MAX) NOT NULL,
    [ConversationID] INT           NULL,
    [UserProfileID]  INT           NULL,
    [CreatedOn]      DATETIME      NULL,
    [CreatedBy]      VARCHAR (50)  NULL,
    CONSTRAINT [PK_ConversationFamilyMessage] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ConversationFamilyMessage_ConversationFamily] FOREIGN KEY ([ConversationID]) REFERENCES [dbo].[ConversationFamily] ([ID]) NOT FOR REPLICATION
);


GO
ALTER TABLE [dbo].[ConversationFamilyMessage] NOCHECK CONSTRAINT [FK_ConversationFamilyMessage_ConversationFamily];

