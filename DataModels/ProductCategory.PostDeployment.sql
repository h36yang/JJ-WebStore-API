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

IF NOT EXISTS ( SELECT 1 FROM dbo.ProductCategory WHERE [Name] = 'Featured' )
	INSERT INTO dbo.ProductCategory ( [Name] ) VALUES ( 'Featured' )
;

IF NOT EXISTS ( SELECT 1 FROM dbo.ProductCategory WHERE [Name] = 'Hot' )
	INSERT INTO dbo.ProductCategory ( [Name] ) VALUES ( 'Hot' )
;
