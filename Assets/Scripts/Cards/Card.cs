using Types;

namespace Cards
{
    public struct Card
    {
        public Numbers Numbers;
        public Suits Suit;

        public Card(Numbers numbers, Suits suit)
        {
            Numbers = numbers;
            Suit = suit;
        }
    }
}