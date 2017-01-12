using System;
using System.Threading;

namespace Tests.ImproveTest
{
    /// <summary>
    /// This is a testing utility stub for the auction service, it's here to facilitate
    /// customizable behavior for the unit tests.
    /// 
    /// DO NOT MODIFY THIS CLASS
    /// </summary>
    public class AuctionServiceStub : IAuctionService
    {
        public int Delay { get; set; } = 0;
        public Func<CarType, int, decimal> Intercept { get; set; } = null;

        public decimal GetSurcharge(CarType carType, int accidents)
        {
            var delay = Math.Max(Delay, 0);
            if (delay != 0)
                Thread.Sleep(delay);

            return Intercept?.Invoke(carType, accidents) ?? 0m;
        }
    }
}