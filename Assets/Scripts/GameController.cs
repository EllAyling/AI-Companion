using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    static public GameController gameController;
    
    public Player player;

    public Entity[] entitiesInLevel;
    public LayerMask overWatchPosLayer;

    void Awake()
    {
        gameController = this;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        entitiesInLevel = FindObjectsOfType(typeof(Entity)) as Entity[];
    }

    void Start()
    {
    }

    void Update()
    {
        if (player == null)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }   
    }
}
