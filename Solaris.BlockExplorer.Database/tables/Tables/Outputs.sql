CREATE TABLE [tables].[Outputs] (
    [Id]    UNIQUEIDENTIFIER CONSTRAINT [DF_Outputs_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Value] DECIMAL (28, 8)  NOT NULL,
    CONSTRAINT [PK_Outputs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

