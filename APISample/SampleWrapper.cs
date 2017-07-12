using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISample
{
    //TODO:  Replace this with a real API wrapper class.
    //This is just a sample API wrapper class.  An API wrapper can be custom built, or downloaded from another source.
    public static class SampleWrapper
    {
        public static void DoSomething(String example1, string example2)
        {
            //do something         
            System.Console.WriteLine($"The SampleWrapper class method DoSomething was called with the string '{example1}' and string '{example2}'.");
        }
    }
}
