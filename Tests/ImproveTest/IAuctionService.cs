namespace Tests.ImproveTest
{
    public interface IAuctionService
    {
        /// <summary>
        /// Gets a surcharge for selling a vehicle on the auction service.
        /// 
        /// DO NOT MODIFY THIS CLASS
        /// </summary>
        /// <param name="carType">The car type</param>
        /// <param name="accidents">The number of accidents</param>
        /// <returns></returns>
        decimal GetSurcharge(CarType carType, int accidents);
    }
}
