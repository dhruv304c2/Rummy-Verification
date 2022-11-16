using Cards;
using Types;
using UnityEngine;

namespace Game
{
    public class CardRenderData
    {
        public string CardText;
        public Color Color;

        public CardRenderData(Card card)
        {
            string text = "";
            switch (card.Numbers)
            {
                case Numbers.Ace:
                    text += "A";
                    break;
                case Numbers.Two:
                    text += "2";
                    break;
                case Numbers.Three:
                    text += "3";
                    break;
                case Numbers.Four:
                    text += "4";
                    break;
                case Numbers.Five:
                    text += "5";
                    break;
                case Numbers.Six:
                    text += "6";
                    break;
                case Numbers.Seven:
                    text += "7";
                    break;
                case Numbers.Eight:
                    text += "8";
                    break;
                case Numbers.Nine:
                    text += "9";
                    break;
                case Numbers.Ten:
                    text += "10";
                    break;
                case Numbers.Jack:
                    text += "J";
                    break;
                case Numbers.Queen:
                    text += "Q";
                    break;
                case Numbers.King:
                    text += "K";
                    break;
            }

            switch (card.Suit)
            {
                case Suits.Spades:
                    text += "♠";
                    Color = Color.black;
                    break;
                case Suits.Clubs:
                    text += "♣";
                    Color = Color.black;
                    break;
                case Suits.Diamonds:
                    text += "♦";
                    Color = Color.red;
                    break;
                case Suits.Hearts:
                    text += "♥";
                    Color = Color.red;
                    break;
            }

            CardText = text;
        }
    }
}