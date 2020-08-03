CREATE TABLE [dbo].[Family] (
    [ID]                       INT             IDENTITY (1, 1) NOT NULL,
    [FamilyLastName]           VARCHAR (50)    NULL,
    [Title]                    VARCHAR (100)   NULL,
    [Picture]                  VARBINARY (MAX) NULL,
    [PictureContentType]       VARCHAR (50)    NULL,
    [Description]              VARCHAR (MAX)   NULL,
    [InvitationCode]           VARCHAR (50)    NULL,
    [FamilyAdminUserProfileId] INT             NULL,
    [CreatedOn]                DATETIME        NULL,
    [CreatedBy]                VARCHAR (50)    NULL,
    CONSTRAINT [PK_Family] PRIMARY KEY CLUSTERED ([ID] ASC)
);

