using System;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;


namespace NipicTask
{
    /// <summary>
    /// Implements the validator for X509 certificates.
    /// </summary>
    public class CustomX509Validator : X509CertificateValidator
    {
        /// <summary>
        /// Validates a certificate.
        /// </summary>
        /// <param name="certificate">The certificate the validate.</param>
        public override void Validate(X509Certificate2 certificate)
        {
            // validate argument
            if (certificate == null)
                throw new ArgumentNullException("X509认证证书为空！");

            // check if the name of the certifcate matches
            if (certificate.SubjectName.Name != ConfigurationManager.AppSettings["CertName"])
                throw new SecurityTokenValidationException("Certificated was not issued by thrusted issuer");
        }
    }
}