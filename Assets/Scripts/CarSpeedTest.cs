using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarSpeedText : MonoBehaviour
{
    public TextMeshProUGUI speedText; 
    public Rigidbody Car;

    void Start()
    {
        if (Car == null)
        {
            Car = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        float speedMph = Car.velocity.magnitude * 2.23693629f; // Convert m/s to mph
        speedText.text = speedMph.ToString("0"); 
    }
}