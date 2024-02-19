using System;
using System.Diagnostics.SymbolStore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace EncryptionHashConsoleASPNetaFramework
{
    public  class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Enter ur password:");
            string password = Console.ReadLine();


            //generate a 128-bit salt-hash  using cryptographically strong random sequence of nonzero values
            byte[] salt  = new byte[128/8];
            using (var rngcsp = new RNGCryptoServiceProvider())
            {
                rngcsp.GetBytes(salt);
            }

            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
            string storedsalt = Convert.ToBase64String(salt);

            //derive a 256 bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            Console.WriteLine($"Hashed: {hashed}");
            Console.ReadLine();
            
            Program.deHarsh(storedsalt, hashed);




        }

        static void deHarsh(string storedSaltString, string storedHashedPassword)
        {
            // Retrieve the stored salt and hashed password from your database
            // For demonstration purposes, let's assume you have them as strings
            //string storedSaltString = "yourStoredSaltString"; // Replace with the actual stored salt
            //string storedHashedPassword = "yourStoredHashedPassword"; // Replace with the actual stored hashed password

            // Convert the stored salt back to a byte array
            byte[] storedSalt = Convert.FromBase64String(storedSaltString);

            // Get the user-entered password
            Console.WriteLine("Enter your password for verification:");
            string userEnteredPassword = Console.ReadLine();

            // Derive the hashed password using the same salt and parameters
            string hashedPasswordToCheck = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userEnteredPassword,
                salt: storedSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            // Compare the derived hashed password with the stored hashed password
            if (hashedPasswordToCheck == storedHashedPassword)
            {
                Console.WriteLine("Password is correct!");
            }
            else
            {
                Console.WriteLine("Password is incorrect!");
            }
            Console.ReadLine();
        }

        

    }
}
