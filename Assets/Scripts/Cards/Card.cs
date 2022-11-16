using Types;

namespace Cards
{
    public struct Card
    {
        public Numbers Number;
        public Suits Suit;

        public Card(Numbers number, Suits suit)
        {
            Number = number;
            Suit = suit;
        }
    }
}