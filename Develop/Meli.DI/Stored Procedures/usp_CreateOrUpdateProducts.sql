CREATE PROCEDURE [dbo].[usp_CreateOrUpdateProducts]
(
    @Products [dbo].[ProductList] READONLY
)
AS
BEGIN
    BEGIN TRANSACTION;
    UPDATE dbo.Products
    SET likesNumber = p.likesNumber + 1
    FROM @Products pl
    INNER JOIN dbo.Products p ON pl.Id = p.Id

	print @@ROWCOUNT
    IF @@ROWCOUNT < (select count(id) from @Products)
        BEGIN
            INSERT into [dbo].[Products] (id, SiteId ,Title,Price, BasePrice)
			SELECT t.id, t.SiteId, t.Title, t.Price,t.BasePrice FROM @Products t left join dbo.Products p ON p.Id = t.Id WHERE p.id IS NULL
        END
    COMMIT TRANSACTION;
END