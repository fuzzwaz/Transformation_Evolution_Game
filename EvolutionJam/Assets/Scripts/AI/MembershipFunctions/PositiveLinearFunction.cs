using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.MembershipFunctions
{
   public class PositiveLinearFunction : IMembershipFunction
   {
      private readonly double _slope;
      private readonly double _yIntercept;

      public PositiveLinearFunction(int minimum, int maximum)
      {
         Minimum = minimum;
         Maximum = maximum;

         _slope = (double)1 / (Maximum - Minimum);
         _yIntercept = 0 - _slope * Minimum;
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
            return 0;
         }

         if (input >= Maximum)
         {
            return 1;
         }

         return _slope * input + _yIntercept;
      }

      public KeyValuePair<int, int> GetRangeForMembership(double membership)
      {
         if (membership.Equals(1))
         {
            return new KeyValuePair<int, int>(Maximum, Maximum);
         }

         return new KeyValuePair<int, int>((int)((membership - _yIntercept) / _slope), Maximum);
      }
   }
}
