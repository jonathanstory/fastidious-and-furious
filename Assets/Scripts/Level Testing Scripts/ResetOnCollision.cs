using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelTest
{
    public class ResetOnCollision : MonoBehaviour
    {
        [SerializeField] bool isTrigger;

        private void OnCollisionEnter(Collision collision)
        {
            if (isTrigger) { return; }

            if (collision.collider.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isTrigger) { return; }

            if (other.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
