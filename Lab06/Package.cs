using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab06
{
    class Package
    {
        private string _packageID;
        private string _address;
        private string _city;
        private int _state;
        private string _zip;
        public DateTime arrivedAt;

        public string PackageID
        {
            get => _packageID;

            private set => _packageID = value;
        }

        public int State
        {
            get => _state;

            private set => _state = value;
        }

        public string Address
        {
            get => _address;

            private set => _address = value;
        }

        public string City
        {
            get => _city;

            private set => _city = value;
        }

        public string Zip
        {
            get => _zip;

            private set => _zip = value;
        }

        public Package(string address, string city, int state, string zip, string packageID)
        {
            this.arrivedAt = DateTime.Now;
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            this.PackageID = packageID;
        }

        public void EditEntries(string address, string city, int state, string zip)
        {
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Zip = zip;
        }

        public void EditAddress(string address) => this.Address = address;
        public void EditCity(string city) => this.City = city;
        public void EditState(int state) => this.State = state;
        public void EditZip(string zip) => this.Zip = zip;
    }
}
