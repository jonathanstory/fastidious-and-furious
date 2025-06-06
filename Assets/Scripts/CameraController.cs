using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject trackTarget;

    private Vector3 offset = new Vector3(0, 3, -8);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = trackTarget.transform.rotation;
        transform.position = trackTarget.transform.position + offset;
    }
}
