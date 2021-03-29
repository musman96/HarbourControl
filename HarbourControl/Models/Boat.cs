    using System;
using System.Collections.Generic;
using System.Text;

namespace HarbourControl.Models
{
    public abstract class Boat
    {
        private string boatType;
        private int speed;
        private bool docked;

        public bool Docked
        {
            get { return docked; }
            set { docked = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public string BoatType
        {
            get { return boatType; }
            set { boatType = value; }
        }

        public Boat()
        {

        }

        public Boat(string type, int speed)
        {
            this.boatType = type;
            this.speed = speed;
        }

    }
}
