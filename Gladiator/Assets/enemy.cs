using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public NavMeshAgent glad;
    public Transform player;


    private void Update()
    {
        glad.SetDestination(player.position);
    }
}
