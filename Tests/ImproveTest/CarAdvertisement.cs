namespace Tests.ImproveTest
{
    /// <summary>
    /// A car advertisement definition
    /// 
    /// DO NOT MODIFY THIS CLASS
    /// </summary>
    public class CarAdvertisement
    {
        /// <summary>
        /// The type of car we're selling
        /// </summary>
        public CarType Type { get; set; }

        /// <summary>
        /// How many years old the car is
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// How many accidents the car has been in
        /// </summary>
        public int Accidents { get; set; }

        /// <summary>
        /// Which market we're selling the car to
        /// </summary>
        public CarMarket SellingMarket { get; set; }
    }
}