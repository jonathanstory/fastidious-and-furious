using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITracking : MonoBehaviour
{
    public Transform target;

    private NavMeshAgent m_Enemy;

    // Start is called before the first frame update
    void Start()
    {
        m_Enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Enemy.destination = target.position;
    }
}
