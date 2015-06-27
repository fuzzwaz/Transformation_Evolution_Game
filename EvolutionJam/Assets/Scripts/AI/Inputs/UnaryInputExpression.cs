using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.Inputs
{
   public abstract class UnaryInputExpression : IInputExpression
   {
      public UnaryInputExpression(IInputExpression inner)
      {
         Inner = inner;
      }

      public IInputExpression Inner
      {
         get;

         private set;
      }

      public abstract void Accept(IInputExpressionVisitor inputExpressionVisitor);
   }
}
