using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.Inputs
{
   public abstract class BinaryInputExpression : IInputExpression
   {
      public BinaryInputExpression(IInputExpression left, IInputExpression right)
      {
         Left = left;
         Right = right;
      }

      public IInputExpression Left
      {
         get;

         private set;
      }

      public IInputExpression Right
      {
         get;

         private set;
      }

      public abstract void Accept(IInputExpressionVisitor inputExpressionVisitor);
   }
}
