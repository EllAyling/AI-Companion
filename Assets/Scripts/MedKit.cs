using UnityEngine;
using System.Collections;

public class MedKit : MonoBehaviour {

    Animation floating;

    void Start()
    {
        floating = GetComponent<Animation>();
    }

    void Update()
    {
        if (!floating.isPlaying)
        {
            floating.Play();
        }
    }
	
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Companion"))
        {
            other.gameObject.GetComponent<Entity>().medKits += 1;
            if (other.gameObject.GetComponent<Entity>().seenMedKits.ContainsKey(gameObject.name))
            {
                Debug.Log(gameObject.name);
                other.gameObject.GetComponent<Entity>().seenMedKits.Remove(gameObject.name);
            }

            if (other.gameObject.GetComponent<Player>())
            {
                if (other.gameObject.GetComponent<Player>().companion.seenMedKits.ContainsKey(gameObject.name))
                {
                    other.gameObject.GetComponent<Player>().companion.seenMedKits.Remove(gameObject.name);
                }
            }
            Destroy(gameObject);
        }
    }
}
