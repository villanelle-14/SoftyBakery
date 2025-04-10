
using System;
using System.Collections.Generic;

namespace DataModel.BakeOperation
{
    public abstract class BakeOperationModel
    {
        public string PrefabName = String.Empty; 
        public List<BeatInput> InputSequence;
    }
    
    public class MixingEggsModel : BakeOperationModel
    {
        //public readonly string PrefabName = "MealSqe/MealMixedEgg"; 
        public MixingEggsModel()
        {
            PrefabName = "MealSqe/MealMixedEgg";
            InputSequence = new List<BeatInput>()
            {
                BeatInput.left, BeatInput.right,
                BeatInput.left, BeatInput.right,
                BeatInput.left, BeatInput.right,
            };
        }
    }
    public class CuttingAppleModel : BakeOperationModel
    {
        //public readonly string PrefabName = "MealSqe/MealApple"; 
        public CuttingAppleModel()
        {
            PrefabName = "MealSqe/MealApple";
            InputSequence = new List<BeatInput>()
            {
                BeatInput.right, BeatInput.left,
                BeatInput.right, BeatInput.left,
                BeatInput.right, BeatInput.left,
            };
        }
    }
    public class PoundingBiscuitsModel : BakeOperationModel
    {
        //public readonly string PrefabName = "MealSqe/MealBiscuit"; 
        public PoundingBiscuitsModel()
        {
            PrefabName = "MealSqe/MealBiscuit";
            InputSequence = new List<BeatInput>()
            {
                BeatInput.left, BeatInput.right,
                BeatInput.right, BeatInput.left,
                BeatInput.left, BeatInput.right,
            };
        }
    }

    public enum BeatInput 
    {
        left,
        right
    }
}