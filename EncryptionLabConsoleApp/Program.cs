using System;
using System.Buffers.Text;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;


namespace EncryptionLabConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {


            //const int Number = 15;
            //byte[] RandomNumberArray = new byte[Number];
            //RNGCryptoServiceProvider rngCSP = new RNGCryptoServiceProvider();
            //rngCSP.GetBytes(RandomNumberArray);

            //for (int index = 0; index < RandomNumberArray.Length; index++)
            //{
            //    Byte roll = (byte)(RandomNumberArray[index] % 1000);
            //    Console.WriteLine(roll);
            //}



            ////-----------------------------------------------------------------------------------


            //const int Number = 15;
            //byte[] randomBytes = new byte[Number * sizeof(int)]; // Change the size based on your needs

            //for (int index = 0; index < Number; index++)
            //{
            //    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            //    {
            //        rng.GetBytes(randomBytes);
            //    }

            //    int randomNumber = BitConverter.ToInt32(randomBytes, index * sizeof(int));
            //    Console.WriteLine(randomNumber);
            //}




            ////-----------------------------------------------------------------------------------

            //const int Number = 1000000;
            //byte[] randomBytes = new byte[Number]; // Change the size based on your needs

            //Stopwatch stopwatch = Stopwatch.StartNew();


            //using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(randomBytes);
            //}
            //int randomNumber = BitConverter.ToInt32(randomBytes, sizeof(int));

            //Console.WriteLine(randomNumber);
            //stopwatch.Stop();
            //Console.WriteLine("Creating " + Number + " Random Numbers took  " + stopwatch.ElapsedMilliseconds + " Milliseconds");





            //-----------------------------------------------------------------------------------

            const int Number = 10;
            byte[] RandomNumberArray = new byte[Number];

            RNGCryptoServiceProvider rngCSP = new RNGCryptoServiceProvider();
            rngCSP.GetBytes(RandomNumberArray);

            String Base64EncodedString;
            Base64EncodedString = Convert.ToBase64String(RandomNumberArray);

            // Converting the Base64-encoded string back to a byte array
            byte[] DecodedRandomNumberArray = Convert.FromBase64String(Base64EncodedString);

            // Comparing the original byte array with the decoded byte array
            bool arraysMatch = Enumerable.SequenceEqual(RandomNumberArray, DecodedRandomNumberArray);

            // Printing the results
            Console.WriteLine("Base64Encoded String:\t\t" + Base64EncodedString);
            Console.WriteLine("\n");
            Console.Write("Decoded RandomNumberArray byte:\t ");
            foreach (byte value in DecodedRandomNumberArray)
            {
                Console.Write(value + " ");
            }
            Console.WriteLine("\n\nArrays Match:\t\t\t " + arraysMatch);
            Console.WriteLine("\n\n\n");



























            //const int Number = 1000000;
            //byte[] RandomNumberArray = new byte[Number];

            //Stopwatch stopwatch = Stopwatch.StartNew();


            //Random random = new Random(Number); 
            //for (int i = 0; i < 10; i++)
            //{
            //    int randomNumber = random.Next();
            //    Console.WriteLine(randomNumber);
            //}


            //stopwatch.Stop();
            ////Console.WriteLine("Creating " + Number + " Random Numbers took  " + stopwatch.ElapsedMilliseconds + " Milliseconds");


            //const int Number = 10;
            //byte[] RandomNumberArray = new byte[Number];
            //List<byte[]> decodedArrays = new List<byte[]>();

            //RNGCryptoServiceProvider rngCSP = new RNGCryptoServiceProvider();


            //String Base64EncodedString;

            //for (int index = 0; index < RandomNumberArray.Length; index++)
            //{
            //    rngCSP.GetBytes(RandomNumberArray); 
            //    Base64EncodedString = Convert.ToBase64String(RandomNumberArray);
            //    Console.WriteLine("Base64Encoded String: " + Base64EncodedString);
            //    byte[] decodedArray = Convert.FromBase64String(Base64EncodedString);
            //    decodedArrays.Add(decodedArray);
            //}



        }
    }
}
