using UnityEngine;

namespace GMTK2021.Gameplay
{
    public class JoinedValues
    {
        private int a;
        private int b;

        public int A => a;
        public int B => b;

        public int TotalValue { get; }

        public int CurrentTotal => A + B;

        public void AddToA(int amount) => a += GetMaximumPossibleChangeAmount(amount);

        public void AddToB(int amount) => b += GetMaximumPossibleChangeAmount(amount);

        public void ShiftToA(int amount) => ShiftTo(amount, ref b, ref a);

        public void ShiftToB(int amount) => ShiftTo(amount, ref a, ref b);

        private void ShiftTo(int amount, ref int from, ref int to)
        {
            if (from < amount)
                amount = from;
            from -= amount;
            to += amount;
        }

        private int GetMaximumPossibleChangeAmount(int amount)
        {
            int difference = TotalValue - CurrentTotal;
            if (difference < amount)
                amount = difference;
            return amount;
        }

        public JoinedValues(int totalValue, int startA, int startB)
        {
            TotalValue = totalValue;
            a = startA;
            b = startB;
            Debug.Assert(a + b <= totalValue / 2, "The start values for the joined values may not exceed the total value.");
        }
    }
}