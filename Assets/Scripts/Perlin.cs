using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perlin : MonoBehaviour
{
    public GameObject pieza;
    public int maxX;
    public int maxZ;

    // Start is called before the first frame update
    void Start()
    {
        float semilla = Random.Range(0.0f, 100.0f);
        float escala = 40.0f;
        int alturaMax = 60;

        for(int i=0;i< maxX; i++)
        {
            for(int j=0;j< maxZ; j++)
            {
                float coorX = semilla + i / escala;
                float coorZ = semilla + j / escala;

                int altura = (int)(Mathf.PerlinNoise(coorX, coorZ) * alturaMax);

                
                GameObject p = Instantiate(pieza, new Vector3(i*5, 0, j*5), Quaternion.identity);

                if(altura >= 35)
                {
                    p.GetComponent<Renderer>().material.color = Color.white;
                }

                
             }
        }
    }

}
