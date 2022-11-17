using System.Collections.Generic;
using Cards;
using Game.Factories;
using Types;
using UnityEngine;

namespace Game
{
    public class CardListRenderer: MonoBehaviour
    {
        private MonoCardGrid cardRoot => GetComponent<MonoCardGrid>();
        private List<Card> currentList;

        public void RenderList(List<Card> list)
        {
            currentList = list;
            
            cardRoot.ClearGrid();
            foreach (var card in list)
            {
                CardFactory.FabricateCard(card,"Card", Vector3.zero, null, (c) => {
                    cardRoot.AddToGrid(c);
                });
            }
        }
        
        public void RenderList(Card[] list)
        {
            currentList = new List<Card>(list);
            
            cardRoot.ClearGrid();
            foreach (var card in list)
            {
                CardFactory.FabricateCard(card,"Card", Vector3.zero, null, (c) => {
                    cardRoot.AddToGrid(c);
                });
            }
        }

        public void AddCardToList(Card card)
        {
            currentList.Add(card);
            CardFactory.FabricateCard(card,"Card", Vector3.zero, null, (c) => {
                cardRoot.AddToGrid(c);
            });
        }

        public void RemoveCardFromList(Card card)
        {
            currentList.Remove(card);
            cardRoot.ClearGrid();
            
            RenderList(currentList);
        }
    }
}