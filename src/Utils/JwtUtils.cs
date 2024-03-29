﻿using Laserfiche.Api.Client.OAuth;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Laserfiche.Api.Client.Utils
{
    internal static class JwtUtils
    {
        internal static string CreateClientCredentialsAuthorizationJwt(string servicePrincipalKey, AccessKey accessKey, string audience = "laserfiche.com", DateTime? validTo = null)
        {
            var claims = new[]
                {
                    new Claim("client_id", accessKey.ClientId),
                    new Claim("client_secret", servicePrincipalKey),
                };
            return CreateSignedJwt(claims, accessKey.Jwk, audience, validTo);
        }

        private static SigningCredentials GetSigningCredentials(JsonWebKey key)
        {
            var ecdsa = ECDsa.Create(new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,
                Q = new ECPoint { X = Base64UrlEncoder.DecodeBytes(key.X), Y = Base64UrlEncoder.DecodeBytes(key.Y) },
                D = Base64UrlEncoder.DecodeBytes(key.D)
            });
            var ecdsaSecurityKey = new ECDsaSecurityKey(ecdsa) { KeyId = key.Kid };
            return new SigningCredentials(ecdsaSecurityKey, SecurityAlgorithms.EcdsaSha256);
        }

        private static string CreateSignedJwt(IEnumerable<Claim> claims, JsonWebKey key, string audience = "laserfiche.com",
            DateTime? validTo = null)
        {
            if(validTo == null)
                validTo = DateTime.UtcNow.AddMinutes(30);
            var signingCredentials = GetSigningCredentials(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = audience,
                Expires = validTo,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials
            };
            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
        }

        internal static string CreateBasicAuth(string clientId, string clientSecret)
        {
            if (clientSecret != null)
            {
                var basicCredentials = clientId + ':' + clientSecret;
                var base64EncodedClientSecret = System.Text.Encoding.UTF8.GetBytes(basicCredentials);
                return $"Basic {System.Convert.ToBase64String(base64EncodedClientSecret)}";
            }
            return null;
        }
    }
}
