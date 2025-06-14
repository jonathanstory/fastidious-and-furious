using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelTest
{
    public class ScoreOnCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Horse")
            {
                ScoreTest.AddScore();
                Destroy(gameObject);
            }
        }
    }
}
