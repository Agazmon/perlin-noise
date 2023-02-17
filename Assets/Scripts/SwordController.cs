using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Espadazo a player");
            collision.gameObject.GetComponentInParent<SamuPlayerController>().quitarVida();
        }

        if (collision.gameObject.tag == "Enemigo")
        {
            Debug.Log("Espadazo a enemigo");
            collision.gameObject.GetComponentInParent<SamuraiController>().quitarVida();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Espadazo a player");
            other.gameObject.GetComponentInParent<SamuPlayerController>().quitarVida();
        }

        if (other.gameObject.tag == "Enemigo")
        {
            Debug.Log("Espadazo a enemigo");
            other.gameObject.GetComponentInParent<SamuraiController>().quitarVida();
        }
    }
}
