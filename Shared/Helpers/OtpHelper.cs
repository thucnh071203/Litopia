using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public static class OtpHelper
    {
        private static readonly Random _random = new Random();
        private const string AlphanumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string GenerateOtp()
        {
            StringBuilder otp = new StringBuilder(6);
            for (int i = 0; i < 6; i++)
            {
                otp.Append(AlphanumericChars[_random.Next(AlphanumericChars.Length)]);
            }
            return otp.ToString();
        }
    }
}
