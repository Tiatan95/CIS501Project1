using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class BlackJack
    {
        private Deck d;
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
                customer.HandValue = 0;
                dealer.HandValue = 0;
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
            Console.WriteLine("Your Hand: " + customer.ToString() + " Hand Value:" + customer.HandValue);
            Console.WriteLine("Dealer Hand: " + dealer.ToString());
            if (testNatual21())
            {
                if(customer.HandValue == 21 && dealer.HandValue == 21)
                {
                    Console.WriteLine("Both got Natural21");
                    Console.WriteLine("Stand Off");
                    customer.NumTies++;
                    return;
                }
                else
                {
                    Console.WriteLine("You got Natural21:  " + ((int)(betAmount * (decimal)2.5)) + " Goes to You from Dealer");
                    customer.Money += (int)(betAmount * (decimal)2.5);
                    customer.NumWins++;
                    return;
                }            
            }
            if (testSurrender())
                return;
            customerTurn(out bust);
            if (bust)
            {
                Console.WriteLine("BUST");
                Console.WriteLine("Dealer Won and got " + betAmount + " from user");
                customer.Money -= betAmount;
                customer.NumLosses++;
                return;
            }
            dealerTurn(out bust);
            if(bust)
            {
                Console.WriteLine("Dealer BUST");
                Console.WriteLine("You Won and got " + betAmount + " from dealer");
                customer.Money += betAmount;
                customer.NumWins++;
                return;
            }
            determineWinner();

        }
        private void determineWinner()
        {
            if(customer.HandValue > dealer.HandValue)
            {
                Console.WriteLine("You Won and got $" + betAmount + " from dealer");
                customer.Money += betAmount;
                customer.NumWins++;
            }
            else if(dealer.HandValue > customer.HandValue)
            {
                Console.WriteLine("Dealer Won and got $" + betAmount + " from user");
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
                bool acceptable = false;
                while (!acceptable)
                {
                    try
                    {
                        Console.Write("Input next car for dealer (3H, AD, TC, etc. or XX to draw from deck) :");
                        dealer.Draw(Console.ReadLine(), true);
                        acceptable = true;
                    }
                    catch
                    {
                        acceptable = false;
                    }
                }
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
                    bool acceptable = false;
                    while (!acceptable)
                    {
                        try
                        {
                            Console.Write("Input next car for customer (3H, AD, TC, etc. or XX to draw from deck) :");
                            customer.Draw(Console.ReadLine(), true);
                            acceptable = true;
                        }
                        catch
                        {
                            acceptable = false;
                        }
                    }
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
            bool acceptable = false;

            while (!acceptable)
            {
                try
                {
                    Console.Write("Input 1st card for customer(3H, AD, TC, etc.or XX to draw from deck) : ");
                    customer.Draw(Console.ReadLine().ToUpper(), true);
                    acceptable = true;
                }
                catch
                {
                    acceptable = false;
                }
             
            }
            acceptable = false;

            while (!acceptable)
            {
                try
                {
                    Console.Write("Input 1st card for dealer(3H, AD, TC, etc.or XX to draw from deck) : ");
                    dealer.Draw(Console.ReadLine().ToUpper(), true);
                    acceptable = true;
                }
                catch
                {
                    acceptable = false;
                }
            }
            acceptable = false;

            while (!acceptable)
            {
                try
                {
                    Console.Write("Input 2nd card for customer (3H, AD, TC, etc. or XX to draw from deck) : ");
                    customer.Draw(Console.ReadLine().ToUpper(), true);
                    acceptable = true;
                }
                catch
                {
                    acceptable = false;
                }
            }
            acceptable = false;
            while (!acceptable)
            {
                try
                {
                    Console.Write("Input 2nd card for dealer (3H, AD, TC, etc. or XX to draw from deck) : ");
                    dealer.Draw(Console.ReadLine().ToUpper(), false);
                    acceptable = true;
                }
                catch
                {
                    acceptable = false;
                }
            }
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
            Console.WriteLine("You have $" + customer.Money);
            Console.WriteLine("Wins: " + customer.NumWins + ", Losses: " + customer.NumLosses + ", Ties: " + customer.NumTies);
        }

        void getUserBet()
        {
            bool b = false;
            Console.Write("Enter Bet Amount: $ ");
            while (!b)
            {
                try
                {  
                    betAmount = Convert.ToDecimal(Console.ReadLine());
                    if(betAmount <= customer.Money && betAmount > 0)
                        b = true;
                    else
                    {
                        throw new InvalidOperationException(); 
                    }
                }
                catch
                {
                    Console.Write("Enter Bet Amount: $ ");
                }
            }
        }


    }
}
