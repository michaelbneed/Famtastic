CREATE TABLE [dbo].[Note] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [Title]         VARCHAR (50)  NOT NULL,
    [Text]          VARCHAR (MAX) NOT NULL,
    [Advanced]      BIT           NOT NULL,
    [DueDate]       DATETIME      NULL,
    [UserProfileID] INT           NULL,
    [ShareToFam]    BIT           NOT NULL,
    [CreatedOn]     DATETIME      NULL,
    [CreatedBy]     VARCHAR (50)  NULL,
    CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Note_UserProfile] FOREIGN KEY ([UserProfileID]) REFERENCES [dbo].[UserProfile] ([ID]) NOT FOR REPLICATION
);


GO
ALTER TABLE [dbo].[Note] NOCHECK CONSTRAINT [FK_Note_UserProfile];

