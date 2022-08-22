CREATE TABLE Products(
    Id VARCHAR(20) NOT NULL,
    SiteId VARCHAR(3),
    Title VARCHAR(MAX),
    Price NUMERIC NOT NULL,
    BasePrice NUMERIC,
    LikesNumber INT DEFAULT 1
)
ALTER TABLE dbo.Products ADD CONSTRAINT UQ_ProductId UNIQUE (Id)
GO