using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichDoor : DoubleDoor
{
    private void Update()
    {
        if (m_isActive)
        {
            if (m_doorOneCor == null && m_doorTwoCor == null)
            {
                MoveDoors();
                SwitchTarget();
                return;
            }

            StopCoroutine(m_doorOneCor);
            StopCoroutine(m_doorTwoCor);

            Vector3 temp1 = m_doorOneOriginalPos, temp2 = m_doorTwoOriginalPos;
            //se guardan las posiciones en las que las puertas se encuentran si estan a mitad de camino
            m_doorOneOriginalPos = m_doorOne.position;
            m_doorTwoOriginalPos = m_doorTwo.position;

            MoveDoors();
            m_doorOneOriginalPos = temp1;
            m_doorTwoOriginalPos = temp2;
            SwitchTarget();
            //m_isActive = false;
        }
    }

    protected override IEnumerator ActivationCoroutine()
    {
        m_isActive = true;
        yield return new WaitForEndOfFrame();
        m_isActive = false;
    }
}
