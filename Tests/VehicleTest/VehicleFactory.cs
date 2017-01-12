using System;
using System.Collections.Generic;
using Framework;

namespace Tests.VehicleTest
{
    /// <summary>
    /// Creates vehicles that adhere to the <c>IVehicle</c> interface
    /// </summary>
    public class VehicleFactory
    {
        public IEnumerable<IVehicle> CreateAllVehicles()
        {
            foreach (VehicleType vehicleType in Enum.GetValues(typeof(VehicleType)))
            {
                yield return CreateVehicle(vehicleType);
            }
        }

        /// <summary>
        /// Creates a vehcle of the specified type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Class that impliments </returns>
        public virtual IVehicle CreateVehicle(VehicleType type)
        {
            Vehicle vehicle = new Vehicle();

            switch (type)
            {
                case VehicleType.Car:
                    CarFactory car = new CarFactory();
                    vehicle = (Vehicle)car.CreateVehicle(VehicleType.Car);
                    break;
                case VehicleType.Truck:
                    TruckFactory truck = new TruckFactory();
                    vehicle = (Vehicle)truck.CreateVehicle(VehicleType.Truck);
                    break;
                case VehicleType.Motorbike:
                    MotorbikeFactory bike = new MotorbikeFactory();
                    vehicle = (Vehicle)bike.CreateVehicle(VehicleType.Motorbike);
                    break;
            }

            return vehicle;
        }        
    }

    public class CarFactory : VehicleFactory
    {
        public override IVehicle CreateVehicle(VehicleType type)
        {
            Vehicle vehicle = new Vehicle();

            vehicle.NumberOfWheels = 4;
            vehicle.VehicleType = type;
            vehicle.HornType = Horn.Honk;

            return vehicle;
        }
    }

    public class TruckFactory : VehicleFactory
    {
        public override IVehicle CreateVehicle(VehicleType type)
        {
            Vehicle vehicle = new Vehicle();

            vehicle.NumberOfWheels = 4;
            vehicle.VehicleType = type;
            vehicle.HornType = Horn.Braap;

            return vehicle;
        }
    }

    public class MotorbikeFactory : VehicleFactory
    {
        public override IVehicle CreateVehicle(VehicleType type)
        {
            Vehicle vehicle = new Vehicle();

            vehicle.NumberOfWheels = 2;
            vehicle.VehicleType = type;
            vehicle.HornType = Horn.Beep;

            return vehicle;
        }
    }
}