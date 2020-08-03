CREATE TABLE [dbo].[Chat] (
    [ID]                INT            IDENTITY (1, 1) NOT NULL,
    [Subject]           VARCHAR (50)   NULL,
    [OriginatingUserID] NVARCHAR (450) NULL,
    [ReceivingUserID]   NVARCHAR (450) NULL,
    [CreatedOn]         DATETIME       NULL,
    [CreatedBy]         VARCHAR (50)   NULL,
    CONSTRAINT [PK_Chat] PRIMARY KEY CLUSTERED ([ID] ASC)
);

