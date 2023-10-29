using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] PosicionesSpawn;

    [SerializeField] private GameObject ObjetoASpawnear;
    [SerializeField] private float VelocidadDeCambio;
    [SerializeField] private float Intervalo;
    private float UltimoSpawn;


    // Start is called before the first frame update
    void Start()
    {
        //PosicionesSpawn = GetComponentsInChildren<Transform>();
        UltimoSpawn = Time.timeSinceLevelLoad;
        //StartCoroutine(Spawn());
        //StartCoroutine(ReducirIntervalo());
    }


    private void Update()
    {
        if (Time.timeSinceLevelLoad >= UltimoSpawn + Intervalo)
        {
            int rand = Random.Range(0, PosicionesSpawn.Length);

            if (WaveManager.GetInstancia().PuedoCrearNuevoZombie())
            {
                GameObject objeto = Instantiate(ObjetoASpawnear);
                objeto.transform.position = PosicionesSpawn[rand].position;
            }

            UltimoSpawn = Time.timeSinceLevelLoad;
        }
    }

    public void ReducirIntervalo(int wave)
    {
        switch (wave)
        {
            case 4:
            case 5: Intervalo = 1.5f; break;
            case 6: 
            case 7: Intervalo = 1f; break;
            case 8: 
            case 9: 
            case 10: Intervalo = 0.5f; break;
            default: Intervalo = 2f; break;
        }
    }
}
