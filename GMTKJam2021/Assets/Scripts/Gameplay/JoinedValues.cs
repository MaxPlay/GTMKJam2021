using UnityEngine;

namespace GMTK2021.Gameplay
{
    public class JoinedValues
    {
        private int a;
        private int b;

        private int maxA;
        private int maxB;

        public int A => a;
        public int B => b;
        public int MaxA => maxA;
        public int MaxB => maxB;

        public int TotalValue { get; }

        public int CurrentTotal => A + B;

        public void AddToA(int amount)
        {
            int targetAmount = GetMaximumPossibleChangeAmount(amount);
            if (a + targetAmount > maxA)
                a = maxA;
            else
                a += GetMaximumPossibleChangeAmount(amount);
        }

        public void AddToB(int amount)
        {
            int targetAmount = GetMaximumPossibleChangeAmount(amount);
            if (b + targetAmount > maxB)
                b = maxA;
            else
                b += GetMaximumPossibleChangeAmount(amount);
        }

        public void ShiftToA(int amount) 
        {
            ShiftTo(amount, ref b, ref a, ref maxB, ref maxA);
        }
        public void ShiftToB(int amount)
        {
            ShiftTo(amount, ref a, ref b, ref maxA, ref maxB);
        }

        private void ShiftTo(int amount, ref int from, ref int to, ref int fromMax, ref int toMax)
        {
            if (from < amount)
                amount = from;
            from -= amount;
            if (fromMax < amount)
                amount = from;
            fromMax -= amount;
            toMax += amount;
            to += amount;
        }

        private int GetMaximumPossibleChangeAmount(int amount)
        {
            int difference = TotalValue - CurrentTotal;
            if (difference < amount)
                amount = difference;
            return amount;
        }

        public JoinedValues(int totalValue, int startA, int startB, int startMaxA, int startMaxB)
        {
            TotalValue = totalValue;
            maxA = startMaxA;
            maxB = startMaxB;
            a = startA;
            b = startB;
            Debug.Assert(a + b <= totalValue, "The start values for the joined values may not exceed the total value.");
        }
    }
}