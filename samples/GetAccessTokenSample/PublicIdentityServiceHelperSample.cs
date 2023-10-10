namespace Insights.Sdk.Samples.GetAccessTokenSample
{
    #region
    using IdentityModel.Client;
    using IdentityModel;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    #endregion
    public class PublicIdentityServiceHelperSample
    {
        /// <summary>
        /// Get Access Token By Local certificate file
        /// </summary>
        /// <param name="url">identity service url</param>
        /// <param name="clientId">identity service clientId</param>
        /// <param name="certificatePath">certificate file Path</param>
        public async Task<string> GetAccessTokenAsyncByCertificatePath(string Url, string clientId, string certificatePath)
        {
            var token = string.Empty;

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(Url);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return token;
            }
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientAssertion = new ClientAssertion()
                {
                    Type = OidcConstants.ClientAssertionTypes.JwtBearer,
                    Value = CreateClientAuthJwtByPath(disco, clientId, certificatePath)
                },
                Scope = "insights.graph.readwrite.all",
            });
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return token;
            }
            token = tokenResponse.AccessToken;

            return token;
        }

        /// <summary>
        /// Get Access Token By CerPrint
        /// </summary>
        /// <param name="url">identity service url</param>
        /// <param name="clientId">identity service clientId</param>
        /// <param name="cerPrint">certificate Thumbprint</param>
        public async Task<string> GetAccessTokenAsyncByCerPrint(string Url, string clientId, string cerPrint)
        {
            var token = string.Empty;

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(Url);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return token;
            }
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientAssertion = new ClientAssertion()
                {
                    Type = OidcConstants.ClientAssertionTypes.JwtBearer,
                    Value = CreateClientAuthJwtByCerPrint(disco, clientId, cerPrint)
                },
                Scope = "insights.graph.readwrite.all",
            });
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return token;
            }
            token = tokenResponse.AccessToken;

            return token;
        }

        /// <summary>
        /// Create Client Auth Jwt
        /// </summary>
        /// <param name="response">DiscoveryDocumentResponse</param>
        /// <param name="clientId">identity service clientId</param>
        /// <param name="cerPrint">certificate Thumbprint</param>
        /// <returns></returns>
        private static string CreateClientAuthJwtByCerPrint(DiscoveryDocumentResponse response, string clientId, string cerPrint)
        {
            // set exp to 5 minutes
            var tokenHandler = new JwtSecurityTokenHandler { TokenLifetimeInMinutes = 5 };

            var securityToken = tokenHandler.CreateJwtSecurityToken(
                // iss must be the client_id of our application
                issuer: clientId,
                // aud must be the identity provider (token endpoint)
                audience: response.TokenEndpoint,
                // sub must be the client_id of our application
                subject: new ClaimsIdentity(
                  new List<Claim> { new Claim("sub", clientId),
                  new Claim("jti", Guid.NewGuid().ToString())}),
                // sign with the private key (using RS256 for IdentityServer)
                signingCredentials: new SigningCredentials(
                  new X509SecurityKey(LoadCertificate(cerPrint)), "RS256")
            );

            return tokenHandler.WriteToken(securityToken);
        }

        /// <summary>
        /// Create Client Auth Jwt By Local certificate file
        /// </summary>
        /// <param name="response">DiscoveryDocumentResponse</param>
        /// <param name="clientId">identity service clientId</param>
        /// <param name="certificatePath">certificate file Path</param>
        /// <returns></returns>
        private static string CreateClientAuthJwtByPath(DiscoveryDocumentResponse response, string clientId, string certificatePath)
        {
            // set exp to 5 minutes
            var tokenHandler = new JwtSecurityTokenHandler { TokenLifetimeInMinutes = 5 };

            var securityToken = tokenHandler.CreateJwtSecurityToken(
                // iss must be the client_id of our application
                issuer: clientId,
                // aud must be the identity provider (token endpoint)
                audience: response.TokenEndpoint,
                // sub must be the client_id of our application
                subject: new ClaimsIdentity(
                  new List<Claim> { new Claim("sub", clientId),
                  new Claim("jti", Guid.NewGuid().ToString())}),
                // sign with the private key (using RS256 for IdentityServer)
                signingCredentials: new SigningCredentials(
                  new X509SecurityKey(GetX509Certificate2(certificatePath)), "RS256")
            );

            return tokenHandler.WriteToken(securityToken);
        }
        /// <summary>
        /// Get Certificate
        /// </summary>
        /// <param name="path">certificate file Path</param>
        /// <returns></returns>
        private static X509Certificate2 GetX509Certificate2(string path)
        {
            var cer = new X509Certificate2(path, "a!v@e#p$o%i^n&t");
            return cer;
        }

        /// <summary>
        /// Load Certificate
        /// </summary>
        /// <param name="cerPrint"certificate Thumbprint></param>
        private static X509Certificate2 LoadCertificate(string cerPrint)
        {
            //Gao certificate
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            var vCloudCertificate = store.Certificates.Find(
                    X509FindType.FindByThumbprint,
                    cerPrint,
                    false)[0];
            return vCloudCertificate;
        }
    }
}
