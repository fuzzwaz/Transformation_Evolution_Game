using System;
using System.Collections.Generic;

namespace FuzzyEvolutions
{
   public class FuzzyOutput
   {
      private readonly IDictionary<string, FuzzySet> _sets;
      private readonly ICollection<KeyValuePair<string, double>> _setMemberships;

      public FuzzyOutput(string label, bool isRepeatable)
      {
         Label = label;
         Repeatable = isRepeatable;
         _sets = new Dictionary<string, FuzzySet>();
         _setMemberships = new List<KeyValuePair<string, double>>();
         Centroid = 0;
      }

      public string Label
      {
         get;

         private set;
      }

      public bool Repeatable
      {
         get;

         private set;
      }

      public double Centroid
      {
         get;

         private set;
      }

      public void AddSet(FuzzySet set)
      {
         _sets.Add(set.Label, set);
      }

      public void AddSetMembership(string setLabel, double membership)
      {
         if (membership.Equals(0))
         {
            return;
         }

         _setMemberships.Add(new KeyValuePair<string, double>(setLabel, membership));
      }

      public void EstimateCentroid()
      {
         if (_setMemberships.Count == 0)
         {
            Centroid = 0;
            return;
         }

         var rootSumSquares = new Dictionary<string, double>();

         foreach (var pair in _setMemberships)
         {
            if (!rootSumSquares.ContainsKey(pair.Key))
            {
               rootSumSquares.Add(pair.Key, 0);
            }

            rootSumSquares[pair.Key] += pair.Value * pair.Value;
         }

         foreach (var set in _sets)
         {
            if (!rootSumSquares.ContainsKey(set.Key))
            {
               continue;
            }

            rootSumSquares[set.Key] = Math.Sqrt(rootSumSquares[set.Key]);
         }

         double numerator = 0;
         double denominator = 0;

         foreach (var record in rootSumSquares)
         {
            numerator += _sets[record.Key].GetMembershipCenter(record.Value) * record.Value;
            denominator += record.Value;
         }

         Centroid = numerator / denominator;
      }

      public void Reset()
      {
         _setMemberships.Clear();
         Centroid = 0;
      }
   }
}
