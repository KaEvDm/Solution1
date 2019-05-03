﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Table
{
    public class Game
    {
        private readonly int InitialBank;
        public Banker banker;
        public List<AbsPlayer> players;
        public int id;
        public Round CurrentRound;
        
        

        private bool isEnd;
        private bool BankerBankrupt => banker.Money == 0;
        private bool AllPlayersBankrupt => players.All(p => p.Money <= 0);
        private bool BankerBankTooBig => banker.Money == InitialBank * 3;


        public Game(AbsPlayer host)
        {
            banker = new Banker();
            players = new List<AbsPlayer>();
            players.Add(host);
            id = new Random().Next();
            banker.Money = host.Money * 2;
            InitialBank = host.Money;
        }
        
        public void Start()
        {
            while(!isEnd || !BankerBankrupt || !AllPlayersBankrupt || !BankerBankTooBig)
            {
                CurrentRound = new Round(players, banker);
                CurrentRound.Start();
            }
        }

        public void End()
        {
            isEnd = true;
        }

        private void ChooseBanker()
        {
            var index = new Random().Next(players.Count);
            banker = (Banker)players[index];
            players.RemoveAt(index);
        }
    }
}
