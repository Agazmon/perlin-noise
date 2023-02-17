using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SceneGenerator : MonoBehaviour
{
    [Header("Player Generation Settings")]
    public GameObject jugador;

    [Header("Enemy Generation Settings")]
    public GameObject samuraiEnemigo;
    public int maxEnemigos;
    private NavMeshSurface surface;

    [Header("Scenario Generation Settings")]
    public GameObject piece;
    public int maxTotalPieces;
    public int maxPiecesX;
    public int maxPiecesZ;

    // Start is called before the first frame update
    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        GenerarMapaPerlin();
    }

    public void GenerarNavMesh()
    {
        surface.BuildNavMesh();
        GenerarJugador();
        GenerarEnemigos();
    }
    private void GenerarMapaPerlin()
    { 
        float semilla = Random.Range(0.0f, 100.0f);
        float escala = 4.00f;
        int alturaMax = 60;

        for (int i = 0; i < maxPiecesX; i++)
        {
            for (int j = 0; j < maxPiecesZ; j++)
            {
                float coorX = semilla + i / escala;
                float coorZ = semilla + j / escala;

                int altura = (int)(Mathf.PerlinNoise(coorX, coorZ) * alturaMax);
                if(altura < 30)
                    Instantiate(piece, new Vector3(i * 5, 0, j * 5), Quaternion.identity);
            }
        }
        Invoke("GenerarNavMesh", 6.0f);
    }
    private void GenerarJugador()
    {
        bool existeJugador = false;

        for( var i = 0; i<1000 && !existeJugador; i++)
        {
            if (i == 1000) break;
            int CoorX = Random.Range(0, (maxPiecesX * 5) + 1);
            int CoorZ = Random.Range(0, (maxPiecesZ * 5) + 1);

            Vector3 spawnRayo = new Vector3(CoorX, 6, CoorZ);

            if (Physics.Raycast(spawnRayo, Vector3.down, 10))
            {
                existeJugador = true;
                Instantiate(jugador, new Vector3(CoorX, 1.5f, CoorZ), Quaternion.identity);
            }

        }
    }
    private void GenerarEnemigos()
    {
        int enemigosGenerados = 0;

        for (var i = 0; i< 100 && enemigosGenerados < maxEnemigos  ; i++)
        {
            if (i == 1000) break;
            int CoorX = Random.Range(0, (maxPiecesX * 5) + 1);
            int CoorZ = Random.Range(0, (maxPiecesZ * 5) + 1);

            Vector3 spawnRayo = new Vector3(CoorX, 6, CoorZ);

            if (Physics.Raycast(spawnRayo, Vector3.down, 10))
            {
                enemigosGenerados++;
                Instantiate(samuraiEnemigo, new Vector3(CoorX, 1.5f, CoorZ), Quaternion.identity);
            }

        }
    }
}
