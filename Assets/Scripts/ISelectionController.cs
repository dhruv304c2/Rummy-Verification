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
            if (SelectionList.Count < MaxSelections)
            {
                Debug.Log("Selection Made");
                SelectionList.Add(card);
                SelectionRenderer.RenderList(SelectionList);
            }
            else
            {
                Debug.Log("Selection capacity reached");
            }
        }

        public void ClearSelection();
    }
}