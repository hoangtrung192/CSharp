-- Run with a Windows account that can create databases on this SQL Server.
USE [master];
GO
IF DB_ID(N'LuxuryBridalStudio') IS NULL
    CREATE DATABASE [LuxuryBridalStudio] COLLATE Latin1_General_100_CI_AS;
GO
