using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ImproveTest
{
    /// <summary>
    /// Please see Improve.cs for details on how to complete this test. For information
    /// about how this test class works, see the region at the bottom of this file.
    /// </summary>
    [TestClass]
    public class Test
    {
        /// <summary>
        /// Tests the method's guarding logic
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null()
        {
            var service = InstanceProvider.GetInstance<Improve>(() => new Improve(_auctionService));
            service.GetSellingPrice(null);
        }

        /// <summary>
        /// Tests a basic scenario
        /// </summary>
        [TestMethod]
        public void Basic()
        {
            var value = BasicTest(CarType.Sedan, age: 5);

            Assert.AreEqual(11606.71m, value);
        }

        /// <summary>
        /// Test for the expected behavior of the Sedan CarType
        /// </summary>
        [TestMethod]
        public void Simple_Type_Sedan()
        {
            var value = BasicTest(CarType.Sedan);

            Assert.AreEqual(15000m, value);
        }

        /// <summary>
        /// Test for the expected behavior of the Coupe CarType
        /// </summary>
        [TestMethod]
        public void Simple_Type_Coupe()
        {
            var value = BasicTest(CarType.Coupe);

            Assert.AreEqual(13000m, value);
        }

        /// <summary>
        /// Test for the expected behavior of the Hatchback CarType
        /// </summary>
        [TestMethod]
        public void Simple_Type_Hatchback()
        {
            var value = BasicTest(CarType.Hatchback);

            Assert.AreEqual(18000m, value);
        }

        /// <summary>
        /// Test for the expected behavior of the PickupTruck CarType
        /// </summary>
        [TestMethod]
        public void Simple_Type_PickupTruck()
        {
            var value = BasicTest(CarType.PickupTruck);

            Assert.AreEqual(25000m, value);
        }

        /// <summary>
        /// Tests depreciation of the value based on the age of a vehicle with no accidents.
        /// </summary>
        [TestMethod]
        public void Depreciation_NoAccidents()
        {
            Assert.AreEqual(14250.00m, BasicTest(CarType.Sedan, age: 1));
            Assert.AreEqual(13537.50m, BasicTest(CarType.Sedan, age: 2));
            Assert.AreEqual(12860.63m, BasicTest(CarType.Sedan, age: 3));
            Assert.AreEqual(12217.59m, BasicTest(CarType.Sedan, age: 4));
            Assert.AreEqual(11606.71m, BasicTest(CarType.Sedan, age: 5));
        }

        /// <summary>
        /// Tests depreciation of the value based on the age of a vehicle with one accident.
        /// </summary>
        [TestMethod]
        public void Depreciation_OneAccident()
        {
            Assert.AreEqual(12750.00m, BasicTest(CarType.Sedan, age: 1, accidents: 1));
            Assert.AreEqual(10837.50m, BasicTest(CarType.Sedan, age: 2, accidents: 1));
            Assert.AreEqual(09211.88m, BasicTest(CarType.Sedan, age: 3, accidents: 1));
            Assert.AreEqual(07830.09m, BasicTest(CarType.Sedan, age: 4, accidents: 1));
            Assert.AreEqual(06655.58m, BasicTest(CarType.Sedan, age: 5, accidents: 1));
        }

        /// <summary>
        /// Tests depreciation of the value based on the age of a vehicle with two accidents.
        /// </summary>
        [TestMethod]
        public void Depreciation_TwoAccidents()
        {
            Assert.AreEqual(11250.00m, BasicTest(CarType.Sedan, age: 1, accidents: 2));
            Assert.AreEqual(08437.50m, BasicTest(CarType.Sedan, age: 2, accidents: 2));
            Assert.AreEqual(06328.13m, BasicTest(CarType.Sedan, age: 3, accidents: 2));
            Assert.AreEqual(04746.09m, BasicTest(CarType.Sedan, age: 4, accidents: 2));
            Assert.AreEqual(03559.57m, BasicTest(CarType.Sedan, age: 5, accidents: 2));
        }

        /// <summary>
        /// Tests depreciation of the value based on the age of a vehicle with more than two accidents.
        /// </summary>
        /// <remarks>
        /// Note: Should be the same behavior as two accidents.
        /// </remarks>
        [TestMethod]
        public void Depreciation_MoreThanTwoAccidents()
        {
            Assert.AreEqual(11250.00m, BasicTest(CarType.Sedan, age: 1, accidents: 12));
            Assert.AreEqual(08437.50m, BasicTest(CarType.Sedan, age: 2, accidents: 12));
            Assert.AreEqual(06328.13m, BasicTest(CarType.Sedan, age: 3, accidents: 12));
            Assert.AreEqual(04746.09m, BasicTest(CarType.Sedan, age: 4, accidents: 12));
            Assert.AreEqual(03559.57m, BasicTest(CarType.Sedan, age: 5, accidents: 12));
        }

        /// <summary>
        /// Tests that if selling on the online market, the price is reduced if 
        /// the normally calculated price is over $5000
        /// </summary>
        [TestMethod]
        public void OnlineMarket_PriceReduced_IfOver5000()
        {
            Assert.AreEqual(13250.00m, BasicTest(CarType.Sedan, age: 1, market: CarMarket.Online));
            Assert.AreEqual(12537.50m, BasicTest(CarType.Sedan, age: 2, market: CarMarket.Online));
            Assert.AreEqual(11860.63m, BasicTest(CarType.Sedan, age: 3, market: CarMarket.Online));
            Assert.AreEqual(11217.59m, BasicTest(CarType.Sedan, age: 4, market: CarMarket.Online));
            Assert.AreEqual(10606.71m, BasicTest(CarType.Sedan, age: 5, market: CarMarket.Online));
        }

        /// <summary>
        /// Tests that if selling on the online market, the price is not reduced if 
        /// the normally calculated price is under $5000
        /// </summary>
        [TestMethod]
        public void OnlineMarket_PriceNotReduced_IfUnder5000()
        {
            Assert.AreEqual(04853.00m, BasicTest(CarType.Sedan, age: 22, market: CarMarket.Online));
            Assert.AreEqual(04610.35m, BasicTest(CarType.Sedan, age: 23, market: CarMarket.Online));
            Assert.AreEqual(04379.84m, BasicTest(CarType.Sedan, age: 24, market: CarMarket.Online));
            Assert.AreEqual(04160.84m, BasicTest(CarType.Sedan, age: 25, market: CarMarket.Online));
            Assert.AreEqual(03952.80m, BasicTest(CarType.Sedan, age: 26, market: CarMarket.Online));
        }

        /// <summary>
        /// Tests that when selling on the auction market the base price is reduced by half.
        /// </summary>
        [TestMethod]
        public void AuctionMarket_HalvesPrice()
        {
            Assert.AreEqual(07500.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction));
        }

        /// <summary>
        /// Tests that when selling on the auction market, the base price is reduced by half
        /// BEFORE depreciation is applied.
        /// </summary>
        [TestMethod]
        public void AuctionMarket_HalvesPrice_BeforeCompounding()
        {
            Assert.AreEqual(07125.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, age: 1));
            Assert.AreEqual(06768.75m, BasicTest(CarType.Sedan, market: CarMarket.Auction, age: 2));
            Assert.AreEqual(06430.31m, BasicTest(CarType.Sedan, market: CarMarket.Auction, age: 3));
            Assert.AreEqual(06108.80m, BasicTest(CarType.Sedan, market: CarMarket.Auction, age: 4));
            Assert.AreEqual(05803.36m, BasicTest(CarType.Sedan, market: CarMarket.Auction, age: 5));
        }

        /// <summary>
        /// Tests that when selling on the auction market, the base prices is increased by the
        /// amount provided by the auction service call.
        /// </summary>
        [TestMethod]
        public void AuctionMarket_AppliesSurcharge()
        {
            // This makes the auction service return 1000
            _auctionService.Intercept = (t, a) => 1000m;

            Assert.AreEqual(08500.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction));
        }

        /// <summary>
        /// Tests that when selling on the auction market, the base prices is increased by the
        /// amount provided by the auction service call BEFORE depreciation is applied.
        /// </summary>
        [TestMethod]
        public void AuctionMarket_AppliesSurcharge_BeforeCompounding()
        {
            // This makes the auction service return 1000
            _auctionService.Intercept = (t, a) => 1000m;

            Assert.AreEqual(08075.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, age: 1));
            Assert.AreEqual(07671.25m, BasicTest(CarType.Sedan, market: CarMarket.Auction, age: 2));
            Assert.AreEqual(07287.69m, BasicTest(CarType.Sedan, market: CarMarket.Auction, age: 3));
            Assert.AreEqual(06923.30m, BasicTest(CarType.Sedan, market: CarMarket.Auction, age: 4));
            Assert.AreEqual(06577.14m, BasicTest(CarType.Sedan, market: CarMarket.Auction, age: 5));
        }

        /// <summary>
        /// A measuring test that tests the performance of successive calls to the same
        /// advertisement configuration.
        /// 
        /// Note: See the comments/run details of this test for a measure of the test speed.
        /// </summary>
        [TestMethod]
        public void AuctionMarket_PerformanceOptimization_RepeatedCall()
        {
            // Introduce an artificial delay
            _auctionService.Delay = 1000;

            // Modify the behavior of the auction service
            _auctionService.Intercept = (t, a) =>
            {
                if (t == CarType.Sedan)
                    return 500m * (a + 1);
                if (t == CarType.Coupe)
                    return 400m * (a + 1);
                if (t == CarType.Hatchback)
                    return 300m * (a + 1);
                if (t == CarType.PickupTruck)
                    return 200m * (a + 1);

                return 0m;
            };

            var service = InstanceProvider.GetInstance<Improve>(() => new Improve(_auctionService));

            var timer = Stopwatch.StartNew();

            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));
            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));

            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));
            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));

            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));
            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));

            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));
            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));

            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));
            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));

            timer.Stop();

            Console.WriteLine($"Test ran in {timer.Elapsed}.");
        }

        /// <summary>
        /// A measuring test that tests the performance of successive calls to a few different
        /// advertisement configurations.
        /// 
        /// Note: See the comments/run details of this test for a measure of the test speed.
        /// </summary>
        [TestMethod]
        public void AuctionMarket_PerformanceOptimization_MixedCalls()
        {
            // Introduce an artificial delay
            _auctionService.Delay = 1000;

            // Modify the behavior of the auction service
            _auctionService.Intercept = (t, a) =>
            {
                if (t == CarType.Sedan)
                    return 500m * (a + 1);
                if (t == CarType.Coupe)
                    return 400m * (a + 1);
                if (t == CarType.Hatchback)
                    return 300m * (a + 1);
                if (t == CarType.PickupTruck)
                    return 200m * (a + 1);

                return 0m;
            };

            var service = InstanceProvider.GetInstance<Improve>(() => new Improve(_auctionService));

            var timer = Stopwatch.StartNew();

            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));
            Assert.AreEqual(08100.00m, BasicTest(CarType.Coupe, market: CarMarket.Auction, service: service, accidents: 3));

            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));
            Assert.AreEqual(08100.00m, BasicTest(CarType.Coupe, market: CarMarket.Auction, service: service, accidents: 3));

            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));
            Assert.AreEqual(08100.00m, BasicTest(CarType.Coupe, market: CarMarket.Auction, service: service, accidents: 3));

            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));
            Assert.AreEqual(08100.00m, BasicTest(CarType.Coupe, market: CarMarket.Auction, service: service, accidents: 3));

            Assert.AreEqual(08000.00m, BasicTest(CarType.Sedan, market: CarMarket.Auction, service: service));
            Assert.AreEqual(08100.00m, BasicTest(CarType.Coupe, market: CarMarket.Auction, service: service, accidents: 3));

            timer.Stop();

            Console.WriteLine($"Test ran in {timer.Elapsed}.");
        }

        #region Core Testing Logic

        public AuctionServiceStub _auctionService;

        [TestInitialize]
        public void TestInit()
        {
            // Initialize the testing Auction Service Stub
            _auctionService = new AuctionServiceStub();
        }

        /// <summary>
        /// The entry point for a unit test, represents the basic structure of a test-case.
        /// </summary>
        /// <remarks>
        /// This is mostly structural code for the unit tests.
        /// </remarks>
        /// <param name="carType">The car type to use</param>
        /// <param name="age">The age of the car</param>
        /// <param name="accidents">The number of accidents</param>
        /// <param name="market">The market to sell on</param>
        /// <param name="service">(Optional) Specify a service to run against, leaving this blank will create one.</param>
        /// <returns></returns>
        private decimal BasicTest(
            CarType carType,
            int age = 0,
            int accidents = 0,
            CarMarket market = CarMarket.Lot,
            Improve service = null)
        {
            // Construct the advertisement to test
            var advertisement = new CarAdvertisement()
            {
                Type = carType,
                Age = age,
                Accidents = accidents,
                SellingMarket = market
            };

            // If the service wasn't provided, let's provide one.
            if (service == null)
            {
                // Request an instance from the provider.
                service = InstanceProvider.GetInstance<Improve>(() => new Improve(_auctionService));
            }

            // Run the test case
            return service.GetSellingPrice(advertisement);
        }

        #endregion
    }
}
