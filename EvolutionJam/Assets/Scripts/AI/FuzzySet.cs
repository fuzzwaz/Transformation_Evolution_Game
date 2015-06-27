using System;
using System.Collections.Generic;
using FuzzyEvolutions.MembershipFunctions;

namespace FuzzyEvolutions
{
   public class FuzzySet
   {
      private readonly IMembershipFunction _membershipFunction;

      public FuzzySet(string label, IMembershipFunction membershipFunction)
      {
         Label = label;
         _membershipFunction = membershipFunction;
      }

      public string Label
      {
         get;

         private set;
      }

      public double CalculateMembership(int input)
      {
         return _membershipFunction.CalculateMembership(input);
      }

      public int GetMembershipCenter(double membership)
      {
         var range = _membershipFunction.GetRangeForMembership(membership);

         return (range.Key + range.Value) / 2;
      }
   }
}
