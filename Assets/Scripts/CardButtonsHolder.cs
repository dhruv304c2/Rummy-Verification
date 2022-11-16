using System.Collections.Generic;
using Cards;
using Game.Factories;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class CardButtonsHolder: MonoBehaviour
    {
        private MonoCardGrid cardRoot => GetComponent<MonoCardGrid>();
        private List<Card> currentList;
        private ISelectionController _selectionController;

        public void RenderButtonList(List<Card> list, ISelectionController selectionController)
        {
            currentList = list;
            _selectionController = selectionController;
            foreach (var card in list)
            {
                CardFactory.FabricateCard(card,"Card_Small", Vector3.zero, null, (c) =>
                {
                    cardRoot.AddToGrid(c);
                    c.GetComponent<MonoCardSelectionButton>().AddSelectionController(_selectionController);
                });
            }
        }

        public void AddCardToButtonList(Card card)
        {
            currentList.Add(card);
            MonoCard C = null;
            CardFactory.FabricateCard(card,"Card_Small", Vector3.zero, null, (c) =>
            {
                cardRoot.AddToGrid(c);
                C = c;
            });

            C.GetComponent<MonoCardSelectionButton>().AddSelectionController(_selectionController);
        }

        public void RemoveCardFromButtonList(Card card)
        {
            currentList.Remove(card);
            cardRoot.ClearGrid();
            
            RenderButtonList(currentList, _selectionController);
        }
    }
}