using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBladeUpgrade : MonoBehaviour
{
    [Header("Other")]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private FirstPersonController _firstPersonController;

    [Header("Blade Object")]
    [SerializeField] private SwordBladeProjectileBehaviour _blade;
    [SerializeField] private Transform _entityContainer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _cam;

    bool _hasShieldUpgradeTwo;
    bool _shootWhileForceField;
    private void Start() 
    {
        if(PlayerUpgradesData.ShieldTwo)
        {
            _hasShieldUpgradeTwo = true;
        }

        if(PlayerUpgradesData.AttackTwo && PlayerUpgradesData.StarTwo)
        {
            _shootWhileForceField = true;
        }
    }
    
    public void ShootOutBlade()
    {
        if(PlayerUpgradesData.AttackFive)
        {
            Shootblade();
        }
        else if(_shootWhileForceField)
        {
            if(_firstPersonController.AmIForceField)
            {
                Shootblade();
            }
        }
    }

    private void Shootblade()
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

            // bool homing = false;
            // if(KillComboHandler.KillComboCounter >= 125)
            // {
            //     int homingChance = Random.Range(1, 101);
            //     if(homingChance <= 35)
            //     {
            //         homing = true;
            //     }
            // }
            tempBlade.Init(CheckNumOfHits(), false);

            Rigidbody tempBladeRb = tempBlade.GetComponent<Rigidbody>();
            tempBladeRb.AddForce(forceToAdd, ForceMode.Impulse);
        }
    }

    private int CheckNumOfHits()
    {
        if(KillComboHandler.KillComboCounter >= 75)
        {
            return 3;
        }
        else if(KillComboHandler.KillComboCounter >= 40 && _hasShieldUpgradeTwo)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
