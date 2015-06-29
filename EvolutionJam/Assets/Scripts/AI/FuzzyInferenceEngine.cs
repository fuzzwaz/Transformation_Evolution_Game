using UnityEngine;
using System;
using System.Collections.Generic;
using FuzzyEvolutions.MembershipFunctions;
using FuzzyEvolutions.Inputs;

namespace FuzzyEvolutions
{
   public class FuzzyInferenceEngine
   {
      private readonly IDictionary<int, Player> _players;
      private readonly IDictionary<string, int> _inputVariableValues;
      private readonly ICollection<FuzzyOutput> _outputs;
      private readonly ICollection<FuzzyRule> _rules;

      public FuzzyInferenceEngine()
      {
         _players = new Dictionary<int, Player>();
         _inputVariableValues = new Dictionary<string, int>();
         _outputs = new List<FuzzyOutput>();
         _rules = new List<FuzzyRule>();

         #region Input Setup

         // Begin DeathRange

         var setDeathRangeShortMembership = new NegativeLinearFunction(4, 10);
         var setDeathRangeShort = new FuzzySet(Labels.Set_DeathRange_Short, setDeathRangeShortMembership);

         var setDeathRangeMidMembership = new TriangleFunction(4, 10, 18);
         var setDeathRangeMid = new FuzzySet(Labels.Set_DeathRange_Mid, setDeathRangeMidMembership);

         var setDeathRangeLongMembership = new PositiveLinearFunction(10, 18);
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

         var setDashesLowMembership = new NegativeLinearFunction(2, 6);
         var setDashesLow = new FuzzySet(Labels.Set_Dashes_Low, setDashesLowMembership);

         var setDashesMediumMembership = new TriangleFunction(2, 6, 10);
         var setDashesMedium = new FuzzySet(Labels.Set_Dashes_Medium, setDashesMediumMembership);

         var setDashesHighMembership = new PositiveLinearFunction(6, 10);
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

         var setLengthOfLifeShortMembership = new NegativeLinearFunction(8, 18);
         var setLengthOfLifeShort = new FuzzySet(Labels.Set_LengthOfLife_Short, setLengthOfLifeShortMembership);

         var setLengthOfLifeMediumMembership = new TriangleFunction(8, 18, 26);
         var setLengthOfLifeMedium = new FuzzySet(Labels.Set_LengthOfLife_Medium, setLengthOfLifeMediumMembership);

         var setLengthOfLifeLongMembership = new PositiveLinearFunction(18, 26);
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

         var inputExpression1 = new FuzzyAnd(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Mid),
            new FuzzyLiteral(inputDashes, Labels.Set_Dashes_High));
         var outputExpression1 = new Dictionary<FuzzyOutput, string>();
         outputExpression1.Add(outputBlock, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression1, outputExpression1, (Player player) => {
            return !player.HasEvolution(Labels.Output_Block);
         }));

         var inputExpression2 = new FuzzyOr(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Long),
            new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Short));
         var outputExpression2 = new Dictionary<FuzzyOutput, string>();
         outputExpression2.Add(outputBlock, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression2, outputExpression2, (Player player) => {
            return !player.HasEvolution(Labels.Output_Block);
         }));

         var inputExpression3 = new FuzzyOr(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Short),
            new FuzzyLiteral(inputSurroundings, Labels.Set_Surroundings_Lots));
         var outputExpression3 = new Dictionary<FuzzyOutput, string>();
         outputExpression3.Add(outputBlock, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression3, outputExpression3, (Player player) => {
            return !player.HasEvolution(Labels.Output_Block);
         }));

         var inputExpression4 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_High),
            new FuzzyAnd(
               new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_None),
               new FuzzyOr(
                  new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Mid),
                  new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Long))));
         var outputExpression4 = new Dictionary<FuzzyOutput, string>();
         outputExpression4.Add(outputSeekingShot, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression4, outputExpression4, (Player player) => {
            return !player.HasEvolution(Labels.Output_SeekingShot);
         }));

         var inputExpression5 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Medium),
            new FuzzyAnd(
               new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Mid),
               new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Medium)));
         var outputExpression5 = new Dictionary<FuzzyOutput, string>();
         outputExpression5.Add(outputSeekingShot, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression5, outputExpression5, (Player player) => {
            return !player.HasEvolution(Labels.Output_SeekingShot);
         }));

         var inputExpression6 = new FuzzyOr(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Low),
            new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some));
         var outputExpression6 = new Dictionary<FuzzyOutput, string>();
         outputExpression6.Add(outputSeekingShot, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression6, outputExpression6, (Player player) => {
            return !player.HasEvolution(Labels.Output_SeekingShot);
         }));

         var inputExpression7 = new FuzzyAnd(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Short),
            new FuzzyLiteral(inputSurroundings, Labels.Set_Surroundings_None));
         var outputExpression7 = new Dictionary<FuzzyOutput, string>();
         outputExpression7.Add(outputPoisonGas, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression7, outputExpression7, (Player player) => {
            return !player.HasEvolution(Labels.Output_PoisonGas);
         }));

         var inputExpression8 = new FuzzyAnd(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Short),
            new FuzzyLiteral(inputSurroundings, Labels.Set_Surroundings_Some));
         var outputExpression8 = new Dictionary<FuzzyOutput, string>();
         outputExpression8.Add(outputPoisonGas, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression8, outputExpression8, (Player player) => {
            return !player.HasEvolution(Labels.Output_PoisonGas);
         }));

         var inputExpression9 = new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Short);
         var outputExpression9 = new Dictionary<FuzzyOutput, string>();
         outputExpression9.Add(outputPoisonGas, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression9, outputExpression9, (Player player) => {
            return !player.HasEvolution(Labels.Output_PoisonGas);
         }));

         var inputExpression10 = new FuzzyAnd(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Short),
            new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Short));
         var outputExpression10 = new Dictionary<FuzzyOutput, string>();
         outputExpression10.Add(outputSpikeOnBody, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression10, outputExpression10, (Player player) => {
            return !player.HasEvolution(Labels.Output_SpikeOnBody);
         }));

         var inputExpression11 = new FuzzyAnd(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Short),
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Medium));
         var outputExpression11 = new Dictionary<FuzzyOutput, string>();
         outputExpression11.Add(outputSpikeOnBody, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression11, outputExpression11));

         var inputExpression12 = new FuzzyNot(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Short));
         var outputExpression12 = new Dictionary<FuzzyOutput, string>();
         outputExpression12.Add(outputSpikeOnBody, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression12, outputExpression12));

         var inputExpression13 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_High),
            new FuzzyAnd(
               new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Mid),
               new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_None)));
         var outputExpression13 = new Dictionary<FuzzyOutput, string>();
         outputExpression13.Add(outputSpreadShot, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression13, outputExpression13, (Player player) => {
            return !player.HasEvolution(Labels.Output_SpreadShot);
         }));

         var inputExpression14 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_High),
            new FuzzyAnd(
               new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Short),
               new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_None)));
         var outputExpression14 = new Dictionary<FuzzyOutput, string>();
         outputExpression14.Add(outputSpreadShot, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression14, outputExpression14, (Player player) => {
            return !player.HasEvolution(Labels.Output_SpreadShot);
         }));

         var inputExpression15 = new FuzzyOr(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Low),
            new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some));
         var outputExpression15 = new Dictionary<FuzzyOutput, string>();
         outputExpression15.Add(outputSpreadShot, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression15, outputExpression15, (Player player) => {
            return !player.HasEvolution(Labels.Output_SpreadShot);
         }));

         var inputExpression16 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_High),
            new FuzzyAnd(
               new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Medium),
               new FuzzyAnd(
                  new FuzzyLiteral(inputSurroundings, Labels.Set_Surroundings_Some),
                  new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_None))));
         var outputExpression16 = new Dictionary<FuzzyOutput, string>();
         outputExpression16.Add(outputExplosiveShot, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression16, outputExpression16, (Player player) => {
            return !player.HasEvolution(Labels.Output_ExplosiveShot);
         }));

         var inputExpression17 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_High),
            new FuzzyAnd(
               new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Medium),
               new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_Some)));
         var outputExpression17 = new Dictionary<FuzzyOutput, string>();
         outputExpression17.Add(outputExplosiveShot, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression17, outputExpression17));

         var inputExpression18 = new FuzzyOr(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Low),
            new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some));
         var outputExpression18 = new Dictionary<FuzzyOutput, string>();
         outputExpression18.Add(outputExplosiveShot, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression18, outputExpression18));

         var inputExpression19 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_High),
            new FuzzyAnd(
               new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Medium),
               new FuzzyLiteral(inputSurroundings, Labels.Set_Surroundings_Some)));
         var outputExpression19 = new Dictionary<FuzzyOutput, string>();
         outputExpression19.Add(outputBouncingShot, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression19, outputExpression19, (Player player) => {
            return !player.HasEvolution(Labels.Output_BouncingShot);
         }));

         var inputExpression20 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Medium),
            new FuzzyAnd(
               new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Long),
               new FuzzyLiteral(inputDashes, Labels.Set_Dashes_Low)));
         var outputExpression20 = new Dictionary<FuzzyOutput, string>();
         outputExpression20.Add(outputBouncingShot, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression20, outputExpression20));

         var inputExpression21 = new FuzzyOr(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Low),
            new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some));
         var outputExpression21 = new Dictionary<FuzzyOutput, string>();
         outputExpression21.Add(outputBouncingShot, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression21, outputExpression21));

         var inputExpression22 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Medium),
            new FuzzyAnd(
               new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Medium),
               new FuzzyAnd(
                  new FuzzyLiteral(inputSurroundings, Labels.Set_Surroundings_Some),
                  new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Mid))));
         var outputExpression22 = new Dictionary<FuzzyOutput, string>();
         outputExpression22.Add(outputPiercingShot, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression22, outputExpression22, (Player player) => {
            return !player.HasEvolution(Labels.Output_PiercingShot);
         }));

         var inputExpression23 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Medium),
            new FuzzyAnd(
               new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Long),
               new FuzzyLiteral(inputDashes, Labels.Set_Dashes_Low)));
         var outputExpression23 = new Dictionary<FuzzyOutput, string>();
         outputExpression23.Add(outputPiercingShot, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression23, outputExpression23, (Player player) => {
            return !player.HasEvolution(Labels.Output_PiercingShot);
         }));

         var inputExpression24 = new FuzzyOr(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Low),
            new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some));
         var outputExpression24 = new Dictionary<FuzzyOutput, string>();
         outputExpression24.Add(outputPiercingShot, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression24, outputExpression24, (Player player) => {
            return !player.HasEvolution(Labels.Output_PiercingShot);
         }));

         var inputExpression25 = new FuzzyAnd(
            new FuzzyLiteral(inputDashes, Labels.Set_Dashes_High),
            new FuzzyAnd(
               new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Mid),
               new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_None)));
         var outputExpression25 = new Dictionary<FuzzyOutput, string>();
         outputExpression25.Add(outputLongerDash, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression25, outputExpression25, (Player player) => {
            return !player.HasEvolution(Labels.Output_LongerDash);
         }));

         var inputExpression26 = new FuzzyAnd(
            new FuzzyLiteral(inputDashes, Labels.Set_Dashes_High),
            new FuzzyAnd(
               new FuzzyLiteral(inputSurroundings, Labels.Set_Surroundings_Some),
               new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Long)));
         var outputExpression26 = new Dictionary<FuzzyOutput, string>();
         outputExpression26.Add(outputLongerDash, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression26, outputExpression26));

         var inputExpression27 = new FuzzyAnd(
            new FuzzyLiteral(inputDashes, Labels.Set_Dashes_Low),
            new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Long));
         var outputExpression27 = new Dictionary<FuzzyOutput, string>();
         outputExpression27.Add(outputLongerDash, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression27, outputExpression27));

         var inputExpression28 = new FuzzyAnd(
            new FuzzyLiteral(inputDashes, Labels.Set_Dashes_High),
            new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Medium));
         var outputExpression28 = new Dictionary<FuzzyOutput, string>();
         outputExpression28.Add(outputFasterDash, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression28, outputExpression28, (Player player) => {
            return !player.HasEvolution(Labels.Output_FasterDash);
         }));

         var inputExpression29 = new FuzzyAnd(
            new FuzzyLiteral(inputDashes, Labels.Set_Dashes_Medium),
            new FuzzyAnd(
               new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Medium),
               new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Long)));
         var outputExpression29 = new Dictionary<FuzzyOutput, string>();
         outputExpression29.Add(outputFasterDash, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression29, outputExpression29));

         var inputExpression30 = new FuzzyAnd(
            new FuzzyLiteral(inputDashes, Labels.Set_Dashes_Low),
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_High));
         var outputExpression30 = new Dictionary<FuzzyOutput, string>();
         outputExpression30.Add(outputFasterDash, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression30, outputExpression30));

         var inputExpression31 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_Some),
            new FuzzyAnd(
               new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_High),
               new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Long)));
         var outputExpression31 = new Dictionary<FuzzyOutput, string>();
         outputExpression31.Add(outputMoreAmmo, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression31, outputExpression31, (Player player) => {
            return !player.HasEvolution(Labels.Output_MoreAmmo);
         }));

         var inputExpression32 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_Some),
            new FuzzyAnd(
               new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_High),
               new FuzzyLiteral(inputDashes, Labels.Set_Dashes_Low)));
         var outputExpression32 = new Dictionary<FuzzyOutput, string>();
         outputExpression32.Add(outputMoreAmmo, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression32, outputExpression32));

         var inputExpression33 = new FuzzyAnd(
            new FuzzyLiteral(inputDashes, Labels.Set_Dashes_High),
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Low));
         var outputExpression33 = new Dictionary<FuzzyOutput, string>();
         outputExpression33.Add(outputMoreAmmo, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression33, outputExpression33));

         var inputExpression34 = new FuzzyAnd(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Long),
            new FuzzyAnd(
               new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_High),
               new FuzzyLiteral(inputDashes, Labels.Set_Dashes_High)));
         var outputExpression34 = new Dictionary<FuzzyOutput, string>();
         outputExpression34.Add(outputFasterBullets, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression34, outputExpression34, (Player player) => {
            return !player.HasEvolution(Labels.Output_FasterBullets);
         }));

         var inputExpression35 = new FuzzyAnd(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Long),
            new FuzzyAnd(
               new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Medium),
               new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Medium)));
         var outputExpression35 = new Dictionary<FuzzyOutput, string>();
         outputExpression35.Add(outputFasterBullets, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression35, outputExpression35));

         var inputExpression36 = new FuzzyOr(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Short),
            new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some));
         var outputExpression36 = new Dictionary<FuzzyOutput, string>();
         outputExpression36.Add(outputFasterBullets, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression36, outputExpression36));

         var inputExpression37 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_High),
            new FuzzyAnd(
               new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Medium),
               new FuzzyAnd(
                  new FuzzyLiteral(inputSurroundings, Labels.Set_Surroundings_Some),
                  new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_Some))));
         var outputExpression37 = new Dictionary<FuzzyOutput, string>();
         outputExpression37.Add(outputLargerBullets, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression37, outputExpression37, (Player player) => {
            return !player.HasEvolution(Labels.Output_LargerBullets);
         }));

         var inputExpression38 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Medium),
            new FuzzyAnd(
               new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Medium),
               new FuzzyAnd(
                  new FuzzyLiteral(inputSurroundings, Labels.Set_Surroundings_None),
                  new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_Some))));
         var outputExpression38 = new Dictionary<FuzzyOutput, string>();
         outputExpression38.Add(outputLargerBullets, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression38, outputExpression38));

         var inputExpression39 = new FuzzyAnd(
            new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some),
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Low));
         var outputExpression39 = new Dictionary<FuzzyOutput, string>();
         outputExpression39.Add(outputLargerBullets, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression39, outputExpression39));

         var inputExpression40 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Low),
            new FuzzyAnd(
               new FuzzyLiteral(inputDashes, Labels.Set_Dashes_High),
               new FuzzyAnd(
                  new FuzzyLiteral(inputSurroundings, Labels.Set_Surroundings_None),
                     new FuzzyAnd(
                        new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Mid),
                        new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some)))));
         var outputExpression40 = new Dictionary<FuzzyOutput, string>();
         outputExpression40.Add(outputGrowingDash, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression40, outputExpression40, (Player player) => {
            return !player.HasEvolution(Labels.Output_GrowingDash);
         }));

         var inputExpression41 = new FuzzyAnd(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Short),
            new FuzzyAnd(
               new FuzzyLiteral(inputSurroundings, Labels.Set_Surroundings_None),
               new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some)));
         var outputExpression41 = new Dictionary<FuzzyOutput, string>();
         outputExpression41.Add(outputGrowingDash, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression41, outputExpression41));

         var inputExpression42 = new FuzzyOr(
            new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_Some),
            new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_None));
         var outputExpression42 = new Dictionary<FuzzyOutput, string>();
         outputExpression42.Add(outputGrowingDash, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression42, outputExpression42));

         var inputExpression43 = new FuzzyAnd(
            new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some),
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Short));
         var outputExpression43 = new Dictionary<FuzzyOutput, string>();
         outputExpression43.Add(outputSpikesWhenDashing, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression43, outputExpression43, (Player player) => {
            return !player.HasEvolution(Labels.Output_SpikesWhenDashing);
         }));

         var inputExpression44 = new FuzzyAnd(
            new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some),
            new FuzzyLiteral(inputDashes, Labels.Set_Dashes_High));
         var outputExpression44 = new Dictionary<FuzzyOutput, string>();
         outputExpression44.Add(outputSpikesWhenDashing, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression44, outputExpression44, (Player player) => {
            return !player.HasEvolution(Labels.Output_SpikesWhenDashing);
         }));

         var inputExpression45 = new FuzzyNot(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Short));
         var outputExpression45 = new Dictionary<FuzzyOutput, string>();
         outputExpression45.Add(outputSpikesWhenDashing, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression45, outputExpression45, (Player player) => {
            return !player.HasEvolution(Labels.Output_SpikesWhenDashing);
         }));

         var inputExpression46 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_High),
            new FuzzyAnd(
               new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_None),
               new FuzzyOr(
                  new FuzzyLiteral(inputSurroundings, Labels.Set_Surroundings_Lots),
                  new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Mid))));
         var outputExpression46 = new Dictionary<FuzzyOutput, string>();
         outputExpression46.Add(outputBlackHoleShot, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression46, outputExpression46, (Player player) => {
            return !player.HasEvolution(Labels.Output_BlackHoleShot);
         }));

         var inputExpression47 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Medium),
            new FuzzyAnd(
               new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_None),
               new FuzzyLiteral(inputDashes, Labels.Set_Dashes_Low)));
         var outputExpression47 = new Dictionary<FuzzyOutput, string>();
         outputExpression47.Add(outputBlackHoleShot, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression47, outputExpression47));

         var inputExpression48 = new FuzzyOr(
            new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some),
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Low));
         var outputExpression48 = new Dictionary<FuzzyOutput, string>();
         outputExpression48.Add(outputBlackHoleShot, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression48, outputExpression48));

         var inputExpression49 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Medium),
            new FuzzyAnd(
               new FuzzyLiteral(inputBulletHits, Labels.Set_BulletHits_Some),
               new FuzzyAnd(
                  new FuzzyLiteral(inputDashes, Labels.Set_Dashes_Medium),
                  new FuzzyLiteral(inputDashHits, Labels.Set_DashHits_Some))));
         var outputExpression49 = new Dictionary<FuzzyOutput, string>();
         outputExpression49.Add(outputFasterMovement, Labels.Set_Helpful_High);
         _rules.Add(new FuzzyRule(inputExpression49, outputExpression49));

         var inputExpression50 = new FuzzyAnd(
            new FuzzyLiteral(inputBulletsShot, Labels.Set_BulletsShot_Medium),
            new FuzzyAnd(
               new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Mid),
               new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Short)));
         var outputExpression50 = new Dictionary<FuzzyOutput, string>();
         outputExpression50.Add(outputFasterMovement, Labels.Set_Helpful_Medium);
         _rules.Add(new FuzzyRule(inputExpression50, outputExpression50));

         var inputExpression51 = new FuzzyAnd(
            new FuzzyLiteral(inputDeathRange, Labels.Set_DeathRange_Long),
            new FuzzyLiteral(inputLengthOfLife, Labels.Set_LengthOfLife_Medium));
         var outputExpression51 = new Dictionary<FuzzyOutput, string>();
         outputExpression51.Add(outputFasterMovement, Labels.Set_Helpful_Low);
         _rules.Add(new FuzzyRule(inputExpression51, outputExpression51));

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

            if (!_players.ContainsKey(playerInformation.playerNum))
            {
               _players.Add(playerInformation.playerNum, new Player(playerInformation.playerNum));
            }

            MakeInputVariableValues(playerInformation);
            var selectionLabel = EvaluateRules(_players[playerInformation.playerNum]);

            /*Console.WriteLine("Player " + playerInformation.playerNum);
            Console.WriteLine("---------\n");

            foreach (var output in _outputs)
            {
               Console.WriteLine(output.Label + ": " + output.Centroid);
            }

            Console.WriteLine("\nSelection: " + selectionLabel);*/

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

      private string EvaluateRules(Player player)
      {
         foreach (var rule in _rules)
         {
            rule.Evaluate(_inputVariableValues, player);
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

         player.AddEvolution(selectedOutput.Label);

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
			GameObject GM = GameObject.Find ("GameManagerMaster");
         if (selectionLabel == Labels.Output_Block)
         {
            // Block selected.
            Debug.Log (playerNumber + ": Block Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_block = true;

         }
         else if (selectionLabel == Labels.Output_SeekingShot)
         {
            // SeekingShot selected.
            Debug.Log (playerNumber + ": Seeking Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_seeking = true;

         }
         else if (selectionLabel == Labels.Output_PoisonGas)
         {
            // PoisonGas selected.
            Debug.Log (playerNumber + ": Poision Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_poison = true;
         }
         else if (selectionLabel == Labels.Output_SpikeOnBody)
         {
            // SpikeOnBody selected.
            Debug.Log (playerNumber + ": Spike Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_spike = 1;
         }
         else if (selectionLabel == Labels.Output_SpreadShot)
         {
            // SpreadShot selected.
            Debug.Log (playerNumber + ": Spread Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_spread = true;
         }
         else if (selectionLabel == Labels.Output_ExplosiveShot)
         {
            // ExplosiveShot selected.
            Debug.Log (playerNumber + ": Explosive Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_explosive = GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_explosive += 1.0f;
         }
         else if (selectionLabel == Labels.Output_BouncingShot)
         {
            // BouncingShot selected.
            Debug.Log (playerNumber + ": Bounce Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_bouncing = GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_bouncing += 2;
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
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_piercing = true;
         }
         else if (selectionLabel == Labels.Output_LongerDash)
         {
            // LongerDash selected.
            Debug.Log (playerNumber + ": Longer Dash Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_longerDash = GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_longerDash += 0.5f;
         }
         else if (selectionLabel == Labels.Output_FasterDash)
         {
            // FasterDash selected.
            Debug.Log (playerNumber + ": Faster Dash Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_fasterDash = GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_fasterDash += 150.0f;
         }
         else if (selectionLabel == Labels.Output_MoreAmmo)
         {
            // MoreAmmo selected.
            Debug.Log (playerNumber + ": More Ammo Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_moreAmmo = GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_moreAmmo += 3;
         }
         else if (selectionLabel == Labels.Output_FasterBullets)
         {
            // FasterBullets selected.
            Debug.Log (playerNumber + ": Faster Bullets Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_fasterShot = GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_fasterShot += 150.0f;
         }
         else if (selectionLabel == Labels.Output_LargerBullets)
         {
            // LargerBullets selected.
            Debug.Log (playerNumber + ": Larger Bullets Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_largerShot = GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_largerShot += 1.0f;
         }
         else if (selectionLabel == Labels.Output_GrowingDash)
         {
            // GrowingDash selected.
            Debug.Log (playerNumber + ": Growing Dash Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_growingDash = GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_growingDash += 0.1f;
         }
         else if (selectionLabel == Labels.Output_SpikesWhenDashing)
         {
            // SpikesWhenDashing selected.
            Debug.Log (playerNumber + ": Spikes when Dashing Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_spikingDash = true;
         }
         else if (selectionLabel == Labels.Output_BlackHoleShot)
         {
            // BlackHoleShot selected.
            Debug.Log (playerNumber + ": BlackHole Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_gravity = GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_gravity += 10.0f;
         }
         else if (selectionLabel == Labels.Output_FasterMovement)
         {
            // FasterMovement selected.
            Debug.Log (playerNumber + ": Faster Movement Selected");
				GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_fasterMovement = GM.GetComponent<GameManager>().PlayerAbilities[playerNumber - 1].a_fasterMovement += 250.0f; 
         }
      }
   }
}
