using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FuzzyEvolutions.MembershipFunctions;
using FuzzyEvolutions.Inputs;

namespace FuzzyEvolutions
{
	public class FuzzyInferenceEngine 
   {
      private readonly IDictionary<string, int> _inputVariableValues;
      private readonly ICollection<FuzzyOutput> _outputs;
      private readonly ICollection<FuzzyRule> _rules;

      public FuzzyInferenceEngine()
      {
         _inputVariableValues = new Dictionary<string, int>();
         _outputs = new List<FuzzyOutput>();
         _rules = new List<FuzzyRule>();



         #region Input Setup

         // Begin DeathRange

         var setDeathRangeShortMembership = new NegativeLinearFunction(11, 27);
         var setDeathRangeShort = new FuzzySet(Labels.Set_DeathRange_Short, setDeathRangeShortMembership);

         var setDeathRangeMidMembership = new TriangleFunction(11, 27, 53);
         var setDeathRangeMid = new FuzzySet(Labels.Set_DeathRange_Mid, setDeathRangeMidMembership);

         var setDeathRangeLongMembership = new PositiveLinearFunction(27, 53);
         var setDeathRangeLong = new FuzzySet(Labels.Set_DeathRange_Long, setDeathRangeLongMembership);

         var inputDeathRange = new InputVariable(Labels.InputVariable_DeathRange);
         inputDeathRange.AddSet(setDeathRangeShort);
         inputDeathRange.AddSet(setDeathRangeMid);
         inputDeathRange.AddSet(setDeathRangeLong);

         // End DeathRange

         // Begin BulletsShot

         var setBulletsShotLowMembership = new NegativeLinearFunction(3, 7);
         var setBulletsShotLow = new FuzzySet(Labels.Set_BulletsShot_Low, setBulletsShotLowMembership);

         var setBulletsShotMediumMembership = new TriangleFunction(3, 7, 11);
         var setBulletsShotMedium = new FuzzySet(Labels.Set_BulletsShot_Medium, setBulletsShotMediumMembership);

         var setBulletsShotHighMembership = new PositiveLinearFunction(7, 11);
         var setBulletsShotHigh = new FuzzySet(Labels.Set_BulletsShot_High, setBulletsShotHighMembership);

         var inputBulletsShot = new InputVariable(Labels.InputVariable_BulletsShot);
         inputBulletsShot.AddSet(setBulletsShotLow);
         inputBulletsShot.AddSet(setBulletsShotMedium);
         inputBulletsShot.AddSet(setBulletsShotHigh);

         // End BulletsShot

         // Begin Dashes

         var setDashesLowMembership = new NegativeLinearFunction(1, 3);
         var setDashesLow = new FuzzySet(Labels.Set_Dashes_Low, setDashesLowMembership);

         var setDashesMediumMembership = new TriangleFunction(1, 3, 5);
         var setDashesMedium = new FuzzySet(Labels.Set_Dashes_Medium, setDashesMediumMembership);

         var setDashesHighMembership = new PositiveLinearFunction(3, 5);
         var setDashesHigh = new FuzzySet(Labels.Set_Dashes_High, setDashesHighMembership);

         var inputDashes = new InputVariable(Labels.InputVariable_Dashes);
         inputDashes.AddSet(setDashesLow);
         inputDashes.AddSet(setDashesMedium);
         inputDashes.AddSet(setDashesHigh);

         // End Dashes

         // Begin BulletHits

         var setBulletHitsNoneMembership = new NegativeLinearFunction(0, 3);
         var setBulletHitsNone = new FuzzySet(Labels.Set_BulletHits_None, setBulletHitsNoneMembership);

         var setBulletHitsSomeMembership = new PositiveLinearFunction(0, 3);
         var setBulletHitsSome = new FuzzySet(Labels.Set_BulletHits_Some, setBulletHitsSomeMembership);

         var inputBulletHits = new InputVariable(Labels.InputVariable_BulletHits);
         inputBulletHits.AddSet(setBulletHitsNone);
         inputBulletHits.AddSet(setBulletHitsSome);

         // End BulletHits

         // Begin DashHits

         var setDashHitsNoneMembership = new NegativeLinearFunction(0, 3);
         var setDashHitsNone = new FuzzySet(Labels.Set_DashHits_None, setDashHitsNoneMembership);

         var setDashHitsSomeMembership = new PositiveLinearFunction(0, 3);
         var setDashHitsSome = new FuzzySet(Labels.Set_DashHits_Some, setDashHitsSomeMembership);

         var inputDashHits = new InputVariable(Labels.InputVariable_DashHits);
         inputDashHits.AddSet(setDashHitsNone);
         inputDashHits.AddSet(setDashHitsSome);

         // End DashHits

         // Begin Surroundings

         var setSurroundingsNoneMembership = new NegativeLinearFunction(4, 7);
         var setSurroundingsNone = new FuzzySet(Labels.Set_Surroundings_None, setSurroundingsNoneMembership);

         var setSurroundingsSomeMembership = new TriangleFunction(4, 7, 10);
         var setSurroundingsSome = new FuzzySet(Labels.Set_Surroundings_Some, setSurroundingsSomeMembership);

         var setSurroundingsLotsMembership = new PositiveLinearFunction(7, 10);
         var setSurroundingsLots = new FuzzySet(Labels.Set_Surroundings_Lots, setSurroundingsLotsMembership);

         var inputSurroundings = new InputVariable(Labels.InputVariable_Surroundings);
         inputSurroundings.AddSet(setSurroundingsNone);
         inputSurroundings.AddSet(setSurroundingsSome);
         inputSurroundings.AddSet(setSurroundingsLots);

         // End Surroundings

         // Begin LengthOfLife

         var setLengthOfLifeShortMembership = new NegativeLinearFunction(10, 25);
         var setLengthOfLifeShort = new FuzzySet(Labels.Set_LengthOfLife_Short, setLengthOfLifeShortMembership);

         var setLengthOfLifeMediumMembership = new TriangleFunction(10, 25, 40);
         var setLengthOfLifeMedium = new FuzzySet(Labels.Set_LengthOfLife_Medium, setLengthOfLifeMediumMembership);

         var setLengthOfLifeLongMembership = new PositiveLinearFunction(25, 40);
         var setLengthOfLifeLong = new FuzzySet(Labels.Set_LengthOfLife_Long, setLengthOfLifeLongMembership);

         var inputLengthOfLife = new InputVariable(Labels.InputVariable_LengthOfLife);
         inputLengthOfLife.AddSet(setLengthOfLifeShort);
         inputLengthOfLife.AddSet(setLengthOfLifeMedium);
         inputLengthOfLife.AddSet(setLengthOfLifeLong);

         // End LengthOfLife

         #endregion

         #region Output Setup

         var outputLowMembership = new NegativeLinearFunction(0, 50);
         var outputSetLow = new FuzzySet(Labels.Set_Helpful_Low, outputLowMembership);

         var outputMediumMembership = new TriangleFunction(0, 50, 100);
         var outputSetMedium = new FuzzySet(Labels.Set_Helpful_Medium, outputMediumMembership);

         var outputHighMembership = new PositiveLinearFunction(50, 100);
         var outputSetHigh = new FuzzySet(Labels.Set_Helpful_High, outputHighMembership);

         var outputBlock = new FuzzyOutput(Labels.Output_Block);
         outputBlock.AddSet(outputSetLow);
         outputBlock.AddSet(outputSetMedium);
         outputBlock.AddSet(outputSetHigh);
         _outputs.Add(outputBlock);

         var outputSeekingShot = new FuzzyOutput(Labels.Output_SeekingShot);
         outputSeekingShot.AddSet(outputSetLow);
         outputSeekingShot.AddSet(outputSetMedium);
         outputSeekingShot.AddSet(outputSetHigh);
         _outputs.Add(outputSeekingShot);

         var outputPoisonGas = new FuzzyOutput(Labels.Output_PoisonGas);
         outputPoisonGas.AddSet(outputSetLow);
         outputPoisonGas.AddSet(outputSetMedium);
         outputPoisonGas.AddSet(outputSetHigh);
         _outputs.Add(outputPoisonGas);

         var outputSpikeOnBody = new FuzzyOutput(Labels.Output_SpikeOnBody);
         outputSpikeOnBody.AddSet(outputSetLow);
         outputSpikeOnBody.AddSet(outputSetMedium);
         outputSpikeOnBody.AddSet(outputSetHigh);
         _outputs.Add(outputSpikeOnBody);

         var outputSpreadShot = new FuzzyOutput(Labels.Output_SpreadShot);
         outputSpreadShot.AddSet(outputSetLow);
         outputSpreadShot.AddSet(outputSetMedium);
         outputSpreadShot.AddSet(outputSetHigh);
         _outputs.Add(outputSpreadShot);

         var outputExplosiveShot = new FuzzyOutput(Labels.Output_ExplosiveShot);
         outputExplosiveShot.AddSet(outputSetLow);
         outputExplosiveShot.AddSet(outputSetMedium);
         outputExplosiveShot.AddSet(outputSetHigh);
         _outputs.Add(outputExplosiveShot);

         var outputBouncingShot = new FuzzyOutput(Labels.Output_BouncingShot);
         outputBouncingShot.AddSet(outputSetLow);
         outputBouncingShot.AddSet(outputSetMedium);
         outputBouncingShot.AddSet(outputSetHigh);
         _outputs.Add(outputBouncingShot);

         var outputBlink = new FuzzyOutput(Labels.Output_Blink);
         outputBlink.AddSet(outputSetLow);
         outputBlink.AddSet(outputSetMedium);
         outputBlink.AddSet(outputSetHigh);
         _outputs.Add(outputBlink);

         var outputPiercingShot = new FuzzyOutput(Labels.Output_PiercingShot);
         outputPiercingShot.AddSet(outputSetLow);
         outputPiercingShot.AddSet(outputSetMedium);
         outputPiercingShot.AddSet(outputSetHigh);
         _outputs.Add(outputPiercingShot);

         var outputLongerDash = new FuzzyOutput(Labels.Output_LongerDash);
         outputLongerDash.AddSet(outputSetLow);
         outputLongerDash.AddSet(outputSetMedium);
         outputLongerDash.AddSet(outputSetHigh);
         _outputs.Add(outputLongerDash);

         var outputFasterDash = new FuzzyOutput(Labels.Output_FasterDash);
         outputFasterDash.AddSet(outputSetLow);
         outputFasterDash.AddSet(outputSetMedium);
         outputFasterDash.AddSet(outputSetHigh);
         _outputs.Add(outputFasterDash);

         var outputMoreAmmo = new FuzzyOutput(Labels.Output_MoreAmmo);
         outputMoreAmmo.AddSet(outputSetLow);
         outputMoreAmmo.AddSet(outputSetMedium);
         outputMoreAmmo.AddSet(outputSetHigh);
         _outputs.Add(outputMoreAmmo);

         var outputFasterBullets = new FuzzyOutput(Labels.Output_FasterBullets);
         outputFasterBullets.AddSet(outputSetLow);
         outputFasterBullets.AddSet(outputSetMedium);
         outputFasterBullets.AddSet(outputSetHigh);
         _outputs.Add(outputFasterBullets);

         var outputLargerBullets = new FuzzyOutput(Labels.Output_LargerBullets);
         outputLargerBullets.AddSet(outputSetLow);
         outputLargerBullets.AddSet(outputSetMedium);
         outputLargerBullets.AddSet(outputSetHigh);
         _outputs.Add(outputLargerBullets);

         var outputGrowingDash = new FuzzyOutput(Labels.Output_GrowingDash);
         outputGrowingDash.AddSet(outputSetLow);
         outputGrowingDash.AddSet(outputSetMedium);
         outputGrowingDash.AddSet(outputSetHigh);
         _outputs.Add(outputGrowingDash);

         var outputSpikesWhenDashing = new FuzzyOutput(Labels.Output_SpikesWhenDashing);
         outputSpikesWhenDashing.AddSet(outputSetLow);
         outputSpikesWhenDashing.AddSet(outputSetMedium);
         outputSpikesWhenDashing.AddSet(outputSetHigh);
         _outputs.Add(outputSpikesWhenDashing);

         var outputBlackHoleShot = new FuzzyOutput(Labels.Output_BlackHoleShot);
         outputBlackHoleShot.AddSet(outputSetLow);
         outputBlackHoleShot.AddSet(outputSetMedium);
         outputBlackHoleShot.AddSet(outputSetHigh);
         _outputs.Add(outputBlackHoleShot);

         var outputFasterMovement = new FuzzyOutput(Labels.Output_FasterMovement);
         outputFasterMovement.AddSet(outputSetLow);
         outputFasterMovement.AddSet(outputSetMedium);
         outputFasterMovement.AddSet(outputSetHigh);
         _outputs.Add(outputFasterMovement);

         #endregion

         #region Rules

         var inputExpression1 = new FuzzyLiteral(inputDashes, Labels.Set_Dashes_Medium);
         var outputExpression1 = new Dictionary<FuzzyOutput, string>();
         outputExpression1.Add(outputSpikeOnBody, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression1, outputExpression1));

         var inputExpression2 = new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Medium);
         var outputExpression2 = new Dictionary<FuzzyOutput, string>();
         outputExpression2.Add(outputSpreadShot, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression2, outputExpression2, () => {
            return true;
         }));

         #endregion
      }

      public void RunWithInputs(playerInfo[] playerInformations)
      {

         foreach (var playerInformation in playerInformations)
         {
            if (!playerInformation.playerDied)
            {
               continue;
            }

            MakeInputVariableValues(playerInformation);
            var selectionLabel = EvaluateRules();


            _inputVariableValues.Clear();
            ResetAllOutputs();

            SendSelection(playerInformation.playerNum, selectionLabel);
         }
      }

      private void MakeInputVariableValues(playerInfo playerInformation)
      {
         _inputVariableValues.Add(Labels.InputVariable_DeathRange, (int)playerInformation.deathRange);
         _inputVariableValues.Add(Labels.InputVariable_BulletsShot, playerInformation.bulletsShot);
         _inputVariableValues.Add(Labels.InputVariable_Dashes, playerInformation.dashesMade);
         _inputVariableValues.Add(Labels.InputVariable_BulletHits, playerInformation.bulletHits);
         _inputVariableValues.Add(Labels.InputVariable_DashHits, playerInformation.dashingHits);
         _inputVariableValues.Add(Labels.InputVariable_Surroundings, playerInformation.surroundingObjects);
         _inputVariableValues.Add(Labels.InputVariable_LengthOfLife, (int)playerInformation.lengthOfLife);
      }

      private string EvaluateRules()
      {
         foreach (var rule in _rules)
         {
            rule.Evaluate(_inputVariableValues);
         }

         FuzzyOutput selectedOutput = null;
         foreach (var output in _outputs)
         {
            output.EstimateCentroid();

            if (selectedOutput == null || selectedOutput.Centroid < output.Centroid)
            {
               selectedOutput = output;
            }
         }

         return selectedOutput.Label;
      }

      private void ResetAllOutputs()
      {
         foreach (var output in _outputs)
         {
            output.Reset();
         }
      }

      private void SendSelection(int playerNumber, string selectionLabel)
      {
         if (selectionLabel == Labels.Output_Block)
         {
            // Block selected.
				Debug.Log (playerNumber + ": Block Selected");
         }
         else if (selectionLabel == Labels.Output_SeekingShot)
         {
            // SeekingShot selected.
				Debug.Log (playerNumber + ": Seeking Selected");

         }
         else if (selectionLabel == Labels.Output_PoisonGas)
         {
            // PoisonGas selected.
				Debug.Log (playerNumber + ": Poision Selected");
         }
         else if (selectionLabel == Labels.Output_SpikeOnBody)
         {
            // SpikeOnBody selected.
				Debug.Log (playerNumber + ": Spike Selected");
         }
         else if (selectionLabel == Labels.Output_SpreadShot)
         {
            // SpreadShot selected.
				Debug.Log (playerNumber + ": Spread Selected");
         }
         else if (selectionLabel == Labels.Output_ExplosiveShot)
         {
            // ExplosiveShot selected.
				Debug.Log (playerNumber + ": Explosive Selected");
         }
         else if (selectionLabel == Labels.Output_BouncingShot)
         {
            // BouncingShot selected.
				Debug.Log (playerNumber + ": Bounce Selected");
         }
         else if (selectionLabel == Labels.Output_Blink)
         {
            // Blink selected.
				Debug.Log (playerNumber + ": Blink Selected");
         }
         else if (selectionLabel == Labels.Output_PiercingShot)
         {
            // PiercingShot selected.
				Debug.Log (playerNumber + ": Piercing Selected");
         }
         else if (selectionLabel == Labels.Output_LongerDash)
         {
            // LongerDash selected.
				Debug.Log (playerNumber + ": Longer Dash Selected");
         }
         else if (selectionLabel == Labels.Output_FasterDash)
         {
            // FasterDash selected.
				Debug.Log (playerNumber + ": Faster Dash Selected");
         }
         else if (selectionLabel == Labels.Output_MoreAmmo)
         {
            // MoreAmmo selected.
				Debug.Log (playerNumber + ": More Ammo Selected");
         }
         else if (selectionLabel == Labels.Output_FasterBullets)
         {
            // FasterBullets selected.
				Debug.Log (playerNumber + ": Faster Bullets Selected");
         }
         else if (selectionLabel == Labels.Output_LargerBullets)
         {
            // LargerBullets selected.
				Debug.Log (playerNumber + ": Larger Bullets Selected");
         }
         else if (selectionLabel == Labels.Output_GrowingDash)
         {
            // GrowingDash selected.
				Debug.Log (playerNumber + ": Growing Dash Selected");
         }
         else if (selectionLabel == Labels.Output_SpikesWhenDashing)
         {
            // SpikesWhenDashing selected.
				Debug.Log (playerNumber + ": Spikes when Dashing Selected");
         }
         else if (selectionLabel == Labels.Output_BlackHoleShot)
         {
            // BlackHoleShot selected.
				Debug.Log (playerNumber + ": BlackHole Selected");
         }
         else if (selectionLabel == Labels.Output_FasterMovement)
         {
            // FasterMovement selected.
				Debug.Log (playerNumber + ": Faster Movement Selected");
         }
      }
   }
}
