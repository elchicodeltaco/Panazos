using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    // Hacemos que sea Singleton
    private static WaveManager instancia;
    public static WaveManager GetInstancia()
    {
        return instancia;
    }

    private void Awake()
    {
        if (instancia == null)  // la variable que hace referencia al manager está asignada?
            instancia = this;   // si no, asígnala

        else if (instancia != this)  // ya estaba asignada, pero es otro objeto que no es este
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);  // hacemos que al cambiar de escena, el manager se mantenga

    }

    [Header("Oleadas")]
    [SerializeField] private int currentWave;
    [SerializeField] private int TotalWaves;
    public Spawner spawn;
    
    private int zombiesTotales;
    private int zombiesRestantes;
    private int zombiesCreados;
    

    // Start is called before the first frame update
    void Start()
    {
        zombiesTotales = 4;
        zombiesRestantes = zombiesTotales;
        zombiesCreados = 0;

        currentWave = 1;
        GameManager.GetInstancia().CambiarDeEstadoEnJuego(GameManager.EstadosDeJuego.SinSpawners);
        GameManager.GetInstancia().UpdateZombiesOnScreen(zombiesTotales);
        GameManager.GetInstancia().UpdateWaveOnScreen(currentWave);
        TotalWaves = HowMuchWaves.GetInstancia().num;
        TotalWaves++;
        
    }


    public void ZombieNuevoCreado()
    {
        zombiesCreados++;
        if (zombiesCreados >= zombiesTotales)
        {
            GameManager.GetInstancia().CambiarDeEstadoEnJuego(GameManager.EstadosDeJuego.SinSpawners);
        }
    }

    public void ZombieAsesinadoMasacradoDestruidoXD()
    {
        zombiesRestantes--;
        GameManager.GetInstancia().UpdateZombiesOnScreen(zombiesRestantes);
        if (zombiesRestantes <= 0)
        {
            GameManager.GetInstancia().UpdateZombiesOnScreen(0);
            StartCoroutine(PausaParaLaSiguienteOleada());
        }else
            if (zombiesRestantes < 4)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Zombie");
            foreach(GameObject zombie in enemies)
            {
                //zombie.GetComponent<EnemyBehavior>().alertRange *= 3;

            }
        }
    }

    IEnumerator PausaParaLaSiguienteOleada()
    {
        GameManager.GetInstancia().CambiarDeEstadoEnJuego(GameManager.EstadosDeJuego.SinSpawners);
        print("OleadaTerminafda");
        yield return new WaitForSeconds(4);
        print("NUeva Oleada");
        NuevaOleada();
        GameManager.GetInstancia().CambiarDeEstadoEnJuego(GameManager.EstadosDeJuego.ConSpawners);
    }

    public void NuevaOleada()
    {
        currentWave++;
        if (currentWave >= TotalWaves)
        {
            StartCoroutine(GameManager.GetInstancia().Gane());
            return;
        }
        zombiesTotales = 2;
        zombiesTotales *= currentWave;
        int rand = Random.Range(0, 10);
        zombiesTotales += rand * (currentWave - 1);
        zombiesRestantes = zombiesTotales;
        zombiesCreados = 0;
        GameManager.GetInstancia().UpdateWaveOnScreen(currentWave);
        spawn.ReducirIntervalo(currentWave);
        PlayerShooter.GetInstancia().AddMoreAmmo();
        if(currentWave%5 == 0)
        PlayerDamage.GetInstancia().AddMoreHealth();
        GameManager.GetInstancia().UpdateZombiesOnScreen(zombiesRestantes);
    }

    public bool PuedoCrearNuevoZombie()
    {
        if (zombiesCreados >= zombiesTotales)
        {
            return false;
        }
        else
        return true;
    }
}
