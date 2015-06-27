using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.Inputs
{
   public interface IInputExpression
   {
      void Accept(IInputExpressionVisitor inputExpressionVisitor);
   }
}
