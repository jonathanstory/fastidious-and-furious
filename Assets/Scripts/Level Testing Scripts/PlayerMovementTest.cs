using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelTest
{
    public class PlayerMovementTest : MonoBehaviour
    {
        Rigidbody thisRigidbody;

        [Header("Default Speed Values")]
        [SerializeField] float forwardSpeed;
        [SerializeField] float horizontalSpeed;

        [Header("Boost Values")]
        [SerializeField] float speedBoostMultiplier;
        [SerializeField] float speedBoostDelay;
        bool canSpeedBoost = true;
        Coroutine speedBoostCoroutine;

        private void Awake()
        {
            thisRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) && canSpeedBoost)
            {
                if (speedBoostCoroutine != null) { StopCoroutine(speedBoostCoroutine); }
                speedBoostCoroutine = StartCoroutine(boost());
            }
        }

        IEnumerator boost()
        {
            canSpeedBoost = false;

            Debug.Log("SPEED BOOST");

            forwardSpeed *= speedBoostMultiplier;
            horizontalSpeed *= speedBoostMultiplier;

            yield return new WaitForSeconds(speedBoostDelay * .15f);

            Debug.Log("Speed boost lost");

            forwardSpeed /= speedBoostMultiplier;
            horizontalSpeed /= speedBoostMultiplier;

            yield return new WaitForSeconds(speedBoostDelay * .85f);

            canSpeedBoost = true; 
            
            Debug.Log("Speed boost reloaded");

        }

        private void FixedUpdate()
        {
            float horizontalMovement = 0;

            if (Input.GetKey(KeyCode.LeftArrow))
            { horizontalMovement = -1; }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (horizontalMovement != 0) { horizontalMovement = 0; }
                else
                {
                    horizontalMovement = 1;
                }
            }

            thisRigidbody.MovePosition(thisRigidbody.position + new Vector3(1 * forwardSpeed * Time.fixedDeltaTime, 0, 1 * horizontalMovement * horizontalSpeed * Time.fixedDeltaTime));
        }
    }

}