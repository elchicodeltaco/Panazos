using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int ToalHealth;
    public int CurrentHealth;
    private Animator animator;
    private bool canGetDamage;
    private bool ChangeLayer;

    public LayerMask playerLayer;

    private static PlayerDamage instancia;
    public static PlayerDamage GetInstancia()
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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        CurrentHealth = ToalHealth;
        //GameManager.GetInstancia().UpdateHeathOnScreen(CurrentHealth);
        canGetDamage = true;
        ChangeLayer = false;
    }

    // Update is called once per frame
    void Update()
    {/*
        if (ChangeLayer)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = playerLayer;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.CompareTag("DamageCollider"))
        {
            if(canGetDamage)
            GetDamage(5);
        }

        if (other.CompareTag("SafeSpace"))
        {
            gameObject.layer = 0;
        }
        if (other.CompareTag("ExitSafeSpace"))
        {
            gameObject.layer = 6;
        }**/
    }

    public void GetDamage(int danio)
    {

        StartCoroutine(GameManager.GetInstancia().SmoothDecreaseHealth(danio, CurrentHealth));
        CurrentHealth -= danio;
        animator.SetTrigger("Damage");
        StartCoroutine(DamageRutine());
        if (CurrentHealth <= 0)
        {
            StartCoroutine(GameManager.GetInstancia().SmoothDecreaseHealth(0, 0));
            GetComponent<RagdollPlayer>().EnableRagdoll();
            return;

        }
    }

    private IEnumerator DamageRutine()
    {
        canGetDamage = false;
        yield return new WaitForSeconds(2);
        canGetDamage = true;
    }

    public void AddMoreHealth()
    {
        //int newHealth = Random.Range(5, 16);
        CurrentHealth += 5;
        GameManager.GetInstancia().UpdateAmmoOnScreen(CurrentHealth);
    }
}
