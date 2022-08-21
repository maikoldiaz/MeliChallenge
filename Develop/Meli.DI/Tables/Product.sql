CREATE TABLE Products(
    Id VARCHAR(20) NOT NULL,
    SiteId VARCHAR(3),
    Title VARCHAR(MAX),
    Price DECIMAL NOT NULL,
    BasePrice DECIMAL,
    LikesNumber INT DEFAULT 1
)
ALTER TABLE dbo.Products ADD CONSTRAINT UQ_ProductId UNIQUE (Id)
GO