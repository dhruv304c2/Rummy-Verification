using Cards;
using TMPro;
using UnityEngine;

namespace Game
{
    public class MonoCard : MonoBehaviour
    {
        public Card cardData;
        private CardRenderData _renderData;
        public TextMeshProUGUI cardText_main;
        public TextMeshProUGUI cardText_Top;
        public TextMeshProUGUI cardText_Bottom;

        public void UpdateRenderData(Card card)
        {
            cardData = card;
            _renderData = new CardRenderData(cardData);

            cardText_main.text = _renderData.CardText;
            cardText_main.color = _renderData.Color;
            
            if (cardText_Top != null)
            {
                cardText_Top.text = _renderData.CardText;
                cardText_Top.color = _renderData.Color;
            }

            if (cardText_Bottom != null)
            {
                cardText_Bottom.text = _renderData.CardText;
                cardText_Bottom.color = _renderData.Color;
            }
        }
    }
}