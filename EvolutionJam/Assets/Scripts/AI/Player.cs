using System;
using System.Collections.Generic;

namespace FuzzyEvolutions
{
   public class Player
   {
      private readonly ICollection<string> _evolutionLabels;

      public Player(int playerNumber)
      {
         PlayerNumber = playerNumber;
         _evolutionLabels = new List<string>();
      }

      public int PlayerNumber
      {
         get;

         private set;
      }

      public void AddEvolution(string label)
      {
         _evolutionLabels.Add(label);
      }

      public bool HasEvolution(string label)
      {
         return _evolutionLabels.Contains(label);
      }
   }
}
