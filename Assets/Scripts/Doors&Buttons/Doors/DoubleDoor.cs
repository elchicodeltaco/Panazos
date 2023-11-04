using System.Collections;
using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
    //Puertas fisicas
    protected Transform m_doorOne;
    protected Transform m_doorTwo;

    private Vector3 m_targetOne;
    private Vector3 m_targetTwo;
    protected Vector3 m_doorOneOriginalPos;
    protected Vector3 m_doorTwoOriginalPos;

    [SerializeField] private float m_lerpDistance;
    [SerializeField] private float m_lerpDuration;
    [SerializeField] private AnimationCurve m_lerpCurve;

    protected bool m_isActive = false;

    protected Coroutine m_doorOneCor;
    protected Coroutine m_doorTwoCor;

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

        m_doorOneCor = null;
        m_doorTwoCor = null;
    }

    protected void MoveDoors()
    {
        m_doorOneCor = StartCoroutine(LerpPosition(m_doorOne, m_doorOneOriginalPos, m_targetOne, m_lerpDuration));
        m_doorTwoCor = StartCoroutine(LerpPosition(m_doorTwo, m_doorTwoOriginalPos, m_targetTwo, m_lerpDuration));
    }

    public void SwitchTarget()
    {
        Vector3 temp = m_targetOne;
        m_targetOne = m_doorOneOriginalPos;
        m_doorOneOriginalPos = temp;
        
        temp = m_targetTwo;
        m_targetTwo = m_doorTwoOriginalPos;
        m_doorTwoOriginalPos = temp;
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
    }

    public void ActivationFunction()
    {
        StartCoroutine(ActivationCoroutine());
    }

    protected virtual IEnumerator ActivationCoroutine()
    {
        m_isActive = true;
        yield return new WaitForFixedUpdate();
        m_isActive = false;
        
    }

}
