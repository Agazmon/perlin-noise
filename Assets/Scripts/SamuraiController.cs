using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SamuraiController : MonoBehaviour
{
    private NavMeshAgent nv;
    private Vector3 posicionPatrulla;
    private GameObject[] ojos;
    private GameObject espada;
    private GameObject mano;
   

    public int vida;
    public int searchRange = 10;

    private bool encontrado;
    private Vector3 posicionObjetivo;

    private void Awake()
    {
        ojos = new GameObject[9];
        ojos[0] = transform.GetChild(2).transform.GetChild(0).transform.gameObject;
        ojos[1] = transform.GetChild(2).transform.GetChild(1).transform.gameObject;
        ojos[2] = transform.GetChild(2).transform.GetChild(2).transform.gameObject;
        ojos[3] = transform.GetChild(2).transform.GetChild(3).transform.gameObject;
        ojos[4] = transform.GetChild(2).transform.GetChild(4).transform.gameObject;
        ojos[5] = transform.GetChild(2).transform.GetChild(5).transform.gameObject;
        ojos[6] = transform.GetChild(2).transform.GetChild(6).transform.gameObject;
        ojos[7] = transform.GetChild(2).transform.GetChild(7).transform.gameObject;
        ojos[8] = transform.GetChild(2).transform.GetChild(8).transform.gameObject;

        mano = transform.GetChild(3).transform.gameObject;
        espada = transform.GetChild(4).transform.gameObject;

        nv = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        encontrado = false;

        StartCoroutine("patrullar");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        encontrado = false;

        for (int i = 0; i < 9; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(ojos[i].transform.position, ojos[i].transform.forward, out hit, 20))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    encontrado = true;
                    posicionObjetivo = hit.point;

                    Debug.DrawRay(ojos[i].transform.position, ojos[i].transform.forward * hit.distance, Color.red);
                }
                else
                {
                    Debug.DrawRay(ojos[i].transform.position, ojos[i].transform.forward * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(ojos[i].transform.position, ojos[i].transform.forward * 20, Color.white);
            }
        }
    }

    IEnumerator patrullar()
    {
        Debug.Log("Entra en patrullar");

        do {
            posicionPatrulla = transform.position + new Vector3(Random.Range(-searchRange, searchRange), 0, Random.Range(-searchRange, searchRange));
            posicionPatrulla.y = 0;
            nv.SetDestination(posicionPatrulla);
            yield return new WaitForFixedUpdate();
        } while (!nv.hasPath);
        

        while (true)
        {
            yield return new WaitForFixedUpdate();

            if (Vector3.Distance(transform.position, posicionPatrulla) < 2.0f)
            {
                nv.ResetPath();
                StartCoroutine("buscar");
                break;
            }

            if (encontrado)
            {
                nv.ResetPath();
                StartCoroutine("perseguir");
                break;
            }

        }
    }

    IEnumerator buscar()
    {
        Debug.Log("Entra en buscar");

        yield return new WaitForSeconds(2.0f);

        int rotacion = 0;

        while (rotacion < 360)
        {
            yield return new WaitForFixedUpdate();

            transform.Rotate(Vector3.up, 1);
            rotacion++;

            if (encontrado)
            {
                nv.ResetPath();
                StartCoroutine("perseguir");
                break;
            }
        }

        if (encontrado == false)
        {
            StartCoroutine("patrullar");
        }
    }

    IEnumerator perseguir()
    {
        Debug.Log("Entra en perseguir");

        while (true)
        {
            yield return new WaitForSecondsRealtime(0.1f);

            if(encontrado)
            {
                nv.SetDestination(posicionObjetivo);

                if (Vector3.Distance(transform.position, posicionObjetivo) < 3.0f)
                {
                    nv.ResetPath();
                    StartCoroutine("atacar");
                    break;
                }
            }
            else
            {
                nv.ResetPath();
                StartCoroutine("buscar");
                break;
            }
        }
    }

    IEnumerator atacar()
    {
        Debug.Log("Entra en atacar");
        
        int rotacion = 0;

        while (rotacion < 24)
        {
            yield return new WaitForFixedUpdate();

            espada.transform.RotateAround(mano.transform.position, Vector3.up, -10);
            rotacion++;

        }

        rotacion = 0;
        while (rotacion < 24)
        {
            yield return new WaitForFixedUpdate();

            espada.transform.RotateAround(mano.transform.position, Vector3.up, 10);
            rotacion++;

        }

        yield return new WaitForSecondsRealtime(0.4f);
        StartCoroutine("buscar");
    }

    public void quitarVida()
    {
        vida--;

        if (vida == 0)
        {
            Destroy(this.gameObject);
        }
    }

}