CREATE TABLE Product(
    Id INT IDENTITY(1,1) NOT NULL,
    SiteId VARCHAR(3),
    Title VARCHAR(MAX),
    Price DECIMAL NOT NULL,
    BasePrice DECIMAL,
    likesNumber INT DEFAULT 0
)