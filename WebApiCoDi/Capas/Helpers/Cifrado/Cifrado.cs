using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace WebApiCoDi.Helpers.Cifrado
{
    public class Cifrado
    {           
        public async Task<string> Sha256encrypt(string str) 
        {
            var t = await Task.Run(() =>
            {
                try
                {
                UTF8Encoding encoder = new UTF8Encoding();
                SHA256Managed sha256hasher = new SHA256Managed();
                byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(str));
                return Convert.ToBase64String(hashedDataBytes);
                }
                catch (Exception e)
                {
                    throw e;
                   // return "Error";
                   
                }
            });
            return t;
        }

    }
}
