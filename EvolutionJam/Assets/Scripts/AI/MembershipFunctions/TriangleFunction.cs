using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.MembershipFunctions
{
   public class TriangleFunction : IMembershipFunction
   {
      private readonly PositiveLinearFunction _positiveLinearFunction;
      private readonly NegativeLinearFunction _negativeLinearFunction;

      public TriangleFunction(int minimum, int peak, int maximum)
      {
         _positiveLinearFunction = new PositiveLinearFunction(minimum, peak);
         _negativeLinearFunction = new NegativeLinearFunction(peak, maximum);
      }

      public int Minimum
      {
         get
         {
            return _positiveLinearFunction.Minimum;
         }
      }

      public int Peak
      {
         get
         {
            return _positiveLinearFunction.Maximum;
         }
      }

      public int Maximum
      {
         get
         {
            return _negativeLinearFunction.Maximum;
         }
      }

      public double CalculateMembership(int input)
      {
         if (input <= Peak)
         {
            return _positiveLinearFunction.CalculateMembership(input);
         }

         return _negativeLinearFunction.CalculateMembership(input);
      }


      public KeyValuePair<int, int> GetRangeForMembership(double membership)
      {
         if (membership.Equals(1))
         {
            return new KeyValuePair<int, int>(Peak, Peak);
         }

         var positiveRange = _positiveLinearFunction.GetRangeForMembership(membership);
         var negativeRange = _negativeLinearFunction.GetRangeForMembership(membership);
         return new KeyValuePair<int, int>(positiveRange.Key, negativeRange.Value);
      }
   }
}
