using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBladeUpgrade : MonoBehaviour
{
    [Header("Other")]
    [SerializeField] private Camera fpsCam;
    [Header("Blade Object")]
    [SerializeField] private SwordBladeProjectileBehaviour _blade;
    [SerializeField] private Transform _entityContainer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _cam;

    public void ShootOutBlade()
    {
        if(PlayerUpgradesData.AttackFive)
        {
            bool canShoot = false;
            if(KillComboHandler.KillComboCounter >= 20)
            {
                canShoot = true;
            }
            else
            {
                int ranChance = Random.Range(1,101);
                if(ranChance <= 75)
                {
                   canShoot = true; 
                }
            }

            if(canShoot)
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

                SwordBladeProjectileBehaviour tempBlade = Instantiate(_blade, _attackPoint.position, _cam.rotation);

                Vector3 forceToAdd = direction.normalized * 30f;


                tempBlade.Init(CheckNumOfHits());

                Rigidbody tempBladeRb = tempBlade.GetComponent<Rigidbody>();
                tempBladeRb.AddForce(forceToAdd,ForceMode.Impulse);
            }
        }
    }

    private int CheckNumOfHits()
    {
        if(KillComboHandler.KillComboCounter >= 30)
        {
            return 3;
        }
        else if(KillComboHandler.KillComboCounter >= 10)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
