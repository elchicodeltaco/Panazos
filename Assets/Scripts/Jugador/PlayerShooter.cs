using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

[RequireComponent(typeof(ObjectPooling))]

public class PlayerShooter : MonoBehaviour
{
    ObjectPooling pool;
    public Transform muzzle;
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
        if (instancia == null)  // la variable que hace referencia al manager está asignada?
            instancia = this;   // si no, asígnala

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
        if (Input.GetButtonDown("Fire2"))
        {
            //SFX

            /*if (currentAmmoTostadora > 0)
            {
                transform.GetComponentInChildren<Animator>().SetBool("Attack", true);
                dustWorld.Play();
                dustLocal.Play();
                //AudioManager.instancia.PlaySFX(0);
            }

            if (currentAmmoTostadora <= 0)
            {
                dustLocal.Play();
            }*/
            ThrowGrenade();
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
            toast.transform.position = muzzle.position;
            toast.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);

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
            grenade.tag = "GranadaBase";
            grenade.transform.position = muzzle.position;
            grenade.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);

            grenade.GetComponent<Rigidbody>().AddForce(transform.rotation * Vector3.forward * force * 100);




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
