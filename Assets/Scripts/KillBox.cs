using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Entity>())
        {
            Destroy(other.gameObject);
        }
    }
}
