using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Hacemos que sea Singleton
    private static GameManager instancia;
    public static GameManager GetInstancia()
    {
        return instancia;
    }

    private void Awake()
    {
        if (instancia == null)  // la variable que hace referencia al manager est� asignada?
            instancia = this;   // si no, as�gnala

        else if (instancia != this)  // ya estaba asignada, pero es otro objeto que no es este
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);  // hacemos que al cambiar de escena, el manager se mantenga

    }
    [Header("UI Stiff")]
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ZombiesRestantesText;
    public TextMeshProUGUI currentWaveText;
    public TextMeshProUGUI NumeroDeOleadaAnimada;
    [SerializeField] private Animator LetrasAnimadas;
    [SerializeField] private Animator FadeInBlack;
    public GameObject FinalFade;

    [Header("Gameplay")]
    [SerializeField] private GameObject Spawners;
    public GameObject[] ThingsToDestroy;

    [Header("Escenas")]
    public int sigEscena;
    public int GameOverEscena;
    public enum EstadosDeJuego
    {
        SinSpawners = 0,
        ConSpawners = 1,
        Pausa,
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
        if(HowMuchWaves.GetInstancia().Toaster)
        foreach(GameObject obj in ThingsToDestroy)
        {
            Destroy(obj);
        }
    }


    public void CambiarDeEstadoEnJuego(EstadosDeJuego estado)
    {
        switch (estado)
        {
            case EstadosDeJuego.SinSpawners:
                Spawners.SetActive(false);
                break;
            case EstadosDeJuego.ConSpawners:
                Spawners.SetActive(true);
                break;
        }
    }

    public IEnumerator Gane()
    {
        FinalFade.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        CargarEscenaInt(sigEscena);
    }

    public IEnumerator GameOver()
    {
        FadeInBlack.SetTrigger("over");
        yield return new WaitForSeconds(2f);
        CargarEscenaInt(GameOverEscena);
    }

    public void CargarEscenaInt(int indice)
    {
        SceneManager.LoadScene(indice);
    }

    public void UpdateZombiesOnScreen(int amount)
    {
        ZombiesRestantesText.text = amount.ToString();
    }

    public void UpdateWaveOnScreen(int wave)
    {
        currentWaveText.text = wave.ToString();
        NumeroDeOleadaAnimada.text = wave.ToString();
        LetrasAnimadas.SetTrigger("mostrar");
    }

    public void UpdateHeathOnScreen(int healt)
    {
        healthText.text = healt.ToString();
        if (healt <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    public void UpdateAmmoOnScreen(int ammo)
    {
        ammoText.text = ammo.ToString();
    }
}
