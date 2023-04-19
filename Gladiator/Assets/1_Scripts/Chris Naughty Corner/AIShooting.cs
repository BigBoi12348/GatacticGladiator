using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIShooting : MonoBehaviour
{
    //public Transform player;
    //public float shootingRange = 10f;
    //public float shootingDelay = 1f;
    //public GameObject bulletPrefab;
    //public Transform bulletSpawnPoint;

    //private float lastShotTime;

    //void Start()
    //{
    //    lastShotTime = Time.time;
    //}

    //void Update()
    //{
    //    if (Vector3.Distance(transform.position, player.position) < shootingRange && Time.time - lastShotTime > shootingDelay)
    //    {
    //        transform.LookAt(player.position);
    //        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    //        bullet.GetComponent<Rigidbody>().velocity = (player.position - bullet.transform.position).normalized * 10;
    //        lastShotTime = Time.time;
    //    }
    //}

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    //public float health;
    //public Vector3 walkPoint;
    //bool walkPointSet;
    //public float walkPointRange;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //if (!playerInSightRange && !playerInAttackRange) Patroling();
        //if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    //private void Patroling()
    //{
    //    if (!walkPointSet) SearchWalkPoint();

    //    if (walkPointSet)
    //        agent.SetDestination(walkPoint);

    //    Vector3 distanceToWalkPoint = transform.position - walkPoint;
    //    if (distanceToWalkPoint.magnitude < 1f)
    //        walkPointSet = false;
    //}
    //private void SearchWalkPoint()
    //{
    //    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //    float randomX = Random.Range(-walkPointRange, walkPointRange);

    //    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //    if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
    //        walkPointSet = true;
    //}

    //private void ChasePlayer()
    //{
    //    agent.SetDestination(player.position);
    //}

    private void AttackPlayer()
    {
       agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //public void TakeDamage(int damage)
    //{
    //    health -= damage;

    //    if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    //}
    //private void DestroyEnemy()
    //{
    //    Destroy(gameObject);
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
