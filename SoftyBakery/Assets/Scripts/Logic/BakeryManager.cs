using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DataModel.BakeOperation;
using GameBase;
using GameBase.FiniteStateMachine;
using GameLogic.Widge;
using Logic.VIew2D;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BakeryManager : SingletonBehaviour<BakeryManager>
{
    [SerializeField]
    BeatSequence2D beatSequence;
    [SerializeField]
    HandController handView;
    [SerializeField]
    MealController mealView;
    [SerializeField]
    ScoreShowController scoreShowController;
    [SerializeField]
    ToolSeleterUI toolSeleterUI;
    
    StateMachine fsm = new StateMachine();
    private int Score = 100;

    private int curOpIndex;
    private List<BakeOperationModel> bakeOperations = new List<BakeOperationModel>()
    {
        new MixingEggsModel(),
        new PoundingBiscuitsModel(),
        new CuttingAppleModel(),
    };
    
    private void Start()
    {
        curOpIndex = 0;
        beatSequence.OnBeatFalse = () =>
        {
            Score -= 20;
        };
        beatSequence.OnEndBeating = EndBakeStep;
        toolSeleterUI.OnEndSelect = StartBakeStep;
        SelectTool();
        
        /*var model = new MixingEggsModel();
        beatSequence.Read(model);
        var meal = Instantiate(Resources.Load<MealSequenceView>(model.PrefabName));
        mealView.SetMeal(meal);
        beatSequence.OnProgress = mealView.Hit;
        var handTool = Instantiate(Resources.Load<HandTool>("HandTool/BaseballBat"));
        handView.SetTool(handTool);
        beatSequence.OnProgress += handView.Hit;*/
    }

    public void StartBakeStep()
    {
        //var model = new MixingEggsModel();
        var model = bakeOperations[curOpIndex];
        beatSequence.Read(model);
        var meal = Instantiate(Resources.Load<MealSequenceView>(model.PrefabName));
        mealView.SetMeal(meal);
        beatSequence.OnProgress += mealView.Hit;
    }

    public void EndBakeStep()
    {
        beatSequence.OnProgress -= mealView.Hit;
        mealView.SetMeal(null);
        RemoveTool();

        curOpIndex++;
        if (curOpIndex >= bakeOperations.Count)
        {
            // show score
            ShowScoreAndQuit();
            /*scoreShowController.ShowScore(Score);
            //todo return main
            SceneManager.LoadScene("Login");
            SingletonSystem.Release(gameObject);*/
        }
        else
        {
            SelectTool();
        }
    }

    public void SelectTool()
    {
        toolSeleterUI.StartSelect();
    }

    public void SetHandTool(HandTool handTool)
    {
        var tool = Instantiate(handTool);
        handView.SetTool(tool);
        beatSequence.OnProgress += handView.Hit;
    }

    public void RemoveTool()
    {
        beatSequence.OnProgress -= handView.Hit;
        handView.SetTool(null);
    }

    async void ShowScoreAndQuit()
    {
        scoreShowController.GetComponent<Image>().enabled = true;
        await scoreShowController.ShowScore(Score);
        await UniTask.Delay(2000);
        scoreShowController.GetComponent<Image>().enabled = false;
        //todo return main
        SceneManager.LoadScene("Login");
        SingletonSystem.Release(gameObject);
    }
}
