using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentActivation : MonoBehaviour
{

    [SerializeField] private List<InTheWay> currentlyInTheWay;
    [SerializeField] private List<InTheWay> AlreadyTransparent;
    [SerializeField] private Transform player;
    private Transform camera;
    // Start is called before the first frame update
    private void Awake()
    {
        currentlyInTheWay= new List<InTheWay>();
        AlreadyTransparent= new List<InTheWay>();

        camera = this.gameObject.transform;
    }

    private void GetAllObjectInTheWay()
    {
        currentlyInTheWay.Clear();

        float cameraPlayerDistance = Vector3.Magnitude(camera.position - player.position);

        Ray ray1_Forward = new Ray(camera.position, player.position - camera.position);
        Ray ray1_Backward = new Ray(player.position, camera.position - player.position);

        var hits1_Forward = Physics.RaycastAll(ray1_Forward, cameraPlayerDistance);
        var hits1_Backward = Physics.RaycastAll(ray1_Backward, cameraPlayerDistance);


        foreach(var hit in hits1_Forward)
        {
            if(hit.collider.gameObject.TryGetComponent(out InTheWay inTheWay))
            {
                if (!currentlyInTheWay.Contains(inTheWay))
                {
                    currentlyInTheWay.Add(inTheWay);
                }
            }
        }
        foreach (var hit in hits1_Backward)
        {
            if (hit.collider.gameObject.TryGetComponent(out InTheWay inTheWay))
            {
                if (!currentlyInTheWay.Contains(inTheWay))
                {
                    currentlyInTheWay.Add(inTheWay);
                }
            }
        }
    }
    private void MakeObjectsTransparent()
    {
        for(int i = 0; i < currentlyInTheWay.Count; i++)
        {
            InTheWay inTheWay = currentlyInTheWay[i];

            if (!AlreadyTransparent.Contains(inTheWay))
            {
                inTheWay.ShowTransparent();
                AlreadyTransparent.Add(inTheWay);
            }
        }
    }

    private void MakeObjectsSolid()
    {
        for (int i = 0; i < AlreadyTransparent.Count; i++)
        {
            InTheWay WasInTheWay = AlreadyTransparent[i];

            if (!currentlyInTheWay.Contains(WasInTheWay))
            {
                WasInTheWay.ShowSolid();
                AlreadyTransparent.Remove(WasInTheWay);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetAllObjectInTheWay();

        MakeObjectsSolid();
        MakeObjectsTransparent();
    }
}
