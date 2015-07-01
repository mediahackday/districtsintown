using FourSquare.SharpSquare.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FourSquareImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var sharpSquare = new SharpSquare("AZQX4TS2BF0SKW5HEFVJANL5C2AMXFBKC3JPBMDZ1NBMLNX", "NWEUHJI1AF3KRGDXKTPZGX22AML5D25FUSL4M2PIR5HELMXU");

            var venues = sharpSquare.SearchVenues(
                new Dictionary<string, string>
            {
                { "near", "Berlin, DE" },
                { "intent", "browse" },
                { "query", "cafe" }
            });

            foreach (var venue in venues)
            {
            }
        }
    }
}
