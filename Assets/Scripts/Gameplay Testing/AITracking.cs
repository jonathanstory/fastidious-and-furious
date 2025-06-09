using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AITracking : MonoBehaviour
{
    public Transform target;
    bool playerIsFound = false;
    private NavMeshAgent m_Enemy;
    private bool isWandering = false;

    private Vector3 checkDistance;

    [SerializeField] private float setWanderTime;

    [SerializeField] private Vector3 triggerDistance = new Vector3(10, 0, 10);
    [SerializeField] private float wanderRadius = 5;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        m_Enemy = GetComponent<NavMeshAgent>();
        timer = setWanderTime;
    }

    // Update is called once per frame
    void Update()
    {
        checkDistance = new Vector3(Mathf.Abs(target.position.x - this.transform.position.x), 0, Mathf.Abs(target.position.z - this.transform.position.z));

        if (checkDistance.x <= triggerDistance.x && checkDistance.z <= triggerDistance.z)
        {
            isWandering = false;
            playerIsFound = true;
        }
        else
        {
            playerIsFound = false;
            isWandering = true;
        }

        if (playerIsFound)
            m_Enemy.destination = target.position;
        else
            if(isWandering == true)
            {
                timer += Time.deltaTime;

                if (timer >= setWanderTime)
                {
                    Vector3 newTarget = RandomNavSphere(transform.position, wanderRadius);
                    m_Enemy.SetDestination(newTarget);
                    timer = 0;
                }
            }
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, -1);

        return navHit.position;
    }
}

