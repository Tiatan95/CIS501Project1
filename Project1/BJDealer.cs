using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class BJDealer : BJPlayer
    {
        public BJDealer(decimal m, Deck d) : base(m, d) { }
        
        /////////////////////////////////////////////////////////////////EDIT
        public void flipCard(int index)
        {
            hand[index].FaceUp = true;
        }
        //////////////////EDIT///////////////////////////////////////////////
    }
}
