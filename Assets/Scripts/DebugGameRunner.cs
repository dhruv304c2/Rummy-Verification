using System.Collections.Generic;
using Cards;
using Types;
using UnityEngine;
using Game.Factories;
using Unity.VisualScripting;

namespace Game
{
    public class DebugGameRunner : MonoBehaviour, ISelectionController
    {
        public CardListRenderer handList;
        public CardButtonsHolder buttonsHolder;
        private void Start()
        {
            List<Card> fullDeck = new List<Card>();

            for (int i=0 ; i < 4; i++)  
            {
                for (int j=0; j < 13; j++)
                {
                    Card card = new Card( (Numbers)j, (Suits)i);
                    fullDeck.Add(card);
                }
            }
            
            buttonsHolder.RenderButtonList(fullDeck, this);
            
        }

        [SerializeField] private List<Card> _hand = new List<Card>();
        public List<Card> SelectionList => _hand;
        public CardListRenderer SelectionRenderer => handList;
        public int MaxSelections { get; set; } = 13;

        public void ClearSelection()
        {
            _hand = new List<Card>();
            handList.RenderList(_hand);
        }

        public void GetAllGroups()
        {
            Cards.GetGroups(_hand);
        }
    }
}