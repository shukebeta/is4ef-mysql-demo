﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.Client;
            // discover endpoints from metadata
            var client = new HttpClient();

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest

            if (tokenResponse.IsError)

            // call api
            var apiClient = new HttpClient();

            // request token
            var tkResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest//await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest

            if (tkResponse.IsError)

            // call api