﻿using System;
using System.Threading.Tasks;

namespace ClashOfClans.App.Examples
{
    public class PlayersExamples
    {
        private readonly string token;
        private readonly string playerTag;

        public PlayersExamples(string token, string playerTag)
        {
            this.token = token;
            this.playerTag = playerTag;
        }

        /// <summary>
        /// Get information about a single player by player tag.
        /// </summary>
        public async Task GetPlayerInformation()
        {
            var coc = new ClashOfClansClient(token);
            var player = await coc.Players.GetPlayerAsync(playerTag);
            Console.WriteLine($"'{player.Name}' has {player.Trophies} \uD83C\uDFC6 and {player.WarStars} war stars");

            if (player.Clan != null)
            {
                var d = player.Donations;
                var dr = player.DonationsReceived;
                Console.WriteLine($"'{player.Name}' is a member of '{player.Clan.Name}' and has a donation ratio {d}/{dr}={(dr != 0 ? (d / (float)dr) : 0):0.00}");
            }
        }

        /// <summary>
        /// Verify player API token that can be found from the game settings
        /// </summary>
        public async Task VerifyPlayerApiToken()
        {
            var playerApiToken = "123456789";
            var coc = new ClashOfClansClient(token);
            var result = await coc.Players.VerifyTokenAsync(playerTag, playerApiToken);

            Console.WriteLine($"Player '{result.Tag}' API token '{result.Token}' status '{result.Status}'");
        }
    }
}
