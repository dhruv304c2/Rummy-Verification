using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;
using Hand = System.Collections.Generic.List<Cards.Card[]>;

namespace Game
{
    public static class Cards
    {
        public static List<Card[]> GetGroups(List<Card> hand)
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
            List<Card[]> AllSequences = new List<Card[]>();
            List<Card[]> AllSets = new List<Card[]>();

            AllSequences.AddRange(GetPureSequence(spades));
            AllSequences.AddRange(GetPureSequence(clubs));
            AllSequences.AddRange(GetPureSequence(hearts));
            AllSequences.AddRange(GetPureSequence(diamonds));
            AllSets.AddRange(GetAllSets(hand));
            
            TotalGroups.AddRange(AllSequences);
            TotalGroups.AddRange(AllSets);

            var AllHands = GetAllPossibleHands(TotalGroups, hand);

            //Getting valid Hands
            var ValidHands = AllHands.Where((a) => { return IsHandValid(a); }).ToList();
            
            //Print valid hands text
            
            string text = "Sequences:\n";
            foreach (var group in AllSequences)
            {
                foreach (var card in group)
                {
                    text += new CardRenderData(card).CardText;
                }

                text += "\n";
            }
            Debug.Log(text);
            
            text = "Sets:\n";
            foreach (var group in AllSets)
            {
                foreach (var card in group)
                {
                    text += new CardRenderData(card).CardText;
                }

                text += "\n";
            }
            
            Debug.Log(text);
            
            text = "Groups:\n";
            foreach (var group in AllHands)
            {
                foreach (var cards in group)
                {
                    foreach (var card in cards)
                    {
                        text += new CardRenderData(card).CardText;
                    }
                    text += "\n";
                }

                text += "\n";
            }
            
            Debug.Log(text);

            return ValidHands[0];
        }

        private static List<List<Card[]>> GetAllPossibleHands(List<Card[]> groups, List<Card> hand)
        {
            List<List<Card[]>> AllHands = new List<List<Card[]>>();
            List<Card[]> group = new Hand(); 
            
            // nested loops to get all possible ways of selecting 4 groups
            for (int i = 0; i < groups.Count; i++)
            {
                group = new Hand();
                for (int j = i+1; j < groups.Count; j++)
                {
                    for (int k = j+1; k < groups.Count; k++)
                    {
                        for (int l = k+1; l < groups.Count; l++)
                        {
                            group = new List<Card[]>(){groups[i], groups[j], groups[k], groups[l]};
                            
                            List<Card> _tempHand = new List<Card>();
                            foreach (var _group in group)
                            {
                                foreach (var card in _group)
                                {
                                    _tempHand.Add(card);
                                }
                            }
                            
                            if(CheckSubset(_tempHand,hand)) AllHands.Add(group);
                        }
                    }
                }
                
                
            }

            return AllHands;
        }

        private static bool IsHandValid(Hand hand)
        {
            int sequenceCount = 0;
            int cardCount = 0;

            List<Card> flattenHand = new List<Card>();
            foreach (var group in hand)
            {
                cardCount += group.Length;
                List<Card> groupList = new List<Card>();
                groupList.AddRange(group);

                if (GetPureSequence(groupList).Count >= 1)
                    sequenceCount++;
            }

            return cardCount == 13 && sequenceCount >= 2;
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
                        break;
                    }
                    
                    if ((int)cards[i].Number - (int)sequence[sequence.Count -1].Number == 1)
                    {
                        sequence.Add(cards[i]);
                        
                        if (sequence.Count >= 3)
                        {
                            Sequences.Add(sequence.ToArray());
                            var temp = sequence;
                            sequence = new List<Card>(temp);
                        }
                    }
                }
            }
            //Sequences = RemoveDuplicates(Sequences);
            Sequences.Sort((a, b) => { return a[0].Number - b[0].Number;});
            return Sequences;
        }
        
        private static List<Card[]> GetAllSets(List<Card> cards)
        {
            List<Card[]> Sets = new List<Card[]>();

            if (cards.Count == 0) return Sets;
            
            cards.Sort((a, b) => { return (int)a.Number - (int)b.Number;});

            for (int j = 1; j < cards.Count; j++)
            {
                List<Card> set = new List<Card>();
                set.Add(cards[j-1]);
                
                for (int i = j; i < cards.Count; i++)
                {
                    if (set.Count == 4) //No array with size above 4 are allowed.
                    {
                        break;
                    }
                    
                    if ((int)cards[i].Number - (int)set[set.Count -1].Number == 0)
                    {
                        set.Add(cards[i]);
                        
                        if (set.Count >= 3)
                        {
                            Sets.Add(set.ToArray());
                            var temp = set;
                            set = new List<Card>(temp);
                        }
                    }
                }
            }
            //Sequences = RemoveDuplicates(Sequences);
            Sets.Sort((a, b) => { return a[0].Number - b[0].Number;});
            return Sets;
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