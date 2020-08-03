CREATE TABLE [dbo].[NoteListItem] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [NoteID]       INT            NOT NULL,
    [NoteListItem] NVARCHAR (50)  NULL,
    [CreatedOn]    DATETIME       NULL,
    [CreatedBy]    NVARCHAR (256) NULL,
    CONSTRAINT [PK_NoteListItem] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_NoteListItem_Note] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[Note] ([ID]) NOT FOR REPLICATION
);


GO
ALTER TABLE [dbo].[NoteListItem] NOCHECK CONSTRAINT [FK_NoteListItem_Note];

