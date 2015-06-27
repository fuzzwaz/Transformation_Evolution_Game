using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.MembershipFunctions
{
   public class NegativeLinearFunction : IMembershipFunction
   {
      private readonly double _slope;
      private readonly double _yIntercept;

      public NegativeLinearFunction(int minimum, int maximum)
      {
         Minimum = minimum;
         Maximum = maximum;

         _slope = (double)-1 / (Maximum - Minimum);
         _yIntercept = 0 - _slope * Maximum;
      }

      public int Minimum
      {
         get;

         private set;
      }

      public int Maximum
      {
         get;

         private set;
      }

      public double CalculateMembership(int input)
      {
         if (input <= Minimum)
         {
            return 1;
         }

         if (input >= Maximum)
         {
            return 0;
         }

         return _slope * input + _yIntercept;
      }

      public KeyValuePair<int, int> GetRangeForMembership(double membership)
      {
         if (membership.Equals(1))
         {
            return new KeyValuePair<int, int>(Minimum, Minimum);
         }

         return new KeyValuePair<int, int>(Minimum, (int)((membership - _yIntercept) / _slope));
      }
   }
}
