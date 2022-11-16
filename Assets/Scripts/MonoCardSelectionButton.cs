using System;
using Cards;
using UnityEngine;

namespace Game
{
    public class MonoCardSelectionButton : MonoBehaviour
    {
        private MonoCard _card => GetComponent<MonoCard>();
        private ISelectionController _selectionController;

        public void AddSelectionController(ISelectionController selectionController)
        {
            _selectionController = selectionController;
        }

        private void OnMouseUp()
        {
            Select();
        }

        public void Select()
        {
            _selectionController.AddSelection(_card.cardData);
        }
    }
}