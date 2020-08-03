CREATE TABLE [dbo].[Media] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [Title]            VARCHAR (50)    NULL,
    [Description]      VARCHAR (MAX)   NULL,
    [Image]            VARBINARY (MAX) NULL,
    [ImageContentType] VARCHAR (50)    NULL,
    [Video]            VARBINARY (MAX) NULL,
    [VideoContentType] VARCHAR (50)    NULL,
    [UserProfileID]    INT             NULL,
    [CreatedOn]        DATETIME        NULL,
    [CreatedBy]        VARCHAR (50)    NULL,
    CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Media_UserProfile] FOREIGN KEY ([UserProfileID]) REFERENCES [dbo].[UserProfile] ([ID]) NOT FOR REPLICATION
);


GO
ALTER TABLE [dbo].[Media] NOCHECK CONSTRAINT [FK_Media_UserProfile];

