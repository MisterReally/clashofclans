﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClashOfClans.Core.Net
{
    /// <summary>
    /// Represents a Clash of Clans game data REST API endpoint
    /// </summary>
    internal class ApiEndpoint
    {
        private int _index = 0;
        private readonly object _indexLock = new object();
        private readonly IList<HttpClient> _clients = new List<HttpClient>();

        private HttpClient Client
        {
            get
            {
                lock (_indexLock)
                {
                    if (++_index == _clients.Count)
                        _index = 0;

                    return _clients[_index];
                }
            }
        }

        public ApiEndpoint(IReadOnlyList<string> tokens)
        {
            var address = new Uri("https://api.clashofclans.com/v1/");

            foreach (var token in tokens)
                _clients.Add(CreateHttpClient(token, address));

            // Sets the number of milliseconds after which an active ServicePoint connection is closed
            var sp = ServicePointManager.FindServicePoint(address);
            sp.ConnectionLeaseTimeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
        }

        /// <summary>
        /// HttpClient is intended to be instantiated once and re-used throughout the life of an application
        /// </summary>
        private HttpClient CreateHttpClient(string token, Uri address)
        {
            var client = new HttpClient
            {
                BaseAddress = address
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        public Task<HttpResponseMessage> GetMessageAsync(string requestUri) => Client.GetAsync(requestUri);

        public Task<HttpResponseMessage> PostMessageAsync(string requestUri, string content) =>
            Client.PostAsync(requestUri, new StringContent(content));
    }
}
