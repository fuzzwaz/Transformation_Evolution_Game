using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.Inputs
{
   public class InputVariable
   {
      private readonly IDictionary<string, FuzzySet> _sets;

      public InputVariable(string label)
      {
         Label = label;
         _sets = new Dictionary<string, FuzzySet>();
      }

      public string Label
      {
         get;

         private set;
      }

      public void AddSet(FuzzySet set)
      {
         _sets.Add(set.Label, set);
      }

      public double CalculateMembershipForSet(string setLabel, int input)
      {
         return _sets[setLabel].CalculateMembership(input);
      }
   }
}
