using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.Inputs
{
   public class FuzzyNot : UnaryInputExpression
   {
      public FuzzyNot(IInputExpression inner)
         : base(inner)
      {
      }

      public override void Accept(IInputExpressionVisitor inputExpressionVisitor)
      {
         Inner.Accept(inputExpressionVisitor);

         inputExpressionVisitor.Visit(this);
      }
   }
}
