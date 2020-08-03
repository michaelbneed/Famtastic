CREATE TABLE [dbo].[ChatMessage] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [MessageText]  VARCHAR (MAX)  NULL,
    [ChatThreadID] INT            NULL,
    [UserID]       NVARCHAR (450) NULL,
    [CreatedOn]    DATETIME       NULL,
    [CreatedBy]    VARCHAR (50)   NULL,
    CONSTRAINT [PK_ChatMessage] PRIMARY KEY CLUSTERED ([ID] ASC)
);

