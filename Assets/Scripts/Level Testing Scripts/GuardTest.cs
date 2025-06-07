using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelTest
{
    public class GuardTest : MonoBehaviour
    {
        bool follow = false;

        [SerializeField] Transform target;
        [SerializeField] float movementSpeed;

        float vectorY;

        private void Start()
        {
            vectorY = transform.position.y;
        }

        // Update is called once per frame
        void Update()
        {
            if (follow)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, vectorY, transform.position.z);
            }
            else
            {
                if (transform.position.x > target.position.x)
                {
                    follow = true;
                }
            }
        }
    }

}
