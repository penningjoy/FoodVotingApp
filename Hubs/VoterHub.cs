using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

namespace FoodVotingApp.Hubs
{
    /* A hub is a class that serves as a high-level pipeline that handles client-server communication. *
     * The VoterHub inherits from the SignalR Hub class. The Hub class manages the messages, groups and*
     * connections. */
    public class VoterHub: Hub
    {
        private List<string> items = new List<string>();
        private IDistributedCache _distributedCache;
        private string _cacheKey1;
        private string _cacheKey2;
        private string _cacheKey3;

        public VoterHub(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            items.Add("Biriyani");
            items.Add("Polao");
            items.Add("Goat Curry Rice");
            _cacheKey1 = "Biriyani";
            _cacheKey2 = "Polao";
            _cacheKey3 = "Curry";
            
        }

        /* When a new connection is established with the Hub. The onConnectedAsync method is used *
         * while the connection is starting. Here the cache is initialized everytime there is a   *
         * new connection established.                                                            */
        public override Task OnConnectedAsync()
        {
            _distributedCache.SetString(_cacheKey1, "0");
            _distributedCache.SetString(_cacheKey2, "0");
            _distributedCache.SetString(_cacheKey3, "0");

            return base.OnConnectedAsync();
        }

        /* The vote method can be called by a connected client to send a reply to ALL clients. *
         * The method is called from the Javascript client code. */

        public async Task Vote(string item, int increment)
        {
            if (items.Contains<string>(item))
            {
                await Clients.All.SendAsync("VoteReceived", item, CacheUpdate(item,increment));
            }
        }

        /* Method CacheUpdate updates the cache with the increased vote count. *
         * Returns the new vote count.                                          */
        public int CacheUpdate(string item, int increment)
        {
            int vote;
            item = item.Trim();

            if (item != String.Empty && increment > 0)
            {
                switch (item)
                {
                    case "Biriyani":
                        vote = int.Parse(_distributedCache.GetString(_cacheKey1));
                        vote = vote + increment;
                        _distributedCache.SetString(_cacheKey1,vote.ToString());
                        return vote;

                    case "Polao":
                        vote = int.Parse(_distributedCache.GetString(_cacheKey2));
                        vote = vote + increment;
                        _distributedCache.SetStringAsync(_cacheKey2, vote.ToString());
                        return vote;

                    case "Goat Curry Rice":
                        vote = int.Parse(_distributedCache.GetString(_cacheKey3));
                        vote = vote + increment;
                        _distributedCache.SetString(_cacheKey3, vote.ToString());
                        return vote;

                    default:
                        return 0;
                }
            }
            return 0;
        }
    }
}
