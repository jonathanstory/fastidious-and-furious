using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    Vector3 startingPosition; // using this to calculate wandering instead of transform.position
    LevelTest.ResetOnCollision resetOnCollisionComponent;

    // Start is called before the first frame update
    void Start()
    {
        m_Enemy = GetComponent<NavMeshAgent>();
        timer = setWanderTime;

        startingPosition = transform.position;

        isWandering = true;

        resetOnCollisionComponent = GetComponent<LevelTest.ResetOnCollision>();
        resetOnCollisionComponent.canReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkDistance = new Vector3(Mathf.Abs(target.position.x - this.transform.position.x), 0, Mathf.Abs(target.position.z - this.transform.position.z));

        /*Temporarily removing this because AI rushes you way too fast
         * 
         * if (checkDistance.x <= triggerDistance.x && checkDistance.z <= triggerDistance.z)
        */

        if (transform.position.x - 2 > target.position.x)
        {
            isWandering = false;
            playerIsFound = true;

            resetOnCollisionComponent.canReset = true;
        }

        /* temporarily removing this because the AI can be a bit easy to evade initially
        else
        {
            playerIsFound = false;
            isWandering = true;
        }*/



        if (playerIsFound)
            m_Enemy.destination = target.position;
        else
            if(isWandering == true)
            {
                timer += Time.deltaTime;

                if (timer >= setWanderTime)
                {
                    Vector3 newTarget = RandomNavSphere(startingPosition, wanderRadius);
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

