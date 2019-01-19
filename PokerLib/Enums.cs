using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLib
{
    public enum PokerHandType
    {
        HighCard,
        Pair,
        ThreeOfKind,
        Flush
    }

    public enum PokerCardSuit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum PokerCardValue
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
}
