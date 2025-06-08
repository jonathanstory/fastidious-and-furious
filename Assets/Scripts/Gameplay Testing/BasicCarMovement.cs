using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCarMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody player_Rb;
    private Vector3 movement;
    [SerializeField] private float speed = 10f;

    void Start()
    {
        player_Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        movePlayer(movement);
    }

    void movePlayer(Vector3 direction)
    {
        player_Rb.velocity = direction * speed * Time.fixedDeltaTime;
    }
}
