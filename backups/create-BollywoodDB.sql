USE [master]
GO

/*
 * Setup database
 *
 */
DROP DATABASE IF EXISTS [BollywoodDB];
GO
CREATE DATABASE [BollywoodDB];
GO

USE [BollywoodDB];
GO

-- Create Actors table
CREATE TABLE [dbo].[Actors]
(
 [ActorId] [int] NOT NULL,
 [FirstName] [nvarchar](50) NOT NULL,
 [LastName] [nvarchar](50) NOT NULL, 
 [Email] [nvarchar](50) NOT NULL,
 [City] [nvarchar](50) NULL,
 [MobileNumber] [nvarchar](50) NOT NULL

 PRIMARY KEY CLUSTERED ([ActorId] ASC) ON [PRIMARY]
);
GO

-- Insert sample data into 'Actors' table
INSERT INTO [dbo].[Actors]([ActorId],[FirstName],[LastName],[Email],[City],[MobileNumber])
VALUES
(1, 'Amitabh', 'Bachchan', 'angry_young_man@gmail.com', 'Mumbai', '2620616212'),
(2, 'Aishwarya', 'Rai', 'ash@gmail.com', 'Mumbai', '9991206339'),
(3, 'Aamir', 'Khan', 'aamir@aamirkhan.com', 'Mumbai', '26050681'),
(4, 'Ranbir', 'Kapoor', 'ranbirkapoor@gmail.com', 'Mumbai', '9733846910'),
(5, 'Kareena', 'Kapoor', 'bebo@kapoor.org', 'Mumbai', '8007891721')
GO
