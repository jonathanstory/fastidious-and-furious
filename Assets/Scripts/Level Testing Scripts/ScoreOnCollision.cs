using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelTest
{
    public class ScoreOnCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                ScoreTest.AddScore();
                Destroy(gameObject);
            }
        }
    }
}
