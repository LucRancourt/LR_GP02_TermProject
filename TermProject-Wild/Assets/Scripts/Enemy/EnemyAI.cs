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
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyAIStates _currentState = EnemyAIStates.Idle;

    [Header("In Range")]
    [SerializeField] private float inRangeRadius = 2.0f;
    [SerializeField] private LayerMask inRangeLayerMask;

    [Header("Line of Sight")]
    [SerializeField] private float losRadius = 2.0f;
    [SerializeField] private float losDistance = 5.0f;
    [SerializeField] private LayerMask losLayerMask;

    [Header("Idle")] 
    [SerializeField] private float timeToPatrol = 5.0f;
    private bool timerToPatrolStarted = false;

    [Header("Patrol")] 
    [SerializeField] private Transform[] patrolPoints;
    private int _currentPatrolPoint = 0;
    [SerializeField] private bool isPatrolRandom = false;
    [SerializeField] private float patrolSpeed = 5.0f;

    [Header("Chase")]
    [SerializeField] private float chaseSpeed = 5.0f;

    [Header("Attack")]
    [SerializeField] private float attackDamage = 5.0f;
    [SerializeField] private float attackDistance = 1.0f;
    [SerializeField] private float attackDelay = 0.5f;
    private bool _hasAttacked = false;

    private bool _playerInRange = false;

    private EnemyAIStates _previousState;
    private GameObject _currentTarget;

    private Animator _animator;

    [Header("NavMeshAgent")]
    private NavMeshAgent _navMeshAgent;



    // Functions
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Is Player in range?
        Collider[] playerHit = Physics.OverlapSphere(transform.position, inRangeRadius, inRangeLayerMask);
        if (playerHit.Length != 0)
        {
            _currentTarget = playerHit[0].gameObject;
            _playerInRange = true;
        }
        else
            _playerInRange = false;

        // Is Player in view?
        if (_playerInRange)
        {
            if (Physics.SphereCast(transform.position, losRadius, transform.forward, out RaycastHit hitInfo,
                losDistance, losLayerMask, QueryTriggerInteraction.Ignore))
                if (hitInfo.transform.gameObject.TryGetComponent(out PlayerController player))
                    if (player != null)
                        _currentState = EnemyAIStates.Chase;
        }
        else if (_currentState == EnemyAIStates.Chase || _currentState == EnemyAIStates.Attack)
            _currentState = EnemyAIStates.Idle;



        switch (_currentState)
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, inRangeRadius);
        Gizmos.DrawWireSphere(transform.position + transform.forward * losDistance, losRadius);
    }

    private void IdleBehaviour()
    {
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
        _currentState = EnemyAIStates.Patrol;
    }

    private void PatrolBehaviour()
    {
        if ((HelpfulFunctions.Approximately(transform.position, patrolPoints[_currentPatrolPoint].position, true)))
        {
            NewPatrolPointTarget();

            _currentState = EnemyAIStates.Idle;
        }
        else
        {
            transform.LookAt(HelpfulFunctions.MoveToWithoutVertical(transform.position, patrolPoints[_currentPatrolPoint].position, patrolSpeed));
            _navMeshAgent.SetDestination(patrolPoints[_currentPatrolPoint].position);
        }
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
        RotateTowardsTarget();


        _navMeshAgent.SetDestination(_currentTarget.transform.position);
        //transform.position = Vector3.MoveTowards(transform.position, _currentTarget.transform.position, chaseSpeed * Time.deltaTime);
        
        float currentDistance = Vector3.Distance(transform.position, _currentTarget.transform.position);

        if (currentDistance <= attackDistance)
        {
            _currentState = EnemyAIStates.Attack;
        }
    }

    private void AttackBehaviour()
    {
        if (_hasAttacked) return;

        _navMeshAgent.SetDestination(transform.position);

        _hasAttacked = true;

        RotateTowardsTarget();

        _currentTarget.TryGetComponent(out IDamageable damageable);

        if (damageable != null)
            damageable.TakeDamage(attackDamage, gameObject);

        StartCoroutine(AttackDelay());
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);

        _currentState = EnemyAIStates.Chase;
        _hasAttacked = false;
    }

    private void RotateTowardsTarget()
    {
        if (_currentTarget)
            transform.LookAt(_currentTarget.transform);
    }
}
