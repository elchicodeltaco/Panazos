using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform muzzle;
    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr= GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, muzzle.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(muzzle.transform.position, muzzle.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, muzzle.transform.forward);
    }
}
