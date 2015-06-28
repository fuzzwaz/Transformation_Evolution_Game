using System;
using System.Collections.Generic;
using FuzzyEvolutions.Inputs;

namespace FuzzyEvolutions
{
   public class FuzzyRule
   {
      public delegate bool Constraint(Player player);

      private readonly IInputExpression _inputExpression;
      private readonly IDictionary<FuzzyOutput, string> _outputExpressions;
      private readonly Constraint _constraint;

      public FuzzyRule(IInputExpression inputExpression, IDictionary<FuzzyOutput, string> outputExpressions, Constraint constraint = null)
      {
         _inputExpression = inputExpression;
         _outputExpressions = outputExpressions;
         _constraint = constraint;
      }

      public void Evaluate(IDictionary<string, int> inputVariableValues, Player player)
      {
         if (_constraint != null && !_constraint(player))
         {
            return;
         }

         var evaluationVisitor = new EvaluationVisitor(inputVariableValues);

         _inputExpression.Accept(evaluationVisitor);

         foreach (var outputExpression in _outputExpressions)
         {
            outputExpression.Key.AddSetMembership(outputExpression.Value, evaluationVisitor.Result);
         }
      }
   }
}
