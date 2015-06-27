using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.Inputs
{
   public class FuzzyLiteral : IInputExpression
   {
      public FuzzyLiteral(InputVariable input, string setLabel)
      {
         Input = input;
         SetLabel = setLabel;
      }

      public InputVariable Input
      {
         get;

         private set;
      }

      public string SetLabel
      {
         get;

         private set;
      }

      public void Accept(IInputExpressionVisitor inputExpressionVisitor)
      {
         inputExpressionVisitor.Visit(this);
      }
   }
}
