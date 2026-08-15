using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : MonoBehaviour
{ 
    [Header("Components")]
    [SerializeField] private FieldOfView fov;
    [SerializeField] private Transform body;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Collider attackCollider;

    [Header("Patrol Variables")]
    [SerializeField] private float speedPatrol;
    [SerializeField] private float minDistanceRoam;
    [SerializeField] private float maxDistanceRoam;
    [SerializeField] private float timeUntilNextRoamPoint;
    private float currentTimeUntilNextRoamPoint;
   
    [Header("Chase Variables")]
    [SerializeField] private float speedChasing;

    [Header("Attack Variables")]    
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDuration;
    private float currentCooldownTime;
    private float currentAttackDuration;
    
    // States
    [SerializeField] private EMeleeEnemyStates currentState;

    private enum EMeleeEnemyStates 
    {
        Patrol,
        Chasing,
        Attaking,
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = EMeleeEnemyStates.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) 
        {
            case EMeleeEnemyStates.Patrol:
                agent.speed = speedPatrol;
                BehaviourPatrol();
                break;
            case EMeleeEnemyStates.Chasing:
                agent.speed = speedChasing;
                BehaviourChase();
                break;
            case EMeleeEnemyStates.Attaking:
                BehaviourAttak();
                break;
        }
    }
    private void BehaviourAttak()
    {
        if (currentCooldownTime > 0f)
        {
            currentCooldownTime -= Time.deltaTime;
        }
        else 
        {
            if (!attackCollider.enabled) attackCollider.enabled = true;
            if (currentAttackDuration < attackDuration) 
            {
                currentAttackDuration += Time.deltaTime;
            }
            else 
            {
                currentCooldownTime = attackCooldown;
                currentAttackDuration = 0;
                attackCollider.enabled = false;
                currentState = EMeleeEnemyStates.Patrol;
            }
        }
    }
    private void BehaviourChase()
    {
        if (fov.HasVisualTarget)
        {
            agent.SetDestination(fov.VisibleTargets[0].transform.position);
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                agent.velocity = Vector3.zero;
                currentState = EMeleeEnemyStates.Attaking;
            }
        }
        else 
        {
            agent.SetDestination(fov.LastKnownPosition);
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                currentTimeUntilNextRoamPoint = 0;
                currentState = EMeleeEnemyStates.Patrol;
            }
        }
    }
    private void BehaviourPatrol() 
    {
        if (!fov.HasVisualTarget)
        {
            if (!agent.hasPath)
            {
                if (currentTimeUntilNextRoamPoint < timeUntilNextRoamPoint) currentTimeUntilNextRoamPoint += Time.deltaTime;
                else
                {
                    while (!GetRandomPointToRoam()) ;
                    currentTimeUntilNextRoamPoint = 0;
                }
            }
            else
            {
                float dist = Vector3.Distance(agent.destination, transform.position);
                if (dist < agent.stoppingDistance)
                {
                    agent.ResetPath();
                }
            }
        }
        else currentState = EMeleeEnemyStates.Chasing;
    }
    private bool GetRandomPointToRoam() 
    {
        //Debug.Log("try get random point");
        float tempValue = Random.Range(minDistanceRoam, maxDistanceRoam);
        Vector3 randomPoint = transform.position + Random.onUnitSphere * tempValue;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            return true;
        }
        else return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            other.gameObject.GetComponent<PlayerController>().GetDamage(attackDamage);
        }
    }
}
