using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoost : MonoBehaviour
{
    PrometeoCarController carController;

    private void Awake()
    {
        carController = GetComponent<PrometeoCarController>();
    }

    [SerializeField] GameObject boostAlert;

    bool isBoosting;

    float timer;
    float MAXtimer = 5f;

    // Update is called once per frame
    void Update()
    {
        if (!isBoosting && Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton5)) 
        {
            boostAlert.SetActive(false);

            isBoosting = true;
            timer = MAXtimer;
            Debug.Log("Boost");
        }

        if (isBoosting)
        {
            timer -= Time.deltaTime;

            if (timer <= 0) { isBoosting = false; boostAlert.SetActive(true); }
            else if (timer <= MAXtimer * .85f) { carController.carIsBoosting = false; }
            else { carController.carIsBoosting = true; }
        }
    }
}
