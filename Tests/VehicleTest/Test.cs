using System.Drawing;
using Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.VehicleTest
{
    /// <summary>
    /// In this scenario you are a vehicle manufacturer,
    /// Impliment the interface <c>IVehicle</c> to create different kinds of vehicles using the <c>VehicleFactory</c>
    /// These vehicles are similar in some respects but different in others. Your vehicle must respond appropriately to its type when its methods are called.
    /// </summary>
    [TestClass]
    public class Test
    {
        /// <summary>
        /// This test uses the factory to create different types of vehicles.
        /// After the vehicle is created its type is checked to make sure it is correct
        /// </summary>
        [TestMethod]
        public void CanCreateDifferentVehicles()
        {
            var factory = InstanceProvider.GetInstance<VehicleFactory>();

            var car = factory.CreateVehicle(VehicleType.Car);

            Assert.IsNotNull(car);
            Assert.AreEqual(VehicleType.Car, car.VehicleType);

            var truck = factory.CreateVehicle(VehicleType.Truck);

            Assert.IsNotNull(truck);
            Assert.AreEqual(VehicleType.Truck, truck.VehicleType);

            var motorbike = factory.CreateVehicle(VehicleType.Motorbike);

            Assert.IsNotNull(motorbike);
            Assert.AreEqual(VehicleType.Motorbike, motorbike.VehicleType);
        }

        /// <summary>
        /// This test verifies that your vehicles can move
        /// <para>All vehicles should start at position 0x 0y</para>
        /// <para>The vehicles should be facing y+ (north), on an x and y grid (Cartesian coordinates)</para>
        /// <para>The vehicles are assumed to be a single point.</para>
        /// </summary>
        [TestMethod]
        public void VehicleCanMove()
        {
            var factory = InstanceProvider.GetInstance<VehicleFactory>();
            foreach (var vehicle in factory.CreateAllVehicles())
            {
                //All vehicles should start at 0,0
                Assert.AreEqual(vehicle.CurrentPosition, new Point(0, 0));

                vehicle.MoveForward(1);

                Assert.AreEqual(vehicle.CurrentPosition, new Point(0, 1));

                vehicle.TurnRight();
                vehicle.TurnRight();

                vehicle.MoveForward(8);

                Assert.AreEqual(vehicle.CurrentPosition, new Point(0, -7));

                vehicle.TurnLeft();

                vehicle.MoveForward(5);

                Assert.AreEqual(vehicle.CurrentPosition, new Point(5, -7));
            }
        }

        /// <summary>
        /// Makes sure that your vehicle can honk, different vehicles produce different sounds.
        /// <para>Cars honk, Trucks braap, motorbikes beep</para>
        /// </summary>
        [TestMethod]
        public void VehicleCanHonk()
        {
            var factory = InstanceProvider.GetInstance<VehicleFactory>();

            var car = factory.CreateVehicle(VehicleType.Car);

            Assert.AreEqual("honk", car.SoundHorn());

            var truck = factory.CreateVehicle(VehicleType.Truck);

            Assert.AreEqual("braap", truck.SoundHorn());

            var motorbike = factory.CreateVehicle(VehicleType.Motorbike);

            Assert.AreEqual("beep", motorbike.SoundHorn());
        }

        /// <summary>
        /// Verifies that your vehicle has the correct number of wheels
        /// <para>Cars and trucks have 4, motorcycles have 2</para>
        /// </summary>
        [TestMethod]
        public void VehicleHasRightNumberOfWheels()
        {
            var factory = InstanceProvider.GetInstance<VehicleFactory>();

            var car = factory.CreateVehicle(VehicleType.Car);

            Assert.AreEqual(4, car.NumberOfWheels);

            var truck = factory.CreateVehicle(VehicleType.Truck);

            Assert.AreEqual(4, truck.NumberOfWheels);

            var motorbike = factory.CreateVehicle(VehicleType.Motorbike);

            Assert.AreEqual(2, motorbike.NumberOfWheels);
        }
    }
}
