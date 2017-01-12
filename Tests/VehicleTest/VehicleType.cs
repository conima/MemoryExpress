namespace Tests.VehicleTest
{
    /// <summary>
    /// Type of vehicle
    /// </summary>
    public enum VehicleType
    {
        /// <summary>
        /// Cars have 4 wheels, and the horn goes "honk"
        /// </summary>
        Car,
        /// <summary>
        /// Trucks have 4 wheels, and the horn goes "braap"
        /// </summary>
        Truck,
        /// <summary>
        /// Motorbikes have 2 wheels, and the horn goes "beep"
        /// </summary>
        Motorbike
    }

    public enum Direction { Forward, Backward, Left, Right }

    public enum Horn { Honk, Braap, Beep }
}