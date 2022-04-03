USE SurveyDb
GO

INSERT INTO dbo.QuestionBank(Text,QuestionType,IsActive) VALUES(N'Overall, how satisfied or dissatisfied are you with our company?', 2, 1)
INSERT INTO dbo.QuestionBank(Text,QuestionType,IsActive) VALUES(N'Which of the following words would you use to describe our products? Select all that apply', 3, 1)
INSERT INTO dbo.QuestionBank(Text,QuestionType,IsActive) VALUES(N'How well do our products meet your needs?', 2, 1)
INSERT INTO dbo.QuestionBank(Text,QuestionType,IsActive) VALUES(N'How would you rate the quality of the product', 2, 1)
INSERT INTO dbo.QuestionBank(Text,QuestionType,IsActive) VALUES(N'Do you have any other comments, questions, or concerns?', 1, 1)
GO

INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Very satisfied', 1, 1)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Somewhat satisfied', 1, 2)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Neither satisfied nor dissatisfied', 1, 3)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Somewhat dissatisfied', 1, 4)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Very dissatisfied', 1, 5)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Reliable', 2, 1)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'High quality', 2, 2)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Useful', 2, 3)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Unique', 2, 4)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Good value for money', 2, 5)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Overpaid', 2, 6)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Impractical', 2, 7)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Ineffective', 2, 8)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Poor quality', 2, 9)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Unreliable', 2, 10)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Extremely well', 3, 1)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Very well', 3, 2)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Somewhat well', 3, 3)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Not so well', 3, 4)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Not at all well', 3, 5)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Very high quality', 4, 1)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'High quality', 4, 2)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Neither high nor low quality', 4, 3)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Low quality', 4, 4)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Very low quality', 4, 5)
INSERT INTO dbo.Responses(Text,QuestionBankId, [Order]) VALUES(N'Very low quality', 4, 6)

GO 

INSERT INTO dbo.SurveyDetails(SurveyDetailId, Title,Description) VALUES('2C343747-EF7D-486F-9BE7-0869FDC726DB', N'Customer Satisfaction', N'Customer satisfaction is defined as a measurement that determines how happy customers are with a company''s products, services, and capabilities')
GO

INSERT INTO dbo.SurveyDetailQuestions(SurveyDetailId,QuestionBankId) VALUES('2C343747-EF7D-486F-9BE7-0869FDC726DB', 1)
INSERT INTO dbo.SurveyDetailQuestions(SurveyDetailId,QuestionBankId) VALUES('2C343747-EF7D-486F-9BE7-0869FDC726DB', 2)
INSERT INTO dbo.SurveyDetailQuestions(SurveyDetailId,QuestionBankId) VALUES('2C343747-EF7D-486F-9BE7-0869FDC726DB', 3) 
INSERT INTO dbo.SurveyDetailQuestions(SurveyDetailId,QuestionBankId) VALUES('2C343747-EF7D-486F-9BE7-0869FDC726DB', 4)
INSERT INTO dbo.SurveyDetailQuestions(SurveyDetailId,QuestionBankId) VALUES('2C343747-EF7D-486F-9BE7-0869FDC726DB', 5)
GO

INSERT INTO	[dbo].UserAccounts (UserAccountId, FirstName, LastName, Email, Password) VALUES('5251B836-603B-4213-891B-2D798E9B1A73','John', 'Doe', 'john.doe@gmail.com', '4Eaa3djVejYjSUCW2rwZvryhoDjJ2mlpQLP4U9EGpuz6W9YM6OcohO+jvZK5MNoXj9YW9A+srWVCEtfC+IF91A==')
INSERT INTO	[dbo].UserAccounts (UserAccountId, FirstName, LastName, Email, Password) VALUES('C78404D5-B7C8-4D81-8321-64459F9ED86F','Sarah', 'Smith', 'sara.smith@gmail.com', '4Eaa3djVejYjSUCW2rwZvryhoDjJ2mlpQLP4U9EGpuz6W9YM6OcohO+jvZK5MNoXj9YW9A+srWVCEtfC+IF91A==')
GO

INSERT INTO	dbo.Respondents(SurveyDetailId,UserAccountId,SendTime,SubmitTime) VALUES('2C343747-EF7D-486F-9BE7-0869FDC726DB', '5251B836-603B-4213-891B-2D798E9B1A73', GETDATE(), NULL)
INSERT INTO	dbo.Respondents(SurveyDetailId,UserAccountId,SendTime,SubmitTime) VALUES('2C343747-EF7D-486F-9BE7-0869FDC726DB', 'C78404D5-B7C8-4D81-8321-64459F9ED86F', GETDATE(), NULL)
GO	