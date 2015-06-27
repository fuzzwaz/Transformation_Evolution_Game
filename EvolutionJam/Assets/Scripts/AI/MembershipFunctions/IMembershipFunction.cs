using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.MembershipFunctions
{
   public interface IMembershipFunction
   {
      double CalculateMembership(int input);

      KeyValuePair<int, int> GetRangeForMembership(double membership);
   }
}
