using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerLib
{
    class PokerHand
    {
        public PokerHandType HandType { get; private set; }
        public String Name { get; private set; }
        public List<PokerCard> PokerCardList { get; private set; }
        public List<PokerCard> HighCardList { get; private set; }

        //The NameLine is the line of text that should contain only the name of the player of this hand (No strict rules)
        //The PokerCardsLine is the line that should contain information about the 5 cards for this hand
        public PokerHand(string NameLine, string PokerCardsLine)
        {
            Name = NameLine;

            AddCardsToHand(PokerCardsLine);

            HandType = FindHandType();
        }

        private void AddCardsToHand(string PokerCardsLine)
        {
            PokerCardList = new List<PokerCard>();

            //Checks to see if there are any commas
            if (PokerCardsLine.IndexOf(',') < 0)
                throw new Exception("Poker hands must be separated by a comma [,]");

            //Count to see if there are exactly 4 commas
            if (PokerCardsLine.Split(',').Length - 1 != 4)
                throw new Exception("Each hand must have exactly 5 cards");

            //Remove whitespace
            PokerCardsLine = PokerCardsLine.Replace(" ", "");

            for (int i = 0; i < 5; i++)
            {
                if (i < 4) //For first 4 cards, all delimited by a comma
                {
                    PokerCardList.Add(new PokerCard(PokerCardsLine.Substring(0, PokerCardsLine.IndexOf(','))));
                    PokerCardsLine = PokerCardsLine.Substring(PokerCardsLine.IndexOf(',') + 1, PokerCardsLine.Length - PokerCardsLine.IndexOf(',') - 1);
                }
                else //For the final card
                {
                    PokerCardList.Add(new PokerCard(PokerCardsLine));
                }
            }
        }

        private PokerHandType FindHandType()
        {
            //This is the list used for tiebreakers
            HighCardList = new List<PokerCard>();
            PokerCardList = PokerCardList.OrderByDescending(f => f.Value).ToList();

            ///////////////////////////////////////////////////////////////////////////
            //Determine if the hand is a flush
                bool isFlush = true;
                PokerCardSuit flushSuit = PokerCardList[0].Suit;

            //If any suits do not match, not a flush
            for (int i = 1; i < PokerCardList.Count; i++)
                if (PokerCardList[i].Suit != flushSuit)
                    isFlush = false;

            if (isFlush)
            {
                HighCardList = PokerCardList;
                return PokerHandType.Flush;
            }
            ///////////////////////////////////////////////////////////////////////////


            ///////////////////////////////////////////////////////////////////////////
            //Determine if the hand is a 3 of a kind or a pair
            int sameValueCount;
            int best3ofKindIndex = -1;
            int best2ofKindIndex = -1;

            for (int i = 0; i < PokerCardList.Count; i++)
            {
                sameValueCount = 1;
                for (int j = 0; j < PokerCardList.Count; j++)
                    if (i != j)
                        if (PokerCardList[i].Value == PokerCardList[j].Value)
                            sameValueCount++;

                if (sameValueCount > 2) //find 3 of a kind
                {
                    if (best3ofKindIndex >= 0)
                        if (PokerCardList[best3ofKindIndex].Value < PokerCardList[i].Value)
                            best3ofKindIndex = i;
                    else
                        best3ofKindIndex = i;
                }

                else if (sameValueCount == 2) //find pairs
                {
                    if (best2ofKindIndex >= 0)
                        if (PokerCardList[best2ofKindIndex].Value < PokerCardList[i].Value)
                            best2ofKindIndex = i;
                    else
                        best2ofKindIndex = i;
                }
            }

            if(best3ofKindIndex >= 0) //If there is a 3 of a kind
            {
                for (int i = 0; i < PokerCardList.Count; i++) //First add the 3 of a kind to the high card list
                    if (PokerCardList[i].Value == PokerCardList[best3ofKindIndex].Value)
                        HighCardList.Add(PokerCardList[i]);

                for (int i = 0; i < PokerCardList.Count; i++) //Then add all other cards in descending order
                    if (PokerCardList[i].Value != PokerCardList[best3ofKindIndex].Value)
                        HighCardList.Add(PokerCardList[i]);

                return PokerHandType.ThreeOfKind;
            }

            else if (best2ofKindIndex >= 0) //If there is a pair
            {
                for (int i = 0; i < PokerCardList.Count; i++) //First add the pair to the high card list
                    if (PokerCardList[i].Value == PokerCardList[best2ofKindIndex].Value)
                        HighCardList.Add(PokerCardList[i]);

                for (int i = 0; i < PokerCardList.Count; i++) //Then add all other cards in descending order
                    if (PokerCardList[i].Value != PokerCardList[best2ofKindIndex].Value)
                        HighCardList.Add(PokerCardList[i]);

                return PokerHandType.Pair;
            }
            ///////////////////////////////////////////////////////////////////////////


            ///////////////////////////////////////////////////////////////////////////
            //If nothing else, its a high card
            HighCardList = PokerCardList;
            return PokerHandType.HighCard;
            ///////////////////////////////////////////////////////////////////////////
        }
    }
}
