
CREATE DATABASE SaabRentals

GO

USE SaabRentals

GO

CREATE TABLE [dbo].Bookings (
    [BookingId]			INT	   IDENTITY (1, 1) NOT NULL,
    [SocialSecurity]    VARCHAR (12)  NOT NULL,
    [CarType]			VARCHAR (50)  NOT NULL,
    [Date]				DATETIME	  NOT NULL,
    [StartKilometers]	INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([BookingId] ASC),
	CONSTRAINT C_UNIQUE_ID UNIQUE(BookingId)
);

GO

INSERT INTO [dbo].[Bookings] ([SocialSecurity], [CarType], [Date], [StartKilometers]) VALUES (960119, 'Bil', '2020-11-10 00:00:00.000', 0)
INSERT INTO [dbo].[Bookings] ([SocialSecurity], [CarType], [Date], [StartKilometers]) VALUES (560412, 'Van', '2020-11-09 00:00:00.000', 0)
INSERT INTO [dbo].[Bookings] ([SocialSecurity], [CarType], [Date], [StartKilometers]) VALUES (780315, 'Buss', '2020-11-07 00:00:00.000', 0)
INSERT INTO [dbo].[Bookings] ([SocialSecurity], [CarType], [Date], [StartKilometers]) VALUES (920319, 'Bil', '2020-11-10 00:00:00.000', 0)
INSERT INTO [dbo].[Bookings] ([SocialSecurity], [CarType], [Date], [StartKilometers]) VALUES (930821, 'Bil', '2020-11-10 00:00:00.000', 0)

GO