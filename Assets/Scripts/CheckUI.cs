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
    public Button hide;

    public GameObject buttonGroup;

    public Player player;
    public Companion companion;

    public Text health;
    public Text ammo;
    public Text medkits;
    public Text buffed;

	// Use this for initialization
	void Start () {
        aggressive.isOn = true;
        stayClose.isOn = true;
    }
	
	// Update is called once per frame
	void Update () {

        CheckToggleStates();
        CheckPlayerInput();
        UpdateHealthAmmoCounters();

        if (player.buffed)
        {
            buffed.gameObject.SetActive(true);
        }
        else
        {
            buffed.gameObject.SetActive(false);
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
        hide.image.color = Color.white;
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
        hide.image.color = Color.white;
    }

    public void onHideButton()
    {
        companion.StopPath();
        companion.blackboard.SetValue("target", Vector3.zero);
        companion.ChangeAction(CompanionAction.HIDE);
        companion.hiding = true;
        overwatch.isOn = false;
        flank.isOn = false;
        hide.image.color = Color.green;
        scavenge.image.color = Color.white;
        follow.image.color = Color.white;
    }

    public void CheckToggleStates()
    {

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
        if (companion.currentAction != CompanionAction.FLANK)
        {
            flank.isOn = false;
        }
    }

    public void CheckPlayerInput()
    {
        if (Input.GetMouseButtonDown(1)) buttonGroup.SetActive(!buttonGroup.activeSelf);

        if (buttonGroup.activeSelf)
        {
            player.playerControls.mouseLook.SetCursorLock(false);
            player.playerControls.enabled = false;
            player.enabled = false;
        }
        else
        {
            player.enabled = true;
            player.playerControls.enabled = true;
            player.playerControls.mouseLook.SetCursorLock(true);
        }
    }

    public void UpdateHealthAmmoCounters()
    {
        health.text = "HEALTH: " + player.health;
        ammo.text = "AMMO: " + player.ammo;
        medkits.text = "MEDKITS: " + player.medKits;
    }
}
