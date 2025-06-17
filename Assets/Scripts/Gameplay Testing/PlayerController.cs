using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    AudioSource audioSource;
    Rigidbody playerRb;

    [Header("Player Statistics")]
    [Space(20)]
    [Range(10, 30)]
    [SerializeField] private float acceleration; // Rate of acceleration
    [Space(10)]
    [SerializeField] private float maxSpeed; // Maximum speed in km/h
    [Space(10)]
    [SerializeField] private float maxReverseSpeed; // Maximum reverse speed in km/h
    [Space(10)]
    [SerializeField] private float boostSpeed; // Extra speed from boost in km/h
    [Space(10)]
    [Range(1, 10)]
    [SerializeField] private float turnSpeed; // Rate at which horse turns
    [Space(10)]
    [Range(10, 30)]
    [SerializeField] private float brakeForce; // Rate at which car brakes and reverses
    [Space(10)]
    [Range(1, 200)]
    [SerializeField] private float gripForce; // Strength that the car grips the road with, no drift included

    [Header("Sounds")]
    [SerializeField] AudioClip getHurt;
    [SerializeField] AudioClip punchNpc;


    private bool isAccelerating;
    private float turnRate = 0f;
    private float speed;
    private bool carIsBoosting = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        playerRb = GetComponent<Rigidbody>();
        maxSpeed /= 3.6f; // Convert from Unity's m/s to km/h
        maxReverseSpeed /= 3.6f;
        boostSpeed /= 3.6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRb.velocity.z < maxSpeed && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            isAccelerating = true;
        else
            isAccelerating = false;

        if (Input.GetKey(KeyCode.A))
        {
            turnRate -= .001f * turnSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            turnRate += .001f * turnSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            speed += -brakeForce;
        }
        else
            speed = acceleration;

        if (!carIsBoosting)
            speed = Mathf.Clamp(speed, -maxReverseSpeed, maxSpeed);
        else
            speed = Mathf.Clamp(speed, -maxReverseSpeed, maxSpeed + boostSpeed);


        turnRate = Mathf.Clamp(turnRate, -turnSpeed / 10, turnSpeed / 10);

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            turnRate = Mathf.Lerp(turnRate, 0, .04f);

        if (playerRb.velocity.z != 0 && GameController.paused == false)
            this.transform.Rotate(0, turnRate, 0);

        if (isAccelerating) { if (!audioSource.isPlaying) { audioSource.Play(); } }
        else {
            if (audioSource.isPlaying) { audioSource.Stop(); } }
    }



    private void FixedUpdate()
    {
        if (isAccelerating)
            playerRb.AddRelativeForce(turnRate * gripForce, 0, speed, ForceMode.Acceleration);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            FleeFromPlayer npc = other.GetComponent<FleeFromPlayer>();
            if (npc != null)
            {
                Debug.Log("Triggered NPC");
                audioSource.PlayOneShot(punchNpc);
                npc.OnPlayerHit(transform.forward);
                GameController.score += 1;
            }
        }
        if (other.CompareTag("Spikes"))
        {
            audioSource.PlayOneShot(getHurt);
            GameController.playerHealth -= 1;
        }
    }
}


