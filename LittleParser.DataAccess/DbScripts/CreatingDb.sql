CREATE DATABASE LittleParserDB

USE LittleParserDB

CREATE TABLE ApacheLogs(
  [Id] BIGINT PRIMARY KEY IDENTITY,
  [Host] VARCHAR(255) NOT NULL,
  [Route] VARCHAR(255) NOT NULL,
  [Geolocation] VARCHAR(255) NOT NULL,
  [QueryParameters] VARCHAR(255),
  [StatusCode] INT NOT NULL,
  [DateTimeOffset] DateTimeOffset NOT NULL,
  [ContentSize] BIGINT NOT NULL
);
-- INSERT INTO QueryParameters([Key], [Value]) VALUES ('Key', 'Value');

-- Adding element
INSERT INTO ApacheLogs(Host, Route, StatusCode, DateTimeOffset, ContentSize, Geolocation, QueryParameters) 
                        VALUES('192.123.1.1', '/LocalHost/', 200, GETDATE(), 65411, 'Kiev/Ukraine', '123,31');

--DECLARE @Start DateTimeOffset;
--SET @Start = '12-10-10 12:32:10.1237 +01:0';

--DECLARE @End DateTimeOffset;
--SET @End = '12-10-25 12:32:10.1237 +01:0';

--DECLARE @Limit INT;
--SET @Limit = 10;

--DECLARE @Offset INT;
--SET @Offset = 0;

DROP TABLE ApacheLogs;
DROP TABLE QueryParameters;
DROP DATABASE LittleParserDB;