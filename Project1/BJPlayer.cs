using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class BJPlayer
    {
        protected Card[] hand;
        private int topCardIndex;
        private int handValue;
        int numCards = 0;
        private decimal money;
        protected StringBuilder handString;
        private int numAces;
        private int LARGEST_HAND_SIZE = 11;

        public BJPlayer(decimal m, Deck d)
        {
            money = m;
            handValue = 0;
            numAces = 0;
            //////////////////////////////////////////////////////////////////////////////EDIT
            topCardIndex = d.TopCardIndex;
            hand = new Card[LARGEST_HAND_SIZE];
            //////////////////////////////////////////////////////////////////////////////EDIT
            handString = new StringBuilder();

        }

        public int HandValue
        {
            get
            {
                return handValue; 
            }
            set
            {
                handValue = value;
            }
        }

        public decimal Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
            }
        }

///////////////////////////////////////////////////////////////EDIT
        public void Draw(string s,bool b)
        {
            Card newCard = Deck.Draw(s.ToUpper());
            newCard.FaceUp = b;
            handString.Append(newCard.ToString()+ ", ");
            if(newCard.Rank == 1)
            {
                numAces++;
            }
            hand[numCards] = newCard;
            numCards++;
           
        }
///////////////////////////////////////////////////////////////EDIT
        public void ReturnHandCardsToDeck()
        {
            foreach(Card i in hand)
            {
                topCardIndex++;
            }
            numAces = 0;
            handValue = 0;
            handString.Clear();
        }
        public string ToString()
        {
            return handString.ToString();
        }

        public void getHandValue()
        {
            for(int i =0; i < numCards; i++)
            {
                if (hand[i].Rank > 9)
                {
                    handValue += 10;
                }
                else if (hand[i].Rank > 1)
                {
                    handValue += hand[i].Rank;
                }
                else { }
            }
            for(int i =0; i < numAces; i++)
            {
                if(handValue < 11)
                {
                    handValue += 11;
                }
                else
                {
                    handValue += 1;
                }
            }


        }
/////////////////////////////////////////////////////////////////////EDIT

    }

}
