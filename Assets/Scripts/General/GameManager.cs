using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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
        if (instancia == null)  // la variable que hace referencia al manager está asignada?
            instancia = this;   // si no, asígnala

        else if (instancia != this)  // ya estaba asignada, pero es otro objeto que no es este
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);  // hacemos que al cambiar de escena, el manager se mantenga

    }
    [Header("UI Stuff")]
    [SerializeField] private float smoothDecreaseDuration = 0.5f;
    public TextMeshProUGUI ammoText;

    private int TotalHealth;
    [SerializeField] private Image Life;


    public TextMeshProUGUI ZombiesRestantesText;
    public TextMeshProUGUI currentWaveText;
    public TextMeshProUGUI NumeroDeOleadaAnimada;
    [SerializeField] private Animator LetrasAnimadas;
    [SerializeField] private Animator FadeInBlack;
    public GameObject FinalFade;

    private float scaleCanvas;

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
        TotalHealth = GameObject.Find("Player").GetComponent<PlayerDamage>().ToalHealth;

        Application.targetFrameRate = 60;
        Cursor.visible = false;
        GettingDamageUI();
        /*if(HowMuchWaves.GetInstancia().Toaster) 
        foreach(GameObject obj in ThingsToDestroy)
        {
            Destroy(obj);
        }*/

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

    public void GettingDamageUI()
    {
        TotalHealth = GameObject.Find("Player").GetComponent<PlayerDamage>().CurrentHealth;
        Vector3 escala = new Vector3();

        switch (TotalHealth)
        {
            case 0:
                if(TotalHealth == 0)
                {
                    escala = new Vector3(25, 14, 1);
                }
                break;

            case 1:
                if (TotalHealth == 1)
                {
                    escala = new Vector3(30, 21, 1);
                }
                break;
            case 2:
                if (TotalHealth == 2)
                {
                    escala = new Vector3(35, 29, 1);
                }
                break;
            case 3:
                if (TotalHealth == 3)
                {
                    escala = new Vector3(100, 100, 1);

                }
                break;
        }
        Life.rectTransform.localScale = Vector3.Lerp(Life.rectTransform.localScale, escala, 2.0f * Time.deltaTime);



    }


}
