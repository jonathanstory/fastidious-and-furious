using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelTest
{
    public class ResetOnCollision : MonoBehaviour
    {
        [SerializeField] bool isTrigger;

        [HideInInspector] public bool canReset = true;

        private void OnCollisionEnter(Collision collision)
        {
            if (!canReset) { return; }
            if (isTrigger) { return; }

            if (collision.collider.gameObject.tag == "Horse")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!canReset) { return; }
            if (!isTrigger) { return; }

            if (other.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
