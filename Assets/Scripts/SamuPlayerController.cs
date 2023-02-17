using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SamuPlayerController: MonoBehaviour
{
    public int vida;
    public int velocidad;
    
    private GameObject espada;
    private GameObject mano;
    private GameObject sombrero;

    private bool atacando;
    private int sentido;

    private void Awake()
    {
        sombrero = transform.GetChild(0).transform.gameObject;
        espada = transform.GetChild(2).transform.gameObject;
        mano = transform.GetChild(3).transform.gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        atacando = false;
    }

    void Update()
    {
        sentido = 0;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, 1);
        }

        if (Input.GetKey(KeyCode.W))
        {
            sentido = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            sentido = -1;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (atacando == false)
            {
                atacando = true;
                StartCoroutine("atacar");

            }
        }

        transform.position = transform.position + transform.forward * sentido * Time.deltaTime * velocidad;
    }

    IEnumerator atacar()
    {
        int rotacion = 0;

        while(rotacion < 24)
        {
            yield return new WaitForFixedUpdate();
            espada.transform.RotateAround(mano.transform.position, Vector3.up * -1, 10);

            rotacion++;
        }

        while (rotacion > 0)
        {
            yield return new WaitForFixedUpdate();
            espada.transform.RotateAround(mano.transform.position, Vector3.up * -1, -10);

            rotacion--;
        }

        atacando = false;
    }

    public void quitarVida()
    {
        vida--;

        if (vida >= 7)
        {
            sombrero.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (vida < 7 && vida > 3)
        {
            sombrero.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            sombrero.GetComponent<Renderer>().material.color = Color.red;
        }

        if (vida == 0)
        {
            Destroy(this.gameObject);
        }
    }

}
