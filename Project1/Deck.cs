using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class Deck
    {
        private Card[] deck = new Card[52];
        private int topCardIndex;
        private const int NUM_SUITS = 5;
        private const int RANKS = 14;
        private const int DECK_SIZE = 51;
        private const int CARD_STRING_SIZE = 2;




        public Deck()
        {
            int index = 0;
            for(int i = 1; i < NUM_SUITS; i++)
            { 
                for(int j = 1; j < RANKS;++j)
                {
                    Card newCard = new Card(j,(CardSuit)i);
                    deck[index] = newCard;
                    index++;
                }
            }
            topCardIndex = 51;
        }

        public void Shuffle()
        {
            Random r = new Random();
            for(int i = DECK_SIZE; i > 0; i--)
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
        public Card Draw(string c)
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
                checkString(c);
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
        public void checkString(string s)
        {
            if(s.Length > CARD_STRING_SIZE)
            {
                Console.WriteLine("!! === ERROR ===  Input Two Characters !!");
                throw new InvalidOperationException();
            }
            if(s[1] != 'D' && s[1] != 'H' && s[1] != 'C' && s[1] != 'S' )
            {
                Console.WriteLine("!! === ERROR === Input Valid Suit Charcter !!");
                throw new InvalidOperationException();
            }
            if(!Char.IsDigit(s[0]))
            {
                if(s[0] != 'A' && s[0] != 'T' && s[0] != 'J' && s[0] != 'Q' && s[0] != 'K')
                {
                    Console.WriteLine("!! === ERROR === Input Valid Rank Character !!");
                    throw new InvalidOperationException();
                }
            }
            foreach(Card i in deck)
            {
                if(i.ToString().Equals(s))
                {
                    return;
                }
            }
            Console.WriteLine("=== ERROR === The card you selected is not in the deck");
            throw new InvalidOperationException();
        }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }


}
