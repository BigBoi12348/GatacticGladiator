using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    public Transform player;
    public float shootingRange = 10f;
    public float shootingDelay = 1f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private float lastShotTime;

    void Start()
    {
        lastShotTime = Time.time;
    }

    void Update()
    {
        // if (Vector3.Distance(transform.position, player.position) < shootingRange && Time.time - lastShotTime > shootingDelay)
        // {
        //     transform.LookAt(player.position);
        //     GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        //     bullet.GetComponent<Rigidbody>().velocity = (player.position - bullet.transform.position).normalized * 10;
        //     lastShotTime = Time.time;
        // }
    }
}
