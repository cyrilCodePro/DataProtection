using System;

namespace DataProtection
{
    class Program
    {
        static void Main(string[] args)
        {

           var data=new  EncryptData();
           data.AssignNewKey();
           Console.WriteLine($"Public key: \n {data.publicOnlyKeyXML}");
           Console.WriteLine($"Private key: \n {data.publicPrivateKeyXML}");
        }
    }
}
