using UnityEngine;
using System.Collections;

public class AITargeting : MonoBehaviour
{
    public Transform target;
    public float shootInterval = 2.0f;
    public float bulletSpeed = 10.0f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private float timeSinceLastShot = 0.0f;

    void Update()
    {
        if (target != null)
        {
            Vector3 targetDir = target.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);

            if (angle < 180.0f)
            {
                timeSinceLastShot += Time.deltaTime;

                if (timeSinceLastShot >= shootInterval)
                {
                    Shoot();
                    timeSinceLastShot = 0.0f;
                }
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        Vector3 shootDirection = (target.position - bulletSpawnPoint.position).normalized;
        bulletRigidbody.velocity = shootDirection * bulletSpeed;
    }
}