using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ApuntadoGranada : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public int linePoints = 200;
    public float timeIntervalInPoint = 0.01f;
    public bool estaActivo;

    public Transform launchPoint;
    public float launchSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        estaActivo= false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer != null)
        {
            if (estaActivo)
            {
                DrawTrajectory();
                lineRenderer.enabled = true;
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
    }

    public void DrawTrajectory()
    {
        Vector3 origin = launchPoint.position;
        Vector3 startVelocity = launchSpeed * launchPoint.up;
        lineRenderer.positionCount = linePoints;
        float time = 0;
        for(int i = 0; i< linePoints; i++)
        {
            var x = (startVelocity.x * time) * (Physics.gravity.x / 2 * time * time);
            var y = (startVelocity.y * time) * (Physics.gravity.x / 2 * time * time);

            Vector3 point = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalInPoint;
        }
    }
}
