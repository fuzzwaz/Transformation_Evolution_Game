using System;
using System.Collections.Generic;

namespace FuzzyEvolutions.Inputs
{
   public class EvaluationVisitor : IInputExpressionVisitor
   {
      private Stack<double> _evaluationStack;
      private readonly IDictionary<string, int> _inputVariableValues;

      public EvaluationVisitor(IDictionary<string, int> inputVariableValues)
      {
         _evaluationStack = new Stack<double>();
         _inputVariableValues = inputVariableValues;
      }

      public double Result
      {
         get
         {
            return _evaluationStack.Peek();
         }
      }

      public void Visit(FuzzyLiteral fuzzyLiteral)
      {
         _evaluationStack.Push(fuzzyLiteral.Input.CalculateMembershipForSet(fuzzyLiteral.SetLabel, _inputVariableValues[fuzzyLiteral.Input.Label]));
      }

      public void Visit(FuzzyAnd fuzzyAnd)
      {
         var first = _evaluationStack.Pop();
         var second = _evaluationStack.Pop();

         _evaluationStack.Push(first * second);
      }

      public void Visit(FuzzyOr fuzzyOr)
      {
         var first = _evaluationStack.Pop();
         var second = _evaluationStack.Pop();

         _evaluationStack.Push(first + second - first * second);
      }

      public void Visit(FuzzyNot fuzzyNot)
      {
         var top = _evaluationStack.Pop();

         _evaluationStack.Push((double)1 - top);
      }
   }
}
