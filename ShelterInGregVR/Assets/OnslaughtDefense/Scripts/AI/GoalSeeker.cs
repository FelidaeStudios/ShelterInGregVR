using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalSeeker : MonoBehaviour
{
    private Seeker seeker;
    private Wanderer wanderer;

    public TMP_Text info;
    public TMP_Text task;
    //public TMP_Text tickLength;

    Goal[] mGoals;
    Action[] mActions;
    Action mChangeOverTime;
    const float TICK_LENGTH = 5.0f;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        wanderer = GetComponent<Wanderer>();

        mGoals = new Goal[4];
        mGoals[0] = new Goal("Eat", 4);
        mGoals[1] = new Goal("Sleep", 3);
        mGoals[2] = new Goal("Bathroom", 3);
        mGoals[3] = new Goal("Affection", 2);

        //Actions:
        //0: eat raw food
        //1: eat a snack
        //2: sleep in bed
        //3: sleep on sofa
        //4: drink soda
        //5: visit bathroom
        //6: bug player

        mActions = new Action[7];
        mActions[0] = new Action("feast upon the flesh of my enemies.");
        mActions[0].targetGoals.Add(new Goal("Eat", -3f));
        mActions[0].targetGoals.Add(new Goal("Sleep", +2f));
        mActions[0].targetGoals.Add(new Goal("Bathroom", +1f));
        mActions[0].targetGoals.Add(new Goal("Affection", -1f));
        mActions[0].player = false;

        mActions[1] = new Action("eat a pretzel.");
        mActions[1].targetGoals.Add(new Goal("Eat", -2f));
        mActions[1].targetGoals.Add(new Goal("Sleep", -1f));
        mActions[1].targetGoals.Add(new Goal("Bathroom", +1f));
        mActions[1].targetGoals.Add(new Goal("Affection", -1f));
        mActions[1].player = false;

        mActions[2] = new Action("bury myself in the sand.");
        mActions[2].targetGoals.Add(new Goal("Eat", +2f));
        mActions[2].targetGoals.Add(new Goal("Sleep", -4f));
        mActions[2].targetGoals.Add(new Goal("Bathroom", +2f));
        mActions[2].targetGoals.Add(new Goal("Affection", -2f));
        mActions[2].player = false;

        mActions[3] = new Action("sleep on map.");
        mActions[3].targetGoals.Add(new Goal("Eat", +1f));
        mActions[3].targetGoals.Add(new Goal("Sleep", -2f));
        mActions[3].targetGoals.Add(new Goal("Bathroom", +1f));
        mActions[3].targetGoals.Add(new Goal("Affection", +2f));
        mActions[3].player = false;

        mActions[4] = new Action("drink the blood of the fallen.");
        mActions[4].targetGoals.Add(new Goal("Eat", -1f));
        mActions[4].targetGoals.Add(new Goal("Sleep", -2f));
        mActions[4].targetGoals.Add(new Goal("Bathroom", +3f));
        mActions[4].targetGoals.Add(new Goal("Affection", -1f));
        mActions[4].player = false;

        mActions[5] = new Action("visit the little feesh room.");
        mActions[5].targetGoals.Add(new Goal("Eat", 0f));
        mActions[5].targetGoals.Add(new Goal("Sleep", 0f));
        mActions[5].targetGoals.Add(new Goal("Bathroom", -4f));
        mActions[5].targetGoals.Add(new Goal("Affection", 0f));
        mActions[5].player = false;

        mActions[6] = new Action("bug player for cuddles.");
        mActions[6].targetGoals.Add(new Goal("Eat", +1f));
        mActions[6].targetGoals.Add(new Goal("Sleep", 0f));
        mActions[6].targetGoals.Add(new Goal("Bathroom", 0f));
        mActions[6].targetGoals.Add(new Goal("Affection", -5f));
        mActions[6].player = true;

        mChangeOverTime = new Action("tick");
        mChangeOverTime.targetGoals.Add(new Goal("Eat", +4f));
        mChangeOverTime.targetGoals.Add(new Goal("Sleep", +1f));
        mChangeOverTime.targetGoals.Add(new Goal("Bathroom", +2f));
        mChangeOverTime.targetGoals.Add(new Goal("Affection", +3f));

        Debug.Log("Starting clock. One hour will pass every " + TICK_LENGTH + " seconds.");
        //tickLength.text = "One hour/tick is " + TICK_LENGTH + " seconds.";
        InvokeRepeating("Tick", 0f, TICK_LENGTH);

        Debug.Log("Hit E to do something.");
    }

    void Tick()
    {
        foreach (Goal goal in mGoals)
        {
            goal.value += mChangeOverTime.GetGoalChange(goal);

            goal.value = Mathf.Max(goal.value, 0);
        }
        task.text = "Greg wanders.";
        PrintGoals();
    }

    void PrintGoals()
    {
        string goalString = "";
        foreach (Goal goal in mGoals)
        {
            goalString += goal.name + ": " + goal.value + "; ";
        }
        goalString += "Discontentment: " + CurrentDiscontentment();
        Debug.Log(goalString);
        info.text = goalString;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Action bestAction = ChooseAction(mActions, mGoals);

            Debug.Log("I think I will " + bestAction.name);
            task.text = "I will now " + bestAction.name;

            foreach (Goal goal in mGoals)
            {
                goal.value += bestAction.GetGoalChange(goal);
                goal.value = Mathf.Max(goal.value, 0);
            }
            if (bestAction.player)
            {
                seeker.enabled = seeker.enabled;
                wanderer.enabled = !wanderer.enabled;
            }
            else
            {
                seeker.enabled = !seeker.enabled;
                wanderer.enabled = wanderer.enabled;
            }

            PrintGoals();
        }
    }

    Action ChooseAction(Action[] actions, Goal[] goals)
    {
        Action bestAction = null;
        float bestValue = float.PositiveInfinity;

        foreach (Action action in actions)
        {
            float thisValue = Discontentment(action, goals);
            if (thisValue < bestValue)
            {
                bestValue = thisValue;
                bestAction = action;
            }
        }

        return bestAction;
    }

    float Discontentment(Action action, Goal[] goals)
    {
        float discontentment = 0f;

        foreach (Goal goal in goals)
        {
            float newValue = goal.value + action.GetGoalChange(goal);
            newValue = Mathf.Max(newValue, 0);

            discontentment += goal.GetDiscontentment(newValue);
        }

        return discontentment;
    }

    float CurrentDiscontentment()
    {
        float total = 0f;
        foreach (Goal goal in mGoals)
        {
            total += (goal.value * goal.value);
        }
        return total;
    }
}