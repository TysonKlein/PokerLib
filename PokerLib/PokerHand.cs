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
            HighCardList = new List<PokerCard>();

            PokerCardList = PokerCardList.OrderByDescending(f => f.Value).ToList();

            //Determine if the hand is a flush
            bool isFlush = true;
            PokerCardSuit flushSuit = PokerCardList[0].Suit;

            for (int i = 1; i < PokerCardList.Count; i++)
            {
                if (PokerCardList[i].Suit != flushSuit)
                    isFlush = false;
            }

            if (isFlush)
            {
                HighCardList = PokerCardList;
                return PokerHandType.Flush;
            }

            //Determine if the hand is a 3 of a kind or a pair
            int sameValueCount;
            int best3oKIndex = -1;
            int best2oKIndex = -1;


            for (int i = 0; i < PokerCardList.Count; i++)
            {
                sameValueCount = 1;
                for (int j = 0; j < PokerCardList.Count; j++)
                {
                    if (i != j)
                    {
                        if (PokerCardList[i].Value == PokerCardList[j].Value)
                        {
                            sameValueCount++;
                        }
                    }
                }
                if (sameValueCount > 2) //For 3 of a kind
                {
                    if (best3oKIndex >= 0)
                    {
                        if (PokerCardList[best3oKIndex].Value < PokerCardList[i].Value)
                        {
                            best3oKIndex = i;
                        }
                    }
                    else
                    {
                        best3oKIndex = i;
                    }
                }

                else if (sameValueCount == 2) //For pairs
                {
                    if (best3oKIndex >= 0)
                    {
                        if (PokerCardList[best2oKIndex].Value < PokerCardList[i].Value)
                        {
                            best2oKIndex = i;
                        }
                    }
                    else
                    {
                        best2oKIndex = i;
                    }
                }
            }

            if(best3oKIndex >= 0)
            {
                for (int i = 0; i < PokerCardList.Count; i++)
                {
                    if(PokerCardList[i].Value == PokerCardList[best3oKIndex].Value)
                    {
                        HighCardList.Add(PokerCardList[i]);
                    }
                }
                for (int i = 0; i < PokerCardList.Count; i++)
                {
                    if (PokerCardList[i].Value != PokerCardList[best3oKIndex].Value)
                    {
                        HighCardList.Add(PokerCardList[i]);
                    }
                }

                return PokerHandType.ThreeOfKind;
            }

            else if (best2oKIndex >= 0)
            {
                for (int i = 0; i < PokerCardList.Count; i++)
                {
                    if (PokerCardList[i].Value == PokerCardList[best2oKIndex].Value)
                    {
                        HighCardList.Add(PokerCardList[i]);
                    }
                }
                for (int i = 0; i < PokerCardList.Count; i++)
                {
                    if (PokerCardList[i].Value != PokerCardList[best2oKIndex].Value)
                    {
                        HighCardList.Add(PokerCardList[i]);
                    }
                }

                return PokerHandType.Pair;
            }

            HighCardList = PokerCardList;
            return PokerHandType.HighCard;
        }
    }
}
