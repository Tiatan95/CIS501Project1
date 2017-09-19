using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class BlackJack
    {
        private static Deck d;
        BJDealer dealer;
        BJCustomer customer;
        decimal betAmount;
        public BlackJack()
        {
            d = new Deck();
            dealer = new BJDealer(200, d);
            customer = new BJCustomer(100, d);
        }

        public void go()
        {
            bool bankrupt = false;
            while(true)
            {
                playOneGame();

                customer.ReturnHandCardsToDeck();
                dealer.ReturnHandCardsToDeck();

                displayStat(out bankrupt);

                if (bankrupt)
                    break;
                if (!checkMoreGames())
                    break;
            }
        }
        private bool checkMoreGames()
        {
            string answer = " ";
            do
            {
                Console.Write("More Games?(Y or N) ");
                answer = Console.ReadLine().ToUpper();
            }while(answer[0] != 'Y' && answer[0] != 'N');
            if(answer[0] == 'Y')
            {
                return true;
            }
            return false;
        }

        private void playOneGame()
        {
            bool bust = false;

            Console.WriteLine("========== New Game ============");
            Console.WriteLine("You have : $" + customer.Money);

            getUserBet();

            d.Shuffle();
            dealCards();

            if(testNatual21())
            {
                if(customer.HandValue == 21 && dealer.HandValue == 21)
                {
                    customer.NumTies++;
                    return;
                }
                else
                {
                    customer.NumWins++;
                    return;
                }            
            }
            Console.WriteLine("Your Hand: " + customer.ToString() + " Hand Value:" + customer.HandValue);
            Console.WriteLine("Dealer Hand: " + dealer.ToString());
            if (testSurrender())
                return;
            customerTurn(out bust);
            if (bust)
            {
                Console.WriteLine("BUST");
                return; 
            }
            dealerTurn(out bust);
            if(bust)
            {
                
                Console.WriteLine("Dealer BUST");
                return;
            }
            determineWinner();

        }
        private void determineWinner()
        {
            if(customer.HandValue > dealer.HandValue)
            {
                Console.WriteLine("You Won and got " + betAmount + " from dealer");
                customer.Money += betAmount;
                customer.NumWins++;
            }
            else if(dealer.HandValue > customer.HandValue)
            {
                Console.WriteLine("Dealer Won and got " + betAmount + "from user");
                customer.Money -= betAmount;
                customer.NumLosses++;
            }
            else
            {
                Console.WriteLine("Players tied");
                customer.NumTies++;
            }
        }

        private void dealerTurn(out bool b)
        {
            b = false;
            dealer.flipCard(1);
            Console.WriteLine("Dealer Hand: " + dealer.ToString() + " Hand Value:" + dealer.HandValue);
            while (dealer.HandValue < 17)
            {
#if DEBUG
                Console.Write("Input next car for dealer (3H, AD, TC, etc. or XX to draw from deck) :");
                dealer.Draw(Console.ReadLine(), true);
#else
                    dealer.Draw("XX", true);
#endif
                Console.WriteLine("Dealer Hand: " + dealer.ToString() + " Hand Value:" + dealer.HandValue);
                if (dealer.HandValue > 21)
                {
                    b = true;
                    return;
                }
            }
        }

        private void customerTurn(out bool b)
        {
            b = false;
            string answer = " ";
            while (answer[0] != 'S')
            {
                while (answer[0] != 'H' && answer[0] != 'S')
                {
                    Console.Write("Will you HIT or STAND (H or S) ? : ");
                    answer = Console.ReadLine().ToUpper();
                }
                if(answer[0] == 'H')
                {
#if DEBUG
                    Console.Write("Input next car for customer (3H, AD, TC, etc. or XX to draw from deck) :");
                    customer.Draw(Console.ReadLine(), true);
#else
                    customer.Draw("XX", true);
#endif
                    Console.WriteLine("Your Hand: "+customer.ToString() + " Hand Value:" + customer.HandValue);
                    if(customer.HandValue > 21)
                    {
                        b = true;
                        return;
                    }
                    answer = " ";
                }
               
            }
        }



       
        private bool testSurrender()
        {
            string answer = " ";
            while (answer[0] != 'Y' && answer[0] != 'N')
            {
                Console.Write("Do you want to surrender (Y or N) ? :");
                answer = Console.ReadLine().ToUpper();
            }
            if(answer[0] == 'Y')
            {
                customer.Money -= betAmount *(decimal) .5;
                return true;
            }
            else
            {
                return false;
            }
        }



        private bool testNatual21()
        {
            if(customer.HandValue== 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void dealCards()
        {
#if DEBUG
            Console.Write("Input 1st card for customer(3H, AD, TC, etc.or XX to draw from deck) : ");
            customer.Draw(Console.ReadLine().ToUpper(),true);
            Console.Write("Input 1st card for dealer(3H, AD, TC, etc.or XX to draw from deck) : ");
            dealer.Draw(Console.ReadLine().ToUpper(), true);
            Console.Write("Input 2nd card for customer (3H, AD, TC, etc. or XX to draw from deck) : ");
            customer.Draw(Console.ReadLine().ToUpper(), true);
            Console.Write("Input 2nd card for dealer (3H, AD, TC, etc. or XX to draw from deck) : ");
            dealer.Draw(Console.ReadLine().ToUpper(), false);
            customer.getHandValue();
#else
            customer.Draw("XX", true);
            customer.Draw("XX", true);
            dealer.Draw("XX", true);
            dealer.Draw("XX", false);
            dealer.getHandValue();
            customer.getHandValue();
#endif
        }

        public void displayStat(out bool b)
        {
            b = false;
            if(customer.Money <= 0)
            {
                b = true;
            }
            if(dealer.Money <= 0)
            {
                b = true;
            }

            Console.WriteLine("Wins: " + customer.NumWins + ", Losses: " + customer.NumLosses + ", Ties: " + customer.NumTies);
        }

        void getUserBet()
        {
            decimal bet = 0;
            bool b = false;
            Console.Write("Enter Bet Amount: $ ");
            while (!b)
            {
                try
                {  
                    bet = Convert.ToDecimal(Console.ReadLine());
                    b = true;
                }
                catch
                {
                    Console.Write("Enter Bet Amount: $ ");
                }
            }
        }


    }
}
