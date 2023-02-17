using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    private GameObject Norte;
    private GameObject Sur;
    private GameObject Este;
    private GameObject Oeste;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("QuitarParedes", 5);
    }

    private void QuitarParedes()
    {
        Norte = transform.GetChild(1).gameObject;
        Sur = transform.GetChild(0).gameObject;
        Este = transform.GetChild(2).gameObject;
        Oeste = transform.GetChild(3).gameObject;

        if (Physics.Raycast(transform.position, transform.forward, 5))
        {
            Destroy(Norte);
        }

        if (Physics.Raycast(transform.position, transform.forward * -1, 5))
        {
            Destroy(Sur);
        }

        if (Physics.Raycast(transform.position, transform.right, 5))
        {
            Destroy(Este);
        }

        if (Physics.Raycast(transform.position, transform.right * -1, 5))
        {
            Destroy(Oeste);
        }
    }

}
