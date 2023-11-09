using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[SelectionBase]
public class ZombieBase : MonoBehaviour
{
    //fms
    private MaquinaEstados maquinaEstados;
    //Lista de estados
    public Estado estadoWander;
    public Estado estadoChase;
    public Estado estadoAttack;

    [Header("Rangos de proximidad")]
    [SerializeField] private float m_alertRange;
    [SerializeField] private float m_attackRange;
    [SerializeField] public float m_wanderRange;

    [Header("Variables")]
    [SerializeField] LayerMask m_playersMask;

    //esferas
    private bool m_alertState;
    private bool m_attackState;
    public bool _getAlertState
    {
        get { return m_alertState; }
    }
    public bool _getAttackState
    {
        get { return m_attackState; }
    }

    //variables privadas
    [HideInInspector] public NavMeshAgent m_agent;
    [HideInInspector] public Transform m_player;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();

        maquinaEstados = new MaquinaEstados(this);

        //Crear estados que puede tener el zombie
        estadoWander = new StateWander(maquinaEstados, animator, this);
        estadoChase = new StateChase(maquinaEstados, animator, this);
        estadoAttack = new StateAttack(maquinaEstados, animator, this);

        //Se empieza a ejecutar la FMS
        maquinaEstados.Iniciar(estadoWander);

    }

    void Update()
    {
        maquinaEstados.Update();

        m_alertState = Physics.CheckSphere(transform.position, m_alertRange, m_playersMask);
        m_attackState = Physics.CheckSphere(transform.position + transform.forward * 0.5f, m_attackRange, m_playersMask);

        if(m_alertState && m_player == null)
        {
            Collider[] coll = Physics.OverlapSphere(transform.position, m_alertRange, m_playersMask);
            m_player = coll[0].gameObject.GetComponent<Transform>();
            Debug.LogWarning("el jugador fue asigando");
        }

        animator.SetFloat("velocity",m_agent.velocity.magnitude);
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, m_alertRange);
        Gizmos.DrawWireSphere(transform.position + transform.forward * 0.5f, m_attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_wanderRange);
    }

}
