using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArtShowcaseMove : MonoBehaviour
{
    public float camSpeed = 0.5f;
    public float rotateSpeed = 30f;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float zAxisValue = Input.GetAxis("Vertical");

        this.transform.Translate(new Vector3(0.0f, 0.0f, zAxisValue * camSpeed));

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(0, -1 * rotateSpeed * Time.deltaTime, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(0, 1 * rotateSpeed * Time.deltaTime, 0);
        }
    }
}
