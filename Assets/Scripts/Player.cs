using UnityEngine;
using System.Collections;

public class Player : Entity
{

    public bool inCombat;
    public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController playerControls;

    // Use this for initialization
    void Start () {
        EntityType type = EntityType.Player;
        AddType(type);

        playerControls = GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();

        inCombat = false;
    }
	
	// Update is called once per frame
	void Update () {

        CheckEnemyStates();

    }

    void CheckEnemyStates()
    {
        if (GameController.gameController.enemies.Length > 0)
        {
            foreach (EnemySM enemy in GameController.gameController.enemies)
            {
                if (enemy.statemachine.currentState == enemy.statemachine.chaseState)
                {
                    inCombat = true;
                }
                else
                {
                    inCombat = false;
                }
            }
        }
    }
}
