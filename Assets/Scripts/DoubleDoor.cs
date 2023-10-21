using System.Collections;
using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
    private bool m_hold = false;
    private Transform m_doorOne;
    private Transform m_doorTwo;
    private Vector3 m_targetOne;
    private Vector3 m_targetTwo;
    private Vector3 m_doorOneOriginalPos;
    private Vector3 m_doorTwoOriginalPos;

    [SerializeField] private float m_lerpDistance;
    [SerializeField] private float m_lerpDuration;
    [SerializeField] private AnimationCurve m_lerpCurve;

    public bool m_isActive = false;
    public bool m_isOpen = false;

    private IEnumerator m_doorOneOpenCor;
    private IEnumerator m_doorTwoOpenCor;
    private IEnumerator m_doorOneCloseCor;
    private IEnumerator m_doorTwoCloseCor;

    private void Start()
    {
        //para guardar las referencias a las puertas sin necesidad del inspector
        Transform[] puertas = GetComponentsInChildren<Transform>();
        m_doorOne = puertas[1];
        m_doorTwo = puertas[2];

        //posiciones originales de las puertas
        m_doorOneOriginalPos = m_doorOne.position;
        m_doorTwoOriginalPos = m_doorTwo.position;

        //posiciones de los targets 
        m_targetOne = m_doorOne.TransformPoint(new Vector3(0, 0, m_lerpDistance));
        m_targetTwo = m_doorTwo.TransformPoint(new Vector3(0, 0, -m_lerpDistance));

        //referencias a las corrutinas de las puertas
            //corrutinas para abrir
        m_doorOneOpenCor = LerpPosition(m_doorOne, m_doorOne.position, m_targetOne, m_lerpDuration);
        m_doorTwoOpenCor = LerpPosition(m_doorTwo, m_doorTwo.position, m_targetTwo, m_lerpDuration);
            //corrutinas para cerrar
        m_doorOneCloseCor = LerpPosition(m_doorOne, m_doorOne.position, m_doorOneOriginalPos, m_lerpDuration);
        m_doorTwoCloseCor = LerpPosition(m_doorTwo, m_doorTwo.position, m_doorTwoOriginalPos, m_lerpDuration);
    }

    public void MoveDoors(bool openOrClosed)
    {
        m_isActive = !m_isActive;
        //
    }

    private void OpenDoors()
    {
        StopAllCoroutines();
        StartCoroutine(m_doorOneOpenCor);
        StartCoroutine(m_doorTwoOpenCor);
        m_isOpen = true;
    }

    private void CloseDoors()
    {
        StopAllCoroutines();
        StartCoroutine(m_doorTwoCloseCor);
        StartCoroutine(m_doorOneCloseCor);
        m_isOpen = false;
    }
    public void Desactivar()
    {
        //estaActivo = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_isActive = !m_isActive;
            if (m_isActive)
            {
                OpenDoors();
            }
            if (!m_isActive)
            {
                CloseDoors();
            }
        }
    }

    private IEnumerator LerpPosition(Transform door, Vector3 start, Vector3 target, float lerpDuration)
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            door.position = Vector3.Lerp(start, target, m_lerpCurve.Evaluate(timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        door.position = target;
    }

}
