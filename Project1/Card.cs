using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public enum CardSuit
    {
        C = 'C',
        H = 'H',
        D = 'D',
        S = 'S'
    }

    class Card
    {
        private CardSuit suit;
        private int rank;
        private bool faceup = true;
        public CardSuit Suit
        {
            get
            {
                return suit;
            }
            set
            {
                suit = value;
            }
        }
        public int Rank
        {
            get
            {
                return rank;
            }
            set
            {
                rank = value;
            }
        }
        public bool FaceUp
        {
            get
            {
                return faceup;
            }
            set
            {
                faceup = value;
            }
        }
        public Card(int r,CardSuit s)
        {
            this.rank = r;
            this.suit = s;
        }
        public string ToString()
        {
            string c;
            if (this.faceup)
            {
                if (rank == 1)
                {
                    c = ("A" + suit.ToString());
                }
                else if (rank == 11)
                {
                    c = ("J" + suit.ToString());
                }
                else if (rank == 12)
                {
                    c = ("Q" + suit.ToString());
                }
                else if (rank == 13)
                {
                    c = ("K" + suit.ToString());
                }
                else
                {
                    c = (string)(rank + suit.ToString());
                }
                return c;
            }
            else
            {
                return "XX";
            }
        }
            
        }


    }

