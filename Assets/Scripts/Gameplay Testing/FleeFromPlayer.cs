using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FleeFromPlayer : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float fleeDistance = 15f;
    public float fleeCooldown = 1f;

    private Transform player;
    private NavMeshAgent agent;
    private bool isFleeing = false;
    private float cooldownTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; //rotation manually
    }

    void Update()
    {
        if (player == null) return;

        if (isFleeing)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
                isFleeing = false;

            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRadius)
        {
            Flee();
        }
    }

    void Flee()
    {
        isFleeing = true;
        cooldownTimer = fleeCooldown;

        Vector3 fleeDir = (transform.position - player.position).normalized;
        fleeDir.y = 0f; // stay on ground
        Debug.DrawRay(transform.position, fleeDir * 5f, Color.red);
        Vector3 fleeTarget = transform.position + fleeDir * fleeDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleeTarget, out hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void LateUpdate()
    {
        // Rotate to face movement direction
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            Vector3 lookDir = new Vector3(agent.velocity.x, 0f, agent.velocity.z);
            Quaternion lookRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
        }
    }
}
