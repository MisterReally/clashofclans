﻿using ClashOfClans.Core;
using ClashOfClans.Models;
using ClashOfClans.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClashOfClans.Tests
{
    [TestClass]
    public class ClansTests : TestsBase
    {
        [TestMethod]
        public async Task SearchClans()
        {
            // Arrange
            var limit = 10;
            var query = new QueryClans
            {
                Name = "Phoenix",
                Limit = limit,
                WarFrequency = WarFrequency.Never
            };

            // Act
            var searchResult = await _coc.Clans.GetAsync(query);

            // Assert
            Assert.IsNotNull(searchResult);
            Assert.AreEqual(limit, searchResult.Items.Count());
        }

        [TestMethod]
        public async Task GetClanInformation()
        {
            // Arrange
            var clanTag = GetRandom(_clanList.Items).Tag;

            // Act
            var clan = await _coc.Clans.GetAsync(clanTag);
            Console.WriteLine(clan);

            // Assert
            Assert.IsNotNull(clan);
        }

        [TestMethod]
        public async Task ListClanMembers()
        {
            // Arrange
            var clanTag = GetRandom(_clanList.Items).Tag;

            // Act
            var clansMembers = await _coc.Clans.GetMembersAsync(clanTag);

            // Assert
            Assert.IsNotNull(clansMembers);
        }

        [TestMethod]
        public async Task RetrieveClansClanWarLog()
        {
            // Arrange
            var clan = GetRandom(_clanList.Items.Where(c => c.IsWarLogPublic == true).ToList());

            // Act
            if (clan != null)
            {
                Console.WriteLine(clan);

                var clanWarLog = await _coc.Clans.GetWarLogAsync(clan.Tag);
                Console.WriteLine(clanWarLog);

                // Assert
                Assert.IsNotNull(clanWarLog);
            }
            else
            {
                Assert.Fail("Test data contains no clan with public war log!");
            }
        }

        [TestMethod]
        public async Task RetrieveInformationAboutClansCurrentClanWar()
        {
            // Arrange
            var clan = GetRandom(_clanList.Items.Where(c => c.IsWarLogPublic == true).ToList());

            // Act
            if (clan != null)
            {
                Console.WriteLine(clan);

                var currentWar = await _coc.Clans.GetCurrentWarAsync(clan.Tag);
                Console.WriteLine(currentWar);

                // Assert
                Assert.IsNotNull(currentWar);
            }
        }

        [TestMethod]
        public async Task RetrieveInformationAboutClansCurrentClanWarLeagueGroupAndWar()
        {
            // Arrange

            // Act
            foreach (var clan in _clanList.Items)
            {
                Console.WriteLine(clan);

                try
                {
                    var leaguegroup = await _coc.Clans.GetCurrentWarLeagueGroupAsync(clan.Tag);
                    Console.WriteLine(leaguegroup);

                    // WarTag="#0" round not started
                    var round = GetRandom(leaguegroup.Rounds, r => !r.WarTags.Contains("#0"));
                    var warTag = GetRandom(round.WarTags);
                    var clanwarleagues = await _coc.Clans.GetClanWarLeaguesWarsAsync(warTag);

                    // Assert
                    Assert.IsNotNull(leaguegroup);
                    Assert.IsNotNull(clanwarleagues);
                    break;
                }
                catch (ClashOfClansException ex)
                {
                    Console.WriteLine(ex.Error);
                }
            }
        }
    }
}
