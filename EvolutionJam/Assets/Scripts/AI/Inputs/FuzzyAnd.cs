using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.Inputs
{
   public class FuzzyAnd : BinaryInputExpression
   {
      public FuzzyAnd(IInputExpression left, IInputExpression right)
         : base(left, right)
      {
      }

      public override void Accept(IInputExpressionVisitor inputExpressionVisitor)
      {
         Left.Accept(inputExpressionVisitor);
         Right.Accept(inputExpressionVisitor);

         inputExpressionVisitor.Visit(this);
      }
   }
}
