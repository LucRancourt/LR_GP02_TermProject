using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyAIStates
{
    Idle,
    Patrol,
    Chase,
    Attack
}



[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyAIStates currentState = EnemyAIStates.Idle;

    [Header("Idle")] 
    [SerializeField] private float timeToPatrol = 5.0f;
    private bool timerToPatrolStarted = false;

    [Header("Patrol")] 
    [SerializeField] private Transform[] patrolPoints;
    private int _currentPatrolPoint = 0;
    [SerializeField] private bool isPatrolRandom = false;
    [SerializeField] private float patrolSpeed = 5.0f;
    
    [Header("Chase")]
    //[SerializeField] private float chaseDistance = 5f;
    [SerializeField] private float chaseSpeed = 5.0f;
    
    [Header("Attack")]
    [SerializeField] private float attackDistance = 1.0f;
    [SerializeField] private float attackDelay = 0.5f;

    private EnemyAIStates _previousState;
    private GameObject _currentTarget;
    private Vector3 _targetPosition;

    [Header("NavMeshAgent")]
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private float stoppingDistance = 5.0f;



    // Functions
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.stoppingDistance = stoppingDistance;
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyAIStates.Idle:
                IdleBehaviour();
                break;
            
            case EnemyAIStates.Patrol:
                PatrolBehaviour();
                break;
            
            case EnemyAIStates.Chase:
                ChaseBehaviour();
                break;
            
            case EnemyAIStates.Attack:
                AttackBehaviour();
                break;
        }
    }

    private void IdleBehaviour()
    {
        Debug.Log("IdleBehaviour");
        
        // Rigidbody? or some custom like CharacterController?
        
        
        // Remove to see Funny Stuff after finished Chasing
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        if (!timerToPatrolStarted)
            StartCoroutine(IdleToPatrol());
    }
    
    IEnumerator IdleToPatrol()
    {
        timerToPatrolStarted = true;
        
        yield return new WaitForSeconds(timeToPatrol);

        timerToPatrolStarted = false;
        currentState = EnemyAIStates.Patrol;
    }

    private void PatrolBehaviour()
    {
        Debug.Log("PatrolBehaviour");

        if ((Mathf.Approximately(transform.position.x, patrolPoints[_currentPatrolPoint].position.x)) && 
                Mathf.Approximately(transform.position.z, patrolPoints[_currentPatrolPoint].position.z))
        {
            NewPatrolPointTarget();
            
            currentState = EnemyAIStates.Idle;
        }
        else
            transform.position = HelpfulFunctions.MoveToWithoutVertical(transform.position, patrolPoints[_currentPatrolPoint].position, patrolSpeed);
    }

    private void NewPatrolPointTarget()
    {
        if (isPatrolRandom)
        {
            _currentPatrolPoint += HelpfulFunctions.RandomOne();
            
            if (_currentPatrolPoint < 0)
                _currentPatrolPoint = patrolPoints.Length - 1;
        }
        else
            _currentPatrolPoint++;
        
        
        if (_currentPatrolPoint >= patrolPoints.Length)
            _currentPatrolPoint = 0;
    }

    private void ChaseBehaviour()
    {
        Debug.Log("Chase");

        RotateTowardsTarget();


        _navMeshAgent.SetDestination(_currentTarget.transform.position);
        //transform.position = Vector3.MoveTowards(transform.position, _currentTarget.transform.position, chaseSpeed * Time.deltaTime);
        
        float currentDistance = Vector3.Distance(transform.position, _currentTarget.transform.position);

        if (currentDistance <= attackDistance)
        {
            currentState = EnemyAIStates.Attack;
        }
    }

    private void AttackBehaviour()
    {
        Debug.Log("Attack");

        RotateTowardsTarget();


        StartCoroutine(AttackDelay());
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);

        currentState = EnemyAIStates.Chase;
    }

    private void RotateTowardsTarget()
    {
        if (_currentTarget)
            transform.LookAt(_currentTarget.transform);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            
            _currentTarget = other.gameObject;
            currentState = EnemyAIStates.Chase;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            
            _currentTarget = null;
            currentState = EnemyAIStates.Idle;
        }
    }
}
