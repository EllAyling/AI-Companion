using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            SceneManager.LoadScene("Win");
        }
    }
}
