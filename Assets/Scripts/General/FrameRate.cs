using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    public int m_limit;

    private void Awake()
    {
        Application.targetFrameRate = m_limit;
    }
}
