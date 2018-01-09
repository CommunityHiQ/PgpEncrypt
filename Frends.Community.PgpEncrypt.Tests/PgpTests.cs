﻿using NUnit.Framework;
using System.IO;
using System.Text.RegularExpressions;



namespace FRENDS.Community.PgpEncrypt.Tests
{
    [TestFixture]
    class PgpTests
    {
        // following keys should not be used on anything except testing as both private key and password are on public GitHub repository 
        private readonly string public_key_path = @"Frends.Community.PgpEncrypt.Tests\TestData\pub.asc";
        private readonly string message_path = @"Frends.Community.PgpEncrypt.Tests\TestData\original_message.txt";
        private readonly string encrypted_message = @"Frends.Community.PgpEncrypt.Tests\TestData\encrypted_message.pgp";


        [TearDown]
        public void DeleteTmpFile()
        {
            File.Delete(encrypted_message);
        }

        [Test]
        public void EncryptFile()
        {
            Input input = new Input
            {
                InputFile = message_path,
                OutputFile = encrypted_message,
                PublicKeyFile = public_key_path,
                UseIntegrityCheck = true,
                UseArmor = true
            };

            Result result_object = FRENDSTaskEncrypt.PgpEncrypt(input);

            string result = File.ReadAllText(result_object.FilePath);

            string expectedResult = "-----BEGINPGPMESSAGE-----Version:BCPGC#v1.8.1.0hIwDzoB5W4N7pN4B";
             // Rest of the file is random.

            Assert.That(Regex.Replace(result, @"[\s+]", ""), Does.StartWith(Regex.Replace(expectedResult, @"[\s+]", "")));


        }
    }
}