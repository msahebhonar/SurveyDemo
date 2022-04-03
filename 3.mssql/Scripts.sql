IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [QuestionBank] (
    [QuestionBankId] int NOT NULL IDENTITY,
    [Text] nvarchar(1000) NOT NULL,
    [QuestionType] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_QuestionBank] PRIMARY KEY ([QuestionBankId])
);
GO

CREATE TABLE [SurveyDetails] (
    [SurveyDetailId] uniqueidentifier NOT NULL,
    [Title] nvarchar(100) NOT NULL,
    [Description] nvarchar(1000) NULL,
    CONSTRAINT [PK_SurveyDetails] PRIMARY KEY ([SurveyDetailId])
);
GO

CREATE TABLE [UserAccounts] (
    [UserAccountId] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(50) NULL,
    [LastName] nvarchar(100) NULL,
    [Email] nvarchar(320) NOT NULL,
    [Password] nvarchar(1000) NOT NULL,
    CONSTRAINT [PK_UserAccounts] PRIMARY KEY ([UserAccountId])
);
GO

CREATE TABLE [Responses] (
    [ResponseId] int NOT NULL IDENTITY,
    [Text] nvarchar(500) NULL,
    [Order] int NOT NULL,
    [QuestionBankId] int NULL,
    CONSTRAINT [PK_Responses] PRIMARY KEY ([ResponseId]),
    CONSTRAINT [FK_Responses_QuestionBank_QuestionBankId] FOREIGN KEY ([QuestionBankId]) REFERENCES [QuestionBank] ([QuestionBankId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SurveyDetailQuestions] (
    [SurveyDetailId] uniqueidentifier NOT NULL,
    [QuestionBankId] int NOT NULL,
    CONSTRAINT [PK_SurveyDetailQuestions] PRIMARY KEY ([SurveyDetailId], [QuestionBankId]),
    CONSTRAINT [FK_SurveyDetailQuestions_QuestionBank_QuestionBankId] FOREIGN KEY ([QuestionBankId]) REFERENCES [QuestionBank] ([QuestionBankId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SurveyDetailQuestions_SurveyDetails_SurveyDetailId] FOREIGN KEY ([SurveyDetailId]) REFERENCES [SurveyDetails] ([SurveyDetailId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Respondents] (
    [RespondentId] int NOT NULL IDENTITY,
    [SurveyDetailId] uniqueidentifier NOT NULL,
    [UserAccountId] uniqueidentifier NOT NULL,
    [SendTime] datetime2 NOT NULL,
    [SubmitTime] datetime2 NULL,
    CONSTRAINT [PK_Respondents] PRIMARY KEY ([RespondentId]),
    CONSTRAINT [FK_Respondents_SurveyDetails_SurveyDetailId] FOREIGN KEY ([SurveyDetailId]) REFERENCES [SurveyDetails] ([SurveyDetailId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Respondents_UserAccounts_UserAccountId] FOREIGN KEY ([UserAccountId]) REFERENCES [UserAccounts] ([UserAccountId]) ON DELETE CASCADE
);
GO

CREATE TABLE [RespondentAnswers] (
    [RespondentAnswerId] int NOT NULL IDENTITY,
    [RespondentId] int NOT NULL,
    [QuestionBankId] int NOT NULL,
    [Answer] nvarchar(200) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NULL,
    CONSTRAINT [PK_RespondentAnswers] PRIMARY KEY ([RespondentAnswerId]),
    CONSTRAINT [FK_RespondentAnswers_QuestionBank_QuestionBankId] FOREIGN KEY ([QuestionBankId]) REFERENCES [QuestionBank] ([QuestionBankId]) ON DELETE CASCADE,
    CONSTRAINT [FK_RespondentAnswers_Respondents_RespondentId] FOREIGN KEY ([RespondentId]) REFERENCES [Respondents] ([RespondentId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_RespondentAnswers_QuestionBankId] ON [RespondentAnswers] ([QuestionBankId]);
GO

CREATE INDEX [IX_RespondentAnswers_RespondentId] ON [RespondentAnswers] ([RespondentId]);
GO

CREATE INDEX [IX_Respondents_SurveyDetailId] ON [Respondents] ([SurveyDetailId]);
GO

CREATE UNIQUE INDEX [IX_Respondents_UserAccountId_SurveyDetailId] ON [Respondents] ([UserAccountId], [SurveyDetailId]);
GO

CREATE INDEX [IX_Responses_QuestionBankId] ON [Responses] ([QuestionBankId]);
GO

CREATE INDEX [IX_SurveyDetailQuestions_QuestionBankId] ON [SurveyDetailQuestions] ([QuestionBankId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220403151904_InitModel', N'5.0.15');
GO

COMMIT;
GO

