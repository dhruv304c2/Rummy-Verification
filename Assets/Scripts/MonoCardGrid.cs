using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace Game
{
    public class MonoCardGrid: MonoBehaviour
    {
        public List<GameObject> CardCells = new List<GameObject>();
        public Transform grid;

        public void AddToGrid(MonoCard card)
        {
            var newHolder = new GameObject(card.name + "Root").AddComponent<RectTransform>();
            CardCells.Add(newHolder.gameObject);
            card.transform.SetParent(newHolder.transform);
            newHolder.transform.SetParent(grid);
        }

        public void ClearGrid()
        {
            foreach (var card in CardCells)
            {
                Destroy(card);
            }
            
            CardCells = new List<GameObject>();
        }
    }
}