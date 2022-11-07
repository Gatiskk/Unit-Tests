using Hierarchy.Exceptions;

namespace Hierarchy
{
    public class Vegetable : Food
    {
        public Vegetable(int quantity) : base(quantity)
        {
            if (quantity <= 0)
            {
                throw new NegativeFoodException();
            }
        }
    }
}