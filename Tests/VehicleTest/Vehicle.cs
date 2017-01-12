using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.VehicleTest
{
    public class Vehicle : IVehicle
    {
        public Point CurrentPosition { get; set; }

        public int NumberOfWheels { get; set; }

        public VehicleType VehicleType { get; set; }

        public Direction Route { get; set; }

        public Horn HornType { get; set; }

        public void MoveForward(int units)
        {
            switch (Route)
            {
                case (Direction.Forward):
                    CurrentPosition = CurrentPosition + new Size(0, units);
                    break;
                case (Direction.Left):
                    CurrentPosition = CurrentPosition + new Size(units * -1, 0);
                    break;
                case (Direction.Backward):
                    CurrentPosition = CurrentPosition + new Size(0, units * -1);
                    break;
                case (Direction.Right):
                    CurrentPosition = CurrentPosition + new Size(units, 0);
                    break;
            }
        }

        public string SoundHorn()
        {
            string horn = "";

            switch (HornType)
            {
                case (Horn.Honk):
                    horn = "honk";
                    break;
                case (Horn.Braap):
                    horn = "braap";
                    break;
                case (Horn.Beep):
                    horn = "beep";
                    break;
            }

            return horn;
        }

        public void TurnLeft()
        {
            switch(Route)
            {
                case (Direction.Forward):
                    Route = Direction.Left;
                    break;
                case (Direction.Left):
                    Route = Direction.Backward;
                    break;
                case (Direction.Backward):
                    Route = Direction.Right;
                    break;
                case (Direction.Right):
                    Route = Direction.Forward;
                    break;
            }
        }

        public void TurnRight()
        {
            switch (Route)
            {
                case (Direction.Forward):
                    Route = Direction.Right;
                    break;
                case (Direction.Right):
                    Route = Direction.Backward;
                    break;
                case (Direction.Backward):
                    Route = Direction.Left;
                    break;
                case (Direction.Left):
                    Route = Direction.Forward;
                    break;
            }
        }
    }
}
