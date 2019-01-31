using System;
using System.Security.Cryptography;
using System.Xml;

namespace DataProtection{

     public class EncryptData
    {
        RSACryptoServiceProvider rsa = null;
       public string publicPrivateKeyXML;
        public  string publicOnlyKeyXML;
        public void AssignNewKey()
        {
         
          
            rsa = new RSACryptoServiceProvider(15360);

            //Pair of public and private key as XML string.
            //Do not share this to other party
            publicPrivateKeyXML = rsa.ToTestXmlString(true);
            publicPrivateKeyXML=RSAKeys.ExportPrivateKey(rsa);
            //Private key in xml file, this string should be share to other parties
            publicOnlyKeyXML =RSAKeys.ExportPublicKey(rsa);
            
        }
    }
   
    public static class Extensions
    {
           public static string ToTestXmlString(this RSA rsa, bool includePrivateParameters)
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
            public static void FromXmlTestString(this RSACryptoServiceProvider rsa, string xmlString)
        {
            RSAParameters parameters = new RSAParameters();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus":     parameters.Modulus =    Convert.FromBase64String(node.InnerText); break;
                        case "Exponent":    parameters.Exponent =   Convert.FromBase64String(node.InnerText); break;
                        case "P":           parameters.P =          Convert.FromBase64String(node.InnerText); break;
                        case "Q":           parameters.Q =          Convert.FromBase64String(node.InnerText); break;
                        case "DP":          parameters.DP =         Convert.FromBase64String(node.InnerText); break;
                        case "DQ":          parameters.DQ =         Convert.FromBase64String(node.InnerText); break;
                        case "InverseQ":    parameters.InverseQ =   Convert.FromBase64String(node.InnerText); break;
                        case "D":           parameters.D =          Convert.FromBase64String(node.InnerText); break;
                    }
                }
            } else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }

    }
}
    