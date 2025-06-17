using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class FleeFromPlayer : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float fleeDistance = 15f;
    public float fleeCooldown = 1f;
    public float throwForceMin = 20f;
    public float throwForceMax = 50f;
    public float torqueMin = 10f;
    public float torqueMax = 50f;
    public float despawnDelay = 1.2f;


    private Transform player;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private bool isFleeing = false;
    private bool isThrown = false;
    private float cooldownTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        agent.updateRotation = false;
        rb.isKinematic = true; // Start with physics off
    }

    void Update()
    {
        if (isThrown || player == null) return;

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
        fleeDir.y = 0f;
        Vector3 fleeTarget = transform.position + fleeDir * fleeDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleeTarget, out hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void LateUpdate()
    {
        if (agent.velocity.sqrMagnitude > 0.1f && !isThrown)
        {
            Vector3 lookDir = new Vector3(agent.velocity.y, 0f, agent.velocity.x);
            Quaternion lookRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
        }
    }

    // Call this when player hits the NPC
    public void OnPlayerHit(Vector3 playerForward)
    {
        if (isThrown) return;
        isThrown = true;

        agent.enabled = false;
        rb.isKinematic = false;

        // Raise slightly before throw to prevent clipping
        transform.position += Vector3.up * 0.2f;

        // Flatten forward direction and add a small upward component
        Vector3 flatForward = new Vector3(playerForward.x, 0f, playerForward.z).normalized;
        Vector3 launchDirection = (flatForward + Vector3.up * 0.2f).normalized;

        // Reset motion
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Apply force
        rb.AddForce(launchDirection * Random.Range(throwForceMin, throwForceMax), ForceMode.Force);

        // Apply only Y-axis torque (so they spin but don't flip downward)
        Vector3 yTorque = Vector3.up * Random.Range(-1f, 1f) * Random.Range(torqueMin, torqueMax);
        rb.AddTorque(yTorque, ForceMode.Impulse);

        // Despawn after delay
        Destroy(gameObject, despawnDelay);
    }


    void OnCollisionEnter(Collision collision)
    {
        FleeFromPlayer npc = collision.gameObject.GetComponent<FleeFromPlayer>();
        if (npc != null)
        {
            Debug.Log("NPC hit!");
            npc.OnPlayerHit(transform.forward);
        }
    }

}
