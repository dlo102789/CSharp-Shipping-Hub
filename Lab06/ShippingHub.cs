using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab06
{
    class ShippingHub
    {
        
        private List<Package> Packages = new List<Package>();
        public List<string> States = new List<string>();    // A public List of strings that can be accessed by ComboBox.
        private int packageID = 1;

        public ShippingHub()
        {
            States.Add("AL");
            States.Add("FL");
            States.Add("GA");
            States.Add("KY");
            States.Add("MS");
            States.Add("NC");
            States.Add("SC");
            States.Add("TN");
            States.Add("WV");
            States.Add("VA");
        }

        /// <summary>
        /// Adds a package to the List of packages and sorts by Package ID.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        public void AddPackage(string address, string city, int state, string zip)
        {
            Packages.Add(new Package(address, city, state, zip, packageID.ToString()));
            Packages.Sort((x, y) => x.PackageID.CompareTo(y.PackageID)); //Sorts list of packages by Package ID#
            packageID++;
        }

        public void RemovePackage(string packageID)
        {
            for (int i = Packages.Count - 1; i >= 0; i--)
            {
                if (Packages[i].PackageID == packageID)
                {
                    Packages.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Returns a sub-list of Packages belonging to a particular state.
        /// </summary>
        /// <param name="stateIndex"></param>
        /// <returns></returns>
        public List<Package> PackagesByState(int stateIndex)
        {
            List<Package> tempList = null;
            foreach(Package package in Packages)
            {
                if (States[package.State] == States[stateIndex])
                {
                    if (tempList == null)
                    {
                        tempList = new List<Package>();
                    }
                    tempList.Add(package);
                }
            }

            return tempList;
        }

        public void EditPackage(string packageID, string address, string city, int state, string zip)
        {
            foreach(Package package in Packages)
            {
                if (package.PackageID == packageID)
                {
                    package.EditAddress(address);
                    package.EditCity(city);
                    package.EditState(state);
                    package.EditZip(zip);
                }
            }
        }
    }
}
