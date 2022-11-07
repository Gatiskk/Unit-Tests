using Hierarchy.Exceptions;

namespace Hierarchy
{
    public class Meat : Food
    {
        public Meat(int quantity) : base(quantity)
        {
            if (quantity <= 0)
            {
                throw new NegativeFoodException();
            }
        }
    }
}