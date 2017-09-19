using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class Deck
    {
        private static Card[] deck = new Card[52];
        private static int topCardIndex;
        private const int NUM_SUITS = 4;
        private const int RANKS = 14;




        /// <summary>
        /// Got the enumeration get values from MSDN 
        /// link: https://msdn.microsoft.com/en-us/library/system.enum.getvalues(v=vs.110).aspx 
        /// </summary>
        public Deck()
        {
            var s = Enum.GetValues(typeof(CardSuit));
            int index = 0;
            foreach(CardSuit i in s)
            {
                for(int j = 1; j < RANKS;++j)
                {
                    Card newCard = new Card(j, i);
                    deck[index] = newCard;
                    index++;
                }
            }
            topCardIndex = 51;
        }

        public void Shuffle()
        {
            Random r = new Random();
            for(int i = 51; i > 0; i--)
            {
                int j = r.Next(i);

                ///shuffles the cards based on index
                Card temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
           
        }
        public int TopCardIndex
        {
            get
            {
                return topCardIndex;
            }
            set
            {
                topCardIndex = value;   
            }
        }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////EDIT
        public static Card Draw(string c)
        {
#if DEBUG
            if (c.Equals("XX"))
            {
                Card newCard = deck[topCardIndex];
                topCardIndex--;
                return newCard;
            }
            else
            {
                for (int i = 0; i < deck.Length; i++)
                {
                    if (deck[i].ToString().Equals(c))
                    {
                        Card dCard = deck[i];
                        deck[i] = deck[topCardIndex];
                        topCardIndex--;
                        return dCard;
                    }
                }
            }
            return null;

#else
              Card newCard = deck[topCardIndex];
                topCardIndex--;
                return newCard;
            
#endif
        }
        public void ReturnCard(Card c)
        {
            topCardIndex++;
            deck[topCardIndex] = c;
        }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
