using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBladeUpgrade : MonoBehaviour
{
    [Header("Other")]
    [SerializeField] private Camera fpsCam;
    [Header("Blade Object")]
    [SerializeField] private GameObject _blade;
    [SerializeField] private Transform _entityContainer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _cam;

    public void ShootOutBlade()
    {
        if(PlayerUpgradesData.AttackAttribute >= 0)
        {
            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
            RaycastHit hit;
            
            Vector3 targetPoint;
            if(Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(75);
            }

            Vector3 direction = targetPoint - _attackPoint.position;

            GameObject tempBlade = Instantiate(_blade, _attackPoint.position, _cam.rotation);

            Vector3 forceToAdd = direction.normalized * 20f;

            Rigidbody tempBladeRb = tempBlade.GetComponent<Rigidbody>();
            tempBladeRb.AddForce(forceToAdd,ForceMode.Impulse);
        }
    }
}
