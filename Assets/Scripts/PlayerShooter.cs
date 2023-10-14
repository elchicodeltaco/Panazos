using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooling))]

public class PlayerShooter : MonoBehaviour
{
    ObjectPooling pool;
    public Transform muzzle;
    public float force;
    public int currentAmmo;
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

            if (currentAmmo > 0)
            {
                transform.GetComponentInChildren<Animator>().SetBool("Attack", true);
                dustWorld.Play();
                dustLocal.Play();
                //AudioManager.instancia.PlaySFX(0);
            }

            if (currentAmmo <= 0)
            {
                dustLocal.Play();
            }
        }
    }

    public void ShootToast()
    {
        GameObject toast = pool.GetObjectFromPool();

        float[] rand = new float[3];
        for (int i = 0; i < rand.Length; i++)
        {
            rand[i] = Random.Range(0f, 360f);
        }

        if (toast)
        {
            toast.tag = "ToastBase";
            toast.transform.position = muzzle.position;
            toast.transform.rotation = Quaternion.Euler(transform.rotation.x + rand[0], transform.rotation.y + rand[1], transform.rotation.z + rand[2]);

            toast.GetComponent<Rigidbody>().AddForce(transform.rotation * Vector3.forward * force * 100);
            

            currentAmmo--;
            //GameManager.GetInstancia().UpdateAmmoOnScreen(currentAmmo);
        }
    }

    public void AddMoreAmmo()
    {
        //int newAmmo = Random.Range(15, 51);
        currentAmmo += 10;
        //GameManager.GetInstancia().UpdateAmmoOnScreen(currentAmmo);
    }
}