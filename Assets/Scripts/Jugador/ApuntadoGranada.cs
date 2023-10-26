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
    public float launchSpeed = 10f; // Ajusta la velocidad de lanzamiento según tus necesidades
    private float timeOfFlight = 2f; // Ajusta el tiempo de vuelo según tus necesidades

    void Start()
    {
        estaActivo = false;
    }

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
        lineRenderer.positionCount = linePoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = launchPoint.position;
        Vector3 startingVelocity = launchPoint.up * launchSpeed;

        for (int i = 0; i < linePoints; i++)
        {
            float t = i * (timeOfFlight / (linePoints - 1));
            float x = startingPosition.x + startingVelocity.x * t;
            float y = startingPosition.y + startingVelocity.y * t - 0.5f * Physics.gravity.y * t * t;
            Vector3 newPoint = new Vector3(x, y, startingPosition.z);
            points.Add(newPoint);
        }

        lineRenderer.SetPositions(points.ToArray());
    }
}
