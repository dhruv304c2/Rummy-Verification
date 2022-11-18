using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace Game
{
    public interface ISelectionController
    {
        List<Card> SelectionList { get; }
        CardListRenderer SelectionRenderer { get; }
        int MaxSelections { get; set; }

        public void AddSelection(Card card)
        {
            if (Cards.GetCardCount(card, SelectionList) >= 2)
            {
                GameLogger.LogError("Already have two of this card");
                return;
            }
            
            if (SelectionList.Count < MaxSelections)
            {
                GameLogger.Log("");
                SelectionList.Add(card);
                SelectionRenderer.RenderList(SelectionList);
            }
            else
            {
                GameLogger.LogError("Selection capacity reached");
            }
        }

        public void ClearSelection();
        public void RemoveOne();
    }
}