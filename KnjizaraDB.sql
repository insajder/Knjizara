
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [id_narudzbine]
      ,[id_osoba]
      ,[datum]
      ,[status]
  FROM [KnjizaraDB].[dbo].[Narudzbina]