﻿using ClashOfClans.Core;
using ClashOfClans.Models;
using System;
using System.Threading.Tasks;

namespace ClashOfClans.Api
{
    internal class Players : ClashOfClansBase, IPlayers
    {
        public Players(string token, IThrottleRequests throttleRequests) :
            base(token, throttleRequests)
        {
        }

        // GET /players/{playerTag}
        public async Task<PlayerDetail> GetAsync(string playerTag)
        {
            if (string.IsNullOrWhiteSpace(playerTag))
            {
                throw new ArgumentNullException(nameof(playerTag));
            }

            var uri = $"players/{playerTag}";

            return await RequestAsync<PlayerDetail>(uri);
        }
    }
}
