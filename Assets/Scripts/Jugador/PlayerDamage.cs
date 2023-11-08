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


    private Coroutine corrutinaActiva;
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
        corrutinaActiva = null;
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

    public void GetDamage()
    {

        if (corrutinaActiva != null)
        {
            StopCoroutine(corrutinaActiva);
        }
        CurrentHealth --;
        GameManager.GetInstancia().GettingDamageUI();
        animator.SetTrigger("Damage");
        corrutinaActiva = StartCoroutine(DamageRutine());
        if (CurrentHealth <= 0)
        {
            GameManager.GetInstancia().GettingDamageUI();
            GetComponent<RagdollPlayer>().EnableRagdoll();
            return;

        }
    }

    private IEnumerator DamageRutine()
    {
        if(CurrentHealth > 0)
        {
            yield return new WaitForSeconds(4);
            CurrentHealth = ToalHealth;
            GameManager.GetInstancia().GettingDamageUI();
        }
        
            
    }

    public void AddMoreHealth()
    {
        //int newHealth = Random.Range(5, 16);
        CurrentHealth += 5;
    }
}
