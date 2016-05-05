using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckUI : MonoBehaviour {

    public Toggle aggressive;
    public Toggle defensive;
    public Toggle passive;

    public Toggle stayClose;
    public Toggle keepDistance;

    public Toggle overwatch;
    public Toggle flank;

    public Button scavenge;

    public Button follow;

    public Companion companion;


	// Use this for initialization
	void Start () {
        aggressive.isOn = true;
        stayClose.isOn = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (companion.currentAction == CompanionAction.FOLLOW)
        {
            flank.isOn = false;
        }

        if (aggressive.isOn)
        {
            companion.currentStance = Stance.AGGRESSIVE;
        }
        else if (defensive.isOn)
        {
            companion.currentStance = Stance.DEFENSIVE;
        }
        else if (passive.isOn)
        {
            companion.currentStance = Stance.PASSIVE;
        }

        if (overwatch.isOn)
        {
            companion.currentAction = CompanionAction.OVERWATCH;
            follow.image.color = Color.white;
            scavenge.image.color = Color.white;
        }
        else if (flank.isOn)
        {
            companion.currentAction = CompanionAction.FLANK;
            follow.image.color = Color.white;
            scavenge.image.color = Color.white;
        }

        if (companion.currentAction == CompanionAction.FOLLOW)
        {
            if (stayClose.isOn)
            {
                companion.FollowState = FollowState.CLOSE;
            }
            else
            {
                companion.FollowState = FollowState.FAR;
            }
        }       
	}

    public void onFollowButton()
    {
        companion.StopPath();
        companion.blackboard.SetValue("target", Vector3.zero);
        companion.ChangeAction(CompanionAction.FOLLOW);
        overwatch.isOn = false;
        flank.isOn = false;
        follow.image.color = Color.green;
        scavenge.image.color = Color.white;
    }

    public void onScavengeButton()
    {
        companion.StopPath();
        companion.blackboard.SetValue("target", Vector3.zero);
        companion.ChangeAction(CompanionAction.SCAVENGE);
        overwatch.isOn = false;
        flank.isOn = false;
        scavenge.image.color = Color.green;
        follow.image.color = Color.white;
    }
}
