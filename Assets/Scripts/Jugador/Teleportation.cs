using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform[] positions;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            GetComponent<PlayerController>().enabled = false;

            if (Input.GetKey(KeyCode.Alpha0))
            {transform.position = positions[0].position; }
            if (Input.GetKey(KeyCode.Alpha1))
            {transform.position = positions[1].position; }
            if (Input.GetKey(KeyCode.Alpha2))
            { transform.position = positions[2].position; }
            if (Input.GetKey(KeyCode.Alpha3))
            {transform.position = positions[3].position; }
            if (Input.GetKey(KeyCode.Alpha4))
            {transform.position = positions[4].position; }
            if (Input.GetKey(KeyCode.Alpha5))
            {transform.position = positions[5].position; }
            GetComponent<PlayerController>().enabled = true;
        }
    }
}
