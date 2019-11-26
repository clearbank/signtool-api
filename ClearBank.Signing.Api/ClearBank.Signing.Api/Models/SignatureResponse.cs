namespace ClearBank.Signing.Api.Models
{
    /// <summary>
    /// Represents a response to a web hook
    /// </summary>
    public class SignatureResponse
    {
        /// <summary>
        /// The signature
        /// </summary>
        public string Signature { get; set; }
    }
}
