using UnityEngine;
using System.Collections;

public class Player : Entity
{

	// Use this for initialization
	void Start () {
        EntityType type = EntityType.Player;
        AddType(type);
    }
	
	// Update is called once per frame
	void Update () {


        if ((this.type & (int)EntityType.Player) != 0)  //Type code
        {
            
        }


    }
}
