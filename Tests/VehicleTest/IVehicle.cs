using System.Drawing;

namespace Tests.VehicleTest
{
    /// <summary>
    /// A vehicle
    /// <para>All vehicles should start at position 0x 0y</para>
    /// <para>The vehicles should be facing y+ (north), on an x and y grid (Cartesian coordinates)</para>
    /// <para>The vehicles are assumed to be a single point.</para>
    /// </summary>
    public interface IVehicle
    {
        /// <summary>
        /// Moves the vehicle in the direction it is facing
        /// </summary>
        void MoveForward(int units);

        /// <summary>
        /// Turns vehicle 90deg right
        /// </summary>
        void TurnRight();

        /// <summary>
        /// Turns vehicle 90deg left
        /// </summary>
        void TurnLeft();

        /// <summary>
        /// Sounds the vehicle horn
        /// </summary>
        string SoundHorn();

        /// <summary>
        /// Returns the current position of the vehicle
        /// </summary>
        Point CurrentPosition { get; }

        /// <summary>
        /// Return the number of wheels on the vehicle
        /// </summary>
        int NumberOfWheels { get; }

        /// <summary>
        /// Returns the type of vehicle we are using
        /// </summary>
        VehicleType VehicleType { get; }
    }
}