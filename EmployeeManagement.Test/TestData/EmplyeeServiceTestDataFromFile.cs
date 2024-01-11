using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test.TestData
{
    public class EmplyeeServiceTestDataFromFile : TheoryData<int, bool>
    {
        public EmplyeeServiceTestDataFromFile()
        {
            var lineFromsFile = File.ReadAllLines("TestData/EmplyeeServiceTestDataFile.csv");

            foreach(var line in lineFromsFile)
            {
                var splitString = line.Split(',');

                if (int.TryParse(splitString[0], out var raise) &&
                    bool.TryParse(splitString[1], out var minmumRaiseGiven))
                {
                    Add(raise, minmumRaiseGiven);
                }
            }
        }
    }
}
