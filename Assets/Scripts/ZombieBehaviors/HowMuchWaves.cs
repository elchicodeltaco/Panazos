using UnityEngine;
using TMPro;

public class HowMuchWaves : MonoBehaviour
{
    // Hacemos que sea Singleton
    private static HowMuchWaves instancia;
    public static HowMuchWaves GetInstancia()
    {
        return instancia;
    }

    private void Awake()
    {
        if (instancia == null)  // la variable que hace referencia al manager está asignada?
            instancia = this;   // si no, asígnala

        else if (instancia != this)  // ya estaba asignada, pero es otro objeto que no es este
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);  // hacemos que al cambiar de escena, el manager se mantenga

    }


    private TextMeshProUGUI textNumero;
    public int num = 3;

    public bool Toaster = false;

    // Start is called before the first frame update
    void Start()
    {
        textNumero = GetComponent<TextMeshProUGUI>();
        textNumero.text = 3.ToString();
    }

    public void SumarNumero(int numbr)
    {
        num += numbr;
        if(num < 2)
            num = 10;
        if (num > 10)
            num = 2;
        textNumero.text = num.ToString();
    }

    public void ToasterMode()
    {
        Toaster = !Toaster;
    }
}
