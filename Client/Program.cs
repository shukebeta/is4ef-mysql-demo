// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.Client;using Newtonsoft.Json.Linq;using System;using System.Net.Http;using System.Threading.Tasks;namespace ConsoleClient{    public class Program    {        private static async Task Main()        {
            // discover endpoints from metadata
            var client = new HttpClient();            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");            if (disco.IsError)            {                Console.WriteLine(disco.Error);                return;            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest            {                // client_credential_flow                Address = disco.TokenEndpoint,                ClientId = "client",                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",                Scope = "api1"            });

            if (tokenResponse.IsError)            {                Console.WriteLine(tokenResponse.Error);                return;            }            Console.WriteLine(tokenResponse.Json);            Console.WriteLine("\n\n");

            // call api
            var apiClient = new HttpClient();            apiClient.SetBearerToken(tokenResponse.AccessToken);            var response = await apiClient.GetAsync("http://localhost:5001/weatherforecast");            if (!response.IsSuccessStatusCode)            {                Console.WriteLine(response.StatusCode);            }            else            {                var content = await response.Content.ReadAsStringAsync();                Console.WriteLine(JArray.Parse(content));            }

            // request token
            var tkResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest//await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest            {                Address = disco.TokenEndpoint,                UserName = "bob",                Password = "Pass123$",                ClientId = "resource_owner_flow",                ClientSecret = "resource_owner_flow_secret",                Scope = "api1 offline_access"            });

            if (tkResponse.IsError)            {                Console.WriteLine(tkResponse.Error);                return;            }            Console.WriteLine(tkResponse.Json);            Console.WriteLine("\n\n");

            // call api            apiClient.SetBearerToken(tokenResponse.AccessToken);            var respo = await apiClient.GetAsync("http://localhost:5001/weatherforecast");            if (!response.IsSuccessStatusCode)            {                Console.WriteLine(respo.StatusCode);            }            else            {                var content = await respo.Content.ReadAsStringAsync();                Console.WriteLine(JArray.Parse(content));            }        }    }}