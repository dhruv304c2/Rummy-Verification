using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace Game
{
    public static class Cards
    {
        public static void GetGroups(List<Card> hand)
        {
            //Separate Suits
            List<Card> spades = new List<Card>();
            List<Card> clubs = new List<Card>();
            List<Card> hearts = new List<Card>();
            List<Card> diamonds = new List<Card>();

            foreach (var card in hand)
            {
                switch (card.Suit)
                {
                    case Suits.Spades:
                        spades.Add(card);
                        break;
                    case Suits.Clubs:
                        clubs.Add(card);
                        break;
                    case Suits.Diamonds:
                        diamonds.Add(card);
                        break;
                    case Suits.Hearts:
                        hearts.Add(card);
                        break;
                }
            }

            //Getting all possible groups
            List<Card[]> TotalGroups = new List<Card[]>();

            TotalGroups.AddRange(GetPureSequence(spades));
            TotalGroups.AddRange(GetPureSequence(clubs));
            TotalGroups.AddRange(GetPureSequence(hearts));
            TotalGroups.AddRange(GetPureSequence(diamonds));
            TotalGroups.AddRange(GetAllSets(hand));

            var AllHands = GetAllPossibleHands(TotalGroups, hand);
            
            //Print text
            string text = "Groups:\n";
            foreach (var group in AllHands)
            {
                foreach (var card in group)
                {
                    text += new CardRenderData(card).CardText;
                }

                text += "\n";
            }
            
            Debug.Log(text);
        }

        private static List<Card[]> GetAllPossibleHands(List<Card[]> groups, List<Card> hand)
        {
            List<Card[]> AllHands = new List<Card[]>();
            
            for (int i = 0; i < groups.Count-1; i++)
            {
                List<Card> _hand = new List<Card>(groups[i]);
                for (int j = 0; j < groups.Count; j++)
                {
                    List<Card> _tempHand = new List<Card>(_hand);
                    _tempHand.AddRange(groups[j]);

                    if (CheckSubset(_tempHand, hand) == true)
                    {
                        _hand = _tempHand;
                    }
                }
                AllHands.Add(_hand.ToArray());
            }

            return AllHands;
        }

        private static List<Card[]> GetPureSequence(List<Card> cards)
        {
            List<Card[]> Sequences = new List<Card[]>();

            if (cards.Count == 0) return Sequences;
            
            cards.Sort((a, b) => { return (int)a.Number - (int)b.Number;});

            for (int j = 1; j < cards.Count; j++)
            {
                List<Card> sequence = new List<Card>();
                sequence.Add(cards[j-1]);
                
                for (int i = j; i < cards.Count; i++)
                {
                    if (sequence.Count == 4) //No array with size above 4 are allowed.
                    {
                        sequence = new List<Card>();
                        continue;
                    }
                    
                    if ((int)cards[i].Number - (int)cards[i-1].Number == 1)
                    {
                        sequence.Add(cards[i]);
                        if (sequence.Count >= 3)
                        {
                            Sequences.Add(sequence.ToArray());
                            var temp = sequence;
                            sequence = new List<Card>(temp);
                        }
                    }
                    else
                    {
                        sequence = new List<Card>();
                        sequence.Add(cards[i]);
                    }
                }
            }
            Sequences = RemoveDuplicates(Sequences);
            Sequences.Sort((a, b) => { return a[0].Number - b[0].Number;});
            return Sequences;
        }
        
        private static List<Card[]> GetAllSets(List<Card> cards)
        {
            List<Card[]> Sequences = new List<Card[]>();

            if (cards.Count == 0) return Sequences;
            
            cards.Sort((a, b) => { return (int)a.Number - (int)b.Number;});

            for (int j = 1; j < cards.Count; j++)
            {
                List<Card> sequence = new List<Card>();
                sequence.Add(cards[j-1]);
                
                for (int i = j; i < cards.Count; i++)
                {
                    if (sequence.Count == 4)
                    {
                        sequence = new List<Card>();
                        continue;
                    }
                    
                    if ((int)cards[i].Number - (int)cards[i-1].Number == 0)
                    {
                        sequence.Add(cards[i]);
                        if (sequence.Count >= 3)
                        {
                            if(!HasDuplicateCards(sequence.ToArray())) // Add sets only if they don't have duplicate cards.
                                Sequences.Add(sequence.ToArray());
                            var temp = sequence;
                            sequence = new List<Card>(temp);
                        }
                    }
                    else
                    {
                        sequence = new List<Card>();
                        sequence.Add(cards[i]);
                    }
                }
            }
            Sequences = RemoveDuplicates(Sequences);
            Sequences.Sort((a, b) => { return a[0].Number - b[0].Number;});
            return Sequences;
        }

        public static List<Card[]> RemoveDuplicates(List<Card[]> groups)
        {
            List<Card[]> freshList = new List<Card[]>();

            foreach (var group in groups)
            {
                if (ContainsGroup(group, freshList) == false)
                {
                    freshList.Add(group);
                }
            }

            return freshList;
        }

        public static bool ContainsGroup(Card[] group, List<Card[]> GroupCollection)
        {
            foreach (var g in GroupCollection)
            {
                if (group.Length == g.Length && group[0].Number == g[0].Number && group[0].Suit == g[0].Suit)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasDuplicateCards(Card[] cards)
        {
            HashSet<Card> cardSet = new HashSet<Card>();
            foreach (var card in cards)
            {
                cardSet.Add(card);
            }

            return cardSet.Count != cards.Length;
        }

        public static int GetCardCount(Card card,List<Card> cards)
        {
            int count = 0;
            foreach (var card1 in cards)
            {
                if (card1.Number == card.Number && card.Suit == card1.Suit)
                {
                    count++;
                }
            }

            return count;
        }

        public static bool CheckSubset(List<Card> subset, List<Card> set)
        {
            foreach (var card in subset)
            {
                if (GetCardCount(card, subset) != GetCardCount(card, set))
                {
                    return false;
                }
            }

            return true;
        }
    }
}