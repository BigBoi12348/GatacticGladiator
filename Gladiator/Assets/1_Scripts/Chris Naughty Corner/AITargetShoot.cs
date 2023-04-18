using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITargetShoot : MonoBehaviour
{
    public Transform player;
    public LayerMask floor, target;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public float sightRange, attackRange;
    public Transform attackPoint;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 20)
        {
            playerInSightRange = true;
        }
        else
        {
            playerInSightRange = false;
        }

        if (Vector3.Distance(transform.position , player.position) < 20)
        {
            playerInAttackRange = true;
        }
        else
        {
            playerInAttackRange = false;
        }
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, target);
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, target);

        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

   private void AttackPlayer()
    {
        

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.Euler(0, 90, 0)).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
