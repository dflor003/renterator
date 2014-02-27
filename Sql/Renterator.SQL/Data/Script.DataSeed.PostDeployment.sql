/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[Users] ON
GO

INSERT [dbo].[Users] ([Id], [Email], [PasswordHash], [FirstName], [LastName], [IsActive], [IsAdmin], [LastLoginDate]) VALUES (1, N'dflor003@gmail.com', N'1000:ac7mgILfsS7SHqquLOUB9vTxJyJzkZD9:JE93uqKf6wOfHNxzQkkROC9r9nMXJmxI', N'Danil', N'Flores', 1, 1, CAST(0x0000A2DC013EC893 AS DateTime))
INSERT [dbo].[Users] ([Id], [Email], [PasswordHash], [FirstName], [LastName], [IsActive], [IsAdmin], [LastLoginDate]) VALUES (2, N'bob.builder@gmail.com', N'1000:wS2JgsGMdJNm0sHBj+SdZ0HhKkvC0Bs7:Ew+mD6GpPbPSKnYX5QTm9G1orFHAdDLX', N'Bob', N'Smith', 1, 0, CAST(0x0000A2DC013EC893 AS DateTime))
GO

SET IDENTITY_INSERT [dbo].[Users] OFF
GO

SET IDENTITY_INSERT [dbo].[Accounts] ON
GO

INSERT [dbo].[Accounts] ([Id], [UserId], [Name], [Description]) VALUES(1, 1, N'Rent', N'Rent account')
INSERT [dbo].[Accounts] ([Id], [UserId], [Name], [Description]) VALUES(2, 2, N'Rent', N'Rent account')
GO

SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO

SET IDENTITY_INSERT [dbo].[AccountEntries] ON
GO

INSERT [dbo].[AccountEntries] ([Id],[AccountId],[Date],[Description],[Amount])
VALUES 
    (1,  2, '2013-6-12' , 'Cable bill June', -41.34),
    (2,  2, '2013-6-14' , 'Power bill June', -45.75),
    (3,  2, '2013-7-12' , 'Power bill July', -43.87),
    (4,  2, '2013-7-13' , 'Cable bill July', -41.34),
    (5,  2, '2013-8-1'  , 'Rent August', -375),
    (6,  2, '2013-8-12' , 'Payment Rent + Utilities', 547.3),
    (7,  2, '2013-8-12' , 'Water Bill June - August', -53.26),
    (8,  2, '2013-8-12' , 'Power bill August', -64.12),
    (9,  2, '2013-8-13' , 'Cable bill August', -55.12),
    (10, 2, '2013-8-30' , 'Rent September', -600),
    (11, 2, '2013-8-31' , 'Payment Rent', 653.26),
    (12, 2, '2013-9-12' , 'Power Bill September', -66.81),
    (13, 2, '2013-9-13' , 'Cable Bill September', -55.12),
    (14, 2, '2013-9-16' , 'Payment Utilities', 119.24),
    (15, 2, '2013-10-1' , 'Rent October', -400),
    (16, 2, '2013-10-1' , 'Payment Rent October', 400),
    (17, 2, '2013-10-13', 'Cable Bill October', -41.34),
    (18, 2, '2013-10-15', 'Power Bill October', -36.75),
    (19, 2, '2013-11-1' , 'Rent November', -400),
    (20, 2, '2013-11-3' , 'Payment Rent + Utilities', 521.93),
    (21, 2, '2013-11-12', 'Power Bill November', -45.48),
    (22, 2, '2013-11-15', 'Cable Bill November', -41.34),
    (23, 2, '2013-11-28', 'Rent December', -400),
    (24, 2, '2013-11-28', 'Water Bill September - November', -22.32),
    (25, 2, '2013-11-29', 'Payment Rent + Utilities', 587.23),
    (26, 2, '2013-12-13', 'Power Bill December', -49.43),
    (27, 2, '2013-12-15', 'Cable Bill December', -55.12),
    (28, 2, '2013-12-31', 'Rent January', -533.33),
    (29, 2, '2014-1-16' , 'Payment Rent + Utilities', 647.88),
    (30, 2, '2014-2-1'  , 'Rent February', -533.33),
    (31, 2, '2014-2-5'  , 'Water Bill', -32.77),
    (32, 2, '2014-1-13' , 'Cable Bill January', -55.12),
    (33, 2, '2014-1-13' , 'Power Bill January', -46.52);

GO

SET IDENTITY_INSERT [dbo].[AccountEntries] OFF
GO

SET IDENTITY_INSERT [dbo].[BillTypes] ON
GO

INSERT [dbo].[BillTypes] ([Id], [Name])
VALUES	
    (1, 'Uncategorized'),
    (2, 'Rent'),
    (3, 'Electric Bill'),
    (4, 'Cable/Internet Bill'),
    (5, 'Water Bill');
GO

SET IDENTITY_INSERT [dbo].[BillTypes] OFF
GO

--SET IDENTITY_INSERT [dbo].[Bills] ON
--GO

INSERT [dbo].[Bills] ([BillTypeId], [Date], [Description], [Amount])
VALUES
    (3, '2011-10-24',  'September Power Bill', 143.63 ),
    (3, '2011-11-21',  'October Power Bill', 124.62 ),
    (3, '2011-12-23',  'December Power Bill', 129.94 ),
    (3, '2012-1-24',  'January Power Bill', 144.28 ),
    (3, '2012-2-22',  'February Power Bill', 97.96 ),
    (3, '2012-3-22',  'March Power Bill', 118.50 ),
    (3, '2012-4-22',  'April Power Bill', 127.52 ),
    (3, '2012-5-22',  'May Power Bill', 117.52 ),
    (3, '2012-6-22',  'June Power Bill', 137.70 ),
    (3, '2012-7-22',  'July Power Bill', 170.92 ),
    (3, '2012-8-22',  'August Power Bill', 167.18 ),
    (3, '2012-9-13',  'September Power Bill', 177.52 ),
    (3, '2012-10-12',  'October Power Bill', 161.49 ),
    (3, '2012-11-13',  'November Power Bill', 128.14 ),
    (3, '2012-12-13',  'December Power Bill', 106.09 ),
    (3, '2013-1-14',  'January Power Bill', 106.67 ),
    (3, '2013-2-14',  'February Power Bill', 123.83 ),
    (3, '2013-3-12',  'March Power Bill', 118.58 ),
    (3, '2013-4-15',  'April Power Bill', 111.81 ),
    (3, '2013-5-12',  'May Power Bill', 153.11 ),
    (3, '2013-6-14',  'June Power Bill', 183.00 ),
    (3, '2013-7-12', 'July Power Bill', 175.47),
    (3, '2013-8-12', 'August Power Bill', 192.36),
    (3, '2013-9-12', 'September Power Bill', 200.44),
    (3, '2013-10-15', 'October Power Bill', 146.98),
    (3, '2013-11-12', 'November Power Bill', 181.93),
    (3, '2013-12-13', 'December Power Bill', 148.30),
    (3, '2014-1-13', 'January Power Bill', 139.57);

INSERT [dbo].[Bills] ([BillTypeId], [Date], [Description], [Amount])
VALUES
    (4, '2012-12-13', 'Cable bill December', 157.83 ),
    (4, '2012-1-13', 'Cable bill January', 157.83 ),
    (4, '2013-2-12', 'Cable bill February', 157.83 ),
    (4, '2013-3-16', 'Cable bill March', 165.37 ),
    (4, '2013-4-13', 'Cable bill April', 165.37 ),
    (4, '2013-5-14', 'Cable bill May', 165.37 ),
    (4, '2013-6-12', 'Cable bill June', 165.37 ),
    (4, '2013-7-13', 'Cable bill July', 165.37 ),
    (4, '2013-8-13', 'Cable bill August', 165.37 ),
    (4, '2013-9-13', 'Cable bill September', 165.37 ),
    (4, '2013-10-13', 'Cable bill October', 165.37 ),
    (4, '2013-11-15', 'Cable bill November', 165.37 ),
    (4, '2013-12-15', 'Cable bill December', 165.37 ),
    (4, '2014-1-13', 'Cable bill January', 165.37 );

INSERT [dbo].[Bills] ([BillTypeId], [Date], [Description], [Amount])
VALUES
    (5, '2011-11-28', 'Water Bill', 91.12),
    (5, '2012-12-6', 'Water Bill', 226.74),
    (5, '2012-5-25', 'Water Bill', 118.16),
    (5, '2012-8-24', 'Water Bill', 151.51),
    (5, '2012-11-26', 'Water Bill', 128.12),
    (5, '2013-5-27', 'Water Bill', 109.35),
    (5, '2013-8-26', 'Water Bill', 213.05),
    (5, '2013-2-5', 'Water Bill', 98.30);
--SET IDENTITY_INSERT [dbo].[Bills] ON
--GO