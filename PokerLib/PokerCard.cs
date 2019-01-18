using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLib
{
    class PokerCard
    {
        public PokerCardSuit Suit { get; private set; }
        public PokerCardValue Value { get; private set; }

        public PokerCard(PokerCardSuit newSuit, PokerCardValue newValue)
        {
            Suit = newSuit;
            Value = newValue;
        }
    }
}
