using System;

namespace Persistance
{
    public class Manufactory
    {
        public int ManufactoryId { get; set; }
        public string ManufactoryName { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Manufactory)
            {
                Manufactory manufactory = (Manufactory)obj;
                return
                // this.ManufactoryId.Equals(manufactory.ManufactoryId) &&
                this.ManufactoryName.Equals(manufactory.ManufactoryName)
                // this.Website.Equals(manufactory.Website) &&
                // this.Address.Equals(manufactory.Address)
                ;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.ManufactoryId.GetHashCode();
        }
    }
}