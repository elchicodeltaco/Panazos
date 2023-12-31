using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooling))]

public class PlayerShooter : MonoBehaviour
{
    ObjectPooling pool;
    public Transform muzzle;
    public Transform runningMuzzle;
    public float force;
    public float GrenadeForce;
    public int currentAmmoTostadora;
    public int currentAmmoGranada;
    [SerializeField] private ParticleSystem dustWorld;
    [SerializeField] private ParticleSystem dustLocal;

    private static PlayerShooter instancia;
    public static PlayerShooter GetInstancia()
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

    // Start is called before the first frame update
    void Start()
    {
        //GameManager.GetInstancia().UpdateAmmoOnScreen(currentAmmo);
        pool = GetComponent<ObjectPooling>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            //SFX

            if (currentAmmoTostadora > 0)
            {
                transform.GetComponentInChildren<Animator>().SetBool("Attack", true);
                dustWorld.Play();
                dustLocal.Play();
                //AudioManager.instancia.PlaySFX(0);
            }

            if (currentAmmoTostadora <= 0)
            {
                dustLocal.Play();
            }
        }
        /*if (Input.GetButton("Fire2"))
        {
            //GetComponent<ApuntadoGranada>().launchSpeed = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z).magnitude;
            GetComponent<ApuntadoGranada>().estaActivo = true;
        }
        */
        if (Input.GetButtonUp("Fire2"))
        {
            ThrowGrenade();
            //GetComponent<ApuntadoGranada>().estaActivo = false;
        }
    }

    public void ShootToast()
    {
        GameObject toast = pool.GetObjectFromPool("Pan");

        float[] rand = new float[3];
        for (int i = 0; i < rand.Length; i++)
        {
            rand[i] = Random.Range(0f, 360f);
        }

        if (toast)
        {
            toast.tag = "ToastBase";
            if (Input.GetButton("Fire3"))
            {
                toast.transform.position = runningMuzzle.position;
            }
            else
            {
                toast.transform.position = muzzle.position;
            }
            toast.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            toast.GetComponent<Rigidbody>().velocity = Vector3.zero;
            toast.GetComponent<Rigidbody>().AddForce(transform.rotation * Vector3.forward * force * 100);
            

            currentAmmoTostadora--;
            //GameManager.GetInstancia().UpdateAmmoOnScreen(currentAmmo);
        }
    }
    public void ThrowGrenade()
    {
        GameObject grenade = pool.GetObjectFromPool("Granada");

        if (grenade)
        {
            float launchAngle = 45f; // �ngulo de inclinaci�n en grados
            float horizontalSpeed = GrenadeForce; // Velocidad horizontal
            float verticalSpeed = horizontalSpeed * Mathf.Tan(launchAngle * Mathf.Deg2Rad); // Calcula la velocidad vertical

            Vector3 launchDirection = transform.forward; // Direcci�n hacia adelante

            // Aplica la fuerza con la velocidad horizontal y vertical
            grenade.transform.position = muzzle.transform.position;
            grenade.GetComponent<Rigidbody>().velocity = launchDirection * horizontalSpeed + Vector3.up * verticalSpeed;
            Debug.Log(grenade.GetComponent<Rigidbody>().velocity);

            currentAmmoGranada--;
            //GameManager.GetInstancia().UpdateAmmoOnScreen(currentAmmo);
        }
    }
    public void AddMoreAmmo()
    {
        //int newAmmo = Random.Range(15, 51);
        currentAmmoTostadora += 10;
        //GameManager.GetInstancia().UpdateAmmoOnScreen(currentAmmo);
    }
}
