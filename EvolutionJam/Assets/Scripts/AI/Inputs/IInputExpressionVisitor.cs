using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.Inputs
{
   public interface IInputExpressionVisitor
   {
      void Visit(FuzzyLiteral fuzzyLiteral);

      void Visit(FuzzyAnd fuzzyAnd);

      void Visit(FuzzyOr fuzzyOr);

      void Visit(FuzzyNot fuzzyNot);
   }
}
