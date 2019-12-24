--/*==================================================User====================================*/

--INSERT INTO [dbo].[User] ([FirstName], [LastName], [E-mail]) VALUES (N'Janet', N'Castile', N'JanetRCastile@jourrapide.com');
--INSERT INTO [dbo].[User] ([FirstName], [LastName], [E-mail]) VALUES (N'Sara ', N'Jackson', N'SaraRJackson@jourrapide.com');
--INSERT INTO [dbo].[User] ([FirstName], [LastName], [E-mail]) VALUES (N'Helen', N'Alvarez', N'HelenCAlvarez@teleworm.us');
--INSERT INTO [dbo].[User] ([FirstName], [LastName], [E-mail]) VALUES (N'Janet', N'Brown', N'JeffreyABrown@jourrapide.com');
--INSERT INTO [dbo].[User] ([FirstName], [LastName], [E-mail]) VALUES (N'Carlos', N'Ullman', N'CarlosEUllman@teleworm.us');

--/*==================================================UserPrivilege====================================*/

--INSERT INTO [dbo].[UserPrivilege] ([User_ID], [Privilege_Name], [StartDate], [EndDate]) VALUES ((SELECT [ID] FROM [dbo].[User] WHERE [FirstName] = 'Janet' AND [LastName] = 'Castile'), N'AFAS', N'2019-12-12', N'2020-01-01');
--INSERT INTO [dbo].[UserPrivilege] ([User_ID], [Privilege_Name], [StartDate], [EndDate]) VALUES ((SELECT [ID] FROM [dbo].[User] WHERE [FirstName] = 'Janet' AND [LastName] = 'Castile'), N'OCC', N'2019-12-12', N'2020-01-01');
--INSERT INTO [dbo].[UserPrivilege] ([User_ID], [Privilege_Name], [StartDate], [EndDate]) VALUES ((SELECT [ID] FROM [dbo].[User] WHERE [FirstName] = 'Janet' AND [LastName] = 'Castile'), N'Outlook', N'2019-12-12', N'2020-02-01');
--INSERT INTO [dbo].[UserPrivilege] ([User_ID], [Privilege_Name], [StartDate], [EndDate]) VALUES ((SELECT [ID] FROM [dbo].[User] WHERE [FirstName] = 'Sara' AND [LastName] = 'Jackson'), N'Sleutel Postvak', N'2019-11-11', N'2020-02-01');
--INSERT INTO [dbo].[UserPrivilege] ([User_ID], [Privilege_Name], [StartDate], [EndDate]) VALUES ((SELECT [ID] FROM [dbo].[User] WHERE [FirstName] = 'Sara' AND [LastName] = 'Jackson'), N'AFAS', N'2019-09-12', N'2020-01-02');
--INSERT INTO [dbo].[UserPrivilege] ([User_ID], [Privilege_Name], [StartDate], [EndDate]) VALUES ((SELECT [ID] FROM [dbo].[User] WHERE [FirstName] = 'Helen' AND [LastName] = 'Alvarez'), N'AFAS', N'2019-12-11', N'2020-01-02');
--INSERT INTO [dbo].[UserPrivilege] ([User_ID], [Privilege_Name], [StartDate], [EndDate]) VALUES ((SELECT [ID] FROM [dbo].[User] WHERE [FirstName] = 'Helen' AND [LastName] = 'Alvarez'), N'Outlook', N'2019-08-11', N'2020-03-01');
--INSERT INTO [dbo].[UserPrivilege] ([User_ID], [Privilege_Name], [StartDate], [EndDate]) VALUES ((SELECT [ID] FROM [dbo].[User] WHERE [FirstName] = 'Helen' AND [LastName] = 'Alvarez'), N'AFAS', N'2019-11-08', N'2020-06-03');
--INSERT INTO [dbo].[UserPrivilege] ([User_ID], [Privilege_Name], [StartDate], [EndDate]) VALUES ((SELECT [ID] FROM [dbo].[User] WHERE [FirstName] = 'Helen' AND [LastName] = 'Alvarez'), N'Laptop', N'2019-10-09', N'2020-06-07');