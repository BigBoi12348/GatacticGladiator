using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBladeUpgrade : MonoBehaviour
{
    [Header("Blade Object")]
    [SerializeField] private GameObject _blade;
    [SerializeField] private Transform _entityContainer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _cam;

    public void ShootOutBlade()
    {
        if(PlayerUpgradesData.AttackAttribute >= 0)
        {
            GameObject tempBlade = Instantiate(_blade, _attackPoint.position, _cam.rotation);

            Rigidbody tempBladeRb = tempBlade.GetComponent<Rigidbody>();

            Vector3 forceToAdd = _cam.transform.forward * 20f;

            tempBladeRb.AddForce(forceToAdd,ForceMode.Impulse);
        }
    }
}
