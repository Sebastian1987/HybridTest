using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;

namespace HybridTest.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        public static string ProcessPassword()
        {
            const int totalRolls = 25000;
            int[] results = new int[6];

            var passhash = "";            
            for (int x = 0; x < totalRolls; x++)
            {
                byte roll = RollDice((byte)results.Length);
                results[roll - 1]++;
            }
            for (int i = 0; i < results.Length; ++i)
            {
                passhash="{0}: {1} ({2:p1})" + i + 1 + results[i] + (double)results[i] / (double)totalRolls;
            }
            rngCsp.Dispose();

            return passhash;
        }

               
        public static byte RollDice(byte numberSides)
        {
            if (numberSides <= 0)
                throw new ArgumentOutOfRangeException("numberSides");

            byte[] randomNumber = new byte[1];
            do
            {             
                rngCsp.GetBytes(randomNumber);
            }
            while (!IsFairRoll(randomNumber[0], numberSides));
         
            return (byte)((randomNumber[0] % numberSides) + 1);
        }

        private static bool IsFairRoll(byte roll, byte numSides)
        {            
            int fullSetsOfValues = Byte.MaxValue / numSides;
           
            return roll < numSides * fullSetsOfValues;
        }

    }
}