CREATE TYPE [dbo].[ProductList] AS TABLE(
	Id VARCHAR(20) NOT NULL,
    SiteId VARCHAR(3),
    Title VARCHAR(MAX),
    Price NUMERIC NOT NULL,
    BasePrice NUMERIC,
    LikesNumber INT
)