using UnityEngine;
using System.Collections;

public class Enemy : Entity {

    // Use this for initialization
    void Start () {
        EntityType type = EntityType.Enemy | EntityType.Normal;
        AddType(type);
        speed = 2;
    }

    void Update()
    {
        
    }
}
