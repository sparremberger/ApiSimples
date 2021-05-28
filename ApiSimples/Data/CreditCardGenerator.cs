using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSimples.Data
{
    public class CreditCardGenerator
    {
        public static string[] PREFIX_LIST = new[] { "4539", "4556", "4916", "4532" };
        private static string CreateFakeCreditCardNumber(string prefix, int length)
        {
            string ccnumber = prefix;
            while (ccnumber.Length < (length - 1))
            {
                double rnd = (new Random().NextDouble() * 1.0f - 0f);
                ccnumber += Math.Floor(rnd * 10);
            }

            // reverse number and convert to int
            var reversedCCnumberstring = ccnumber.ToCharArray().Reverse();
            var reversedCCnumberList = reversedCCnumberstring.Select(c => Convert.ToInt32(c.ToString()));
            // calculate sum
            int sum = 0;
            int pos = 0;
            int[] reversedCCnumber = reversedCCnumberList.ToArray();
            while (pos < length - 1)
            {
                int odd = reversedCCnumber[pos] * 2;
                if (odd > 9)
                    odd -= 9;

                sum += odd;

                if (pos != (length - 2))
                    sum += reversedCCnumber[pos + 1];

                pos += 2;
            }
            // calculate check digit
            int checkdigit =
                Convert.ToInt32((Math.Floor((decimal)sum / 10) + 1) * 10 - sum) % 10;
            ccnumber += checkdigit;
            return ccnumber;
        }
        public static IEnumerable<string> GetCreditCardNumbers(string[] prefixList, int length, int howMany)
        {
            var result = new Stack<string>();
            for (int i = 0; i < howMany; i++)
            {
                int randomPrefix = new Random().Next(0, prefixList.Length - 1);
                if (randomPrefix > 1)
                {
                    randomPrefix--;
                }
                string ccnumber = prefixList[randomPrefix];
                result.Push(CreateFakeCreditCardNumber(ccnumber, length));
            }
            return result;
        }
        // Gera cartões visa
        public static IEnumerable<string> GenerateVisaCardNumbers(int howMany)
        {
            return GetCreditCardNumbers(PREFIX_LIST, 16, howMany);
        }
        // Gera apenas 1 visa
        public static string GenerateMasterCardNumber()
        {
            return GetCreditCardNumbers(PREFIX_LIST, 16, 1).First();
        }
    }
}
