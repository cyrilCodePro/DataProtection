using System;
using System.Security.Cryptography;

namespace DataProtection{

     public class EncryptData
    {
        RSACryptoServiceProvider rsa = null;
       public string publicPrivateKeyXML;
        public  string publicOnlyKeyXML;
        public void AssignNewKey()
        {
         
          
            rsa = new RSACryptoServiceProvider();

            //Pair of public and private key as XML string.
            //Do not share this to other party
            publicPrivateKeyXML = rsa.ToTestString(true);
            //Private key in xml file, this string should be share to other parties
            publicOnlyKeyXML = rsa.ToTestString(false);
            
        }
    }
   
    public static class Extensions
    {
           public static string ToTestString(this RSA rsa, bool includePrivateParameters)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);

            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                  parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null,
                  parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null,
                  parameters.P != null ? Convert.ToBase64String(parameters.P) : null,
                  parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null,
                  parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null,
                  parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null,
                  parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null,
                  parameters.D != null ? Convert.ToBase64String(parameters.D) : null);
        }
    }
}
    