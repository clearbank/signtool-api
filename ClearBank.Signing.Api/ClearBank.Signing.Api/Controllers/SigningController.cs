using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using ClearBank.Signing.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace ClearBank.Signing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SigningController : ControllerBase
    {
        private readonly DirectoryInfo ApplicationDirectoryInfo = new DirectoryInfo(
            Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)
            );

        /// <summary>
        /// Signs body with specified private key
        /// </summary>
        /// <param name="content">The content to sign</param>
        [HttpPost]
        public ActionResult<SignatureResponse> Post([FromBody] string content)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                    return BadRequest($"{nameof(content)} not supplied");

                // Get private key pem
                var privateKeyPem = System.IO.File.ReadAllText(
                    Path.Combine(ApplicationDirectoryInfo.FullName, "Data", "Testing.key")
                    );

                var data = Encoding.UTF8.GetBytes(content);

                byte[] hash;
                using (var provider = new SHA256Managed())
                {
                    hash = provider.ComputeHash(data);
                }

                byte[] signedHash;
                using (var privateKey = GetCryptoProvider(privateKeyPem))
                {
                    signedHash = privateKey.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }

                var encodedHash = Convert.ToBase64String(signedHash);

                return new SignatureResponse
                {
                    Signature = encodedHash
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        private static RSACryptoServiceProvider GetCryptoProvider(string pem)
        {
            var pr = new PemReader(new StringReader(pem));
            var rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)pr.ReadObject());

            var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(rsaParams);
            return csp;
        }
    }
}
