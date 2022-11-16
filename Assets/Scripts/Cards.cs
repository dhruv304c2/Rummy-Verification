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
            //TotalGroups.AddRange(GetAllSets(hand));

            //Print text
            string text = "";
            foreach (var group in TotalGroups)
            {
                foreach (var card in group)
                {
                    text += new CardRenderData(card).CardText;
                }

                text += "\n";
            }
            
            Debug.Log(text);
        }
        
        public static List<Card[]> GetPureSequence(List<Card> cards)
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

        public static List<List<Card>> GetAllSets(List<Card> cards)
        {
            List<List<Card>> Sets = new List<List<Card>>();
            
            if (cards.Count == 0) return Sets;
            
            cards.Sort((a, b) => { return (int)a.Number - (int)b.Number;});
            
            List<Card> Set = new List<Card>();
            Set.Add(cards[0]);
            
            for (int i = 1; i < cards.Count; i++)
            {
                if ((int)cards[i].Number - (int)cards[i-1].Number == 0|| Set.Count == 0)
                {
                    Set.Add(cards[i]);
                    if (Set.Count >= 3)
                    {
                        Sets.Add(Set);
                        var temp = Set;
                        Set = new List<Card>(temp);
                    }
                }
                else
                {
                    Set = new List<Card>();
                }
            }

            return Sets;
        }
    }
}