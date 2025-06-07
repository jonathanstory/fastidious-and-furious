using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelTest
{
    public class ScoreTest : MonoBehaviour
    {
        static int score;

        private void Start()
        {
            score = 0;
        }

        public static void AddScore()
        {
            score++;
            Debug.Log("Score: " + score);
        }
    }
}
