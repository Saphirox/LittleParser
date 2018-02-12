CREATE DATABASE LittleParserDB;

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

INSERT INTO ApacheLogs(Host, Route, StatusCode, DateTimeOffset, ContentSize, Geolocation, QueryParameterId) 
                        VALUES('192.123.1.1', '/LocalHost/', 200, GETDATE(), 65411, 'Kiev/Ukraine', 1);

DECLARE @Start DateTimeOffset;
SET @Start = '12-10-10 12:32:10.1237 +01:0';

DECLARE @End DateTimeOffset;
SET @End = '12-10-25 12:32:10.1237 +01:0';

DECLARE @Limit INT;
SET @Limit = 10;

DECLARE @Offset INT;
SET @Offset = 0;

-- SELECT * FROM ApacheLogs WHERE @Start <= DateTimeOffset AND @End >= DateTimeOffset 
-- ORDER BY DateTimeOffset ASC OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY

SELECT * FROM ApacheLogs AL 
WHERE @Start <= DateTimeOffset AND @End >= DateTimeOffset 
ORDER BY DateTimeOffset ASC OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY
JOIN QueryParameters QP ON QP.[Id] = AL.[QueryParameterId]

-- SELECT [Host] FROM ApacheLogs WHERE @Start <= DateTimeOffset AND @End >= DateTimeOffset 
--             ORDER BY [DateTimeOffset] DESC
--             OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY
            
-- SELECT [Route] FROM ApacheLogs WHERE @Start <= DateTimeOffset AND @End >= DateTimeOffset 
-- ORDER BY [DateTimeOffset] DESC
-- OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY

DROP TABLE ApacheLogs;
DROP TABLE QueryParameters;
DROP DATABASE LittleParserDB;