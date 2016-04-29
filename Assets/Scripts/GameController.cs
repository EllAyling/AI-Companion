using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    static public GameController gameController;

    Canvas mainCanvas;
    Player player;

    public Enemy[] enemies;

    void Awake()
    {
        gameController = this;
        mainCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        mainCanvas.gameObject.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        enemies = FindObjectsOfType(typeof(Enemy)) as Enemy[];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) mainCanvas.gameObject.SetActive(!mainCanvas.gameObject.activeSelf);

        if (mainCanvas.gameObject.activeSelf)
        {
            player.playerControls.mouseLook.SetCursorLock(false);
            player.playerControls.enabled = false;
        }
        else
        {
            player.playerControls.enabled = true;
            player.playerControls.mouseLook.SetCursorLock(true);
        }
    }
}
