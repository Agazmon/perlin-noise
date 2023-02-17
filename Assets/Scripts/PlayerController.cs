using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent personaje;

    // Start is called before the first frame update
    void Start()
    {
        personaje = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit rayo;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayo))
            {
                personaje.SetDestination(rayo.point);
            }
        }
    }
}
