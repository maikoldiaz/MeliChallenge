namespace UnitTest;

using Moq;
using Meli.Processor.Interfaces;
using Meli.Processor;
using Meli.Proxies.Interfaces;
using Meli.Proxies;
using Meli.Entities;
using Meli.DataAccess;
using Meli.DataAccess.Interfaces;

[TestClass]
public class CouponControllerTest
{
    private CouponProcessor? processor;
    private Mock<ICouponProxy>? mockCouponProxy;
    private Mock<IProductProcessor>? mockProductProcessor;
    private Mock<ICouponProcessor>? mockCouponProcessor;
    /// <summary>
    /// The unit of work instance.
    /// </summary>
    private UnitOfWork? unitOfWork;

    /// <summary>
    /// The mock data context.
    /// </summary>
    private Mock<IDataContext>? mockDataContext;

    /// <summary>
    /// The mock repository factory.
    /// </summary>
    private Mock<IUnitOfWorkFactory>? mockUnitOfWorkFactory;
    private Mock<IRepositoryFactory>? mockRepositoryFactory;
    private List<Product>? products;
    private CouponResponse? couponResponse;

    [TestInitialize]
    public void Initialize()
    {
        BuildData();
        this.mockCouponProxy = new Mock<ICouponProxy>();
        this.mockProductProcessor = new Mock<IProductProcessor>();
        this.mockUnitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
        this.mockRepositoryFactory = new Mock<IRepositoryFactory>();
        this.mockCouponProcessor = new Mock<ICouponProcessor>();
        this.mockDataContext = new Mock<IDataContext>();
        this.unitOfWork = new UnitOfWork(this.mockDataContext!.Object, this.mockRepositoryFactory.Object);
        this.processor = new CouponProcessor(
               this.mockCouponProxy.Object,
               this.mockUnitOfWorkFactory.Object,
               this.mockProductProcessor!.Object);
    }

    [TestMethod]
    public async Task GetCouponAsync_ShouldBySuccess()
    {
        var coupon = new Coupon
        {
            ItemIds = this.products!.Select(x => x.Id).ToList(),
            Amount = 200
        };
        mockCouponProxy!.Setup(x => x.ObtainProductAsync(It.IsAny<string>())).ReturnsAsync(products![0]);
        mockCouponProcessor!.Setup(x => x.GetProductsFromCouponAsync(It.IsAny<List<string>>())).ReturnsAsync(products);
        var result = await this.processor!.GetCouponAsync(coupon);
        this.mockCouponProxy!.Verify(x => x.ObtainProductAsync(It.IsAny<string>()), Times.AtLeastOnce);
    }

    [TestMethod]
    public async Task CalculateCouponAsync_ShuldBySuccess()
    {
        var result = await this.processor!.CalculateCouponAsync(this.products!, 500);
        Assert.AreEqual(this.couponResponse!.Total, result.Total);
    }

    private void BuildData(){
        this.products = new List<Product> {
            new Product {
                Id = "MELI01",
                BasePrice = 100,
                Price = 100,
                SiteId = "MCO",
                Title = "Test1"
            },
            new Product {
                Id = "MELI02",
                BasePrice = 30,
                Price = 210,
                SiteId = "MCO",
                Title = "Test2"
            },
            new Product {
                Id = "MELI03",
                BasePrice = 80,
                Price = 260,
                SiteId = "MCO",
                Title = "Test3"
            },
            new Product {
                Id = "MELI04",
                BasePrice = 80,
                Price = 80,
                SiteId = "MCO",
                Title = "Test3"
            },
            new Product {
                Id = "MELI05",
                BasePrice = 80,
                Price = 90,
                SiteId = "MCO",
                Title = "Test3"
            }
        };

        this.couponResponse = new CouponResponse
        {
            ItemIds = this.products.Where(x => x.Id != "MELI03").Select(p => p.Id).ToList(),
            Total = 480
        };
    }
}