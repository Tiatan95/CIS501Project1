using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class BJCustomer : BJPlayer
    {
        private int num_wins = 0;
        private int num_losses = 0;
        private int num_ties = 0;

        public BJCustomer(decimal m, Deck d) : base(m, d) { }

        public int NumWins
        {
            get
            {
                return num_wins;
            }
            set
            {
                num_wins = value;
            }
        }

        public int NumLosses
        {
            get
            {
                return num_losses;
            }
            set
            {
                num_losses = value;
            }
        }

        public int NumTies
        {
            get
            {
                return num_ties;
            }
            set
            {
                num_ties = value;
            }
        }

    }
}
