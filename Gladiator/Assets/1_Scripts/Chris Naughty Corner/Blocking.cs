using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blocking : MonoBehaviour
{
    [Header("Objects")]
    public Transform shieldTransform;
    //public MeshRenderer UnBlockedMesh;
    public GameObject UnBlockedBattery;
    public GameObject UnblockArm;
    //public MeshRenderer BlockedMesh;
    public GameObject BlockedBattery;
    [SerializeField] private GameObject _antiParticleEffect;


    [Header("References")]
    [SerializeField] private PlayerShieldBehaviour playerShieldBehaviour;
    [SerializeField] private PlayerHealth _playerhealth;


    [Header("Control Varaibles")]
    public bool IAmBlocking;
    private bool ShieldLock;
    bool _canBlockElementDmg;

    void Start()
    {
        playerShieldBehaviour.Init(this);
        if(PlayerUpgradesData.ShieldThree)
        {
            _canBlockElementDmg = true;
        }
        else
        {
            _canBlockElementDmg = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && !ShieldLock)
        {
            UnBlockedBattery.SetActive(false);
            UnblockArm.SetActive(false);
            BlockedBattery.SetActive(true);

            _playerhealth.TakeNoDamage = true;
            
            _antiParticleEffect.SetActive(_canBlockElementDmg);
            _playerhealth.TakeNoFireDamage = _canBlockElementDmg;
            _playerhealth.TakeNoPoisonDamage = _canBlockElementDmg;
            
            playerShieldBehaviour._isBlocking = true;
        }
        else
        {   
            //UnBlockedMesh.enabled = true;
            UnBlockedBattery.SetActive(true);
            UnblockArm.SetActive(true);
            //BlockedMesh.enabled = true;
            BlockedBattery.SetActive(false);
            _playerhealth.TakeNoDamage = false;
            _playerhealth.TakeNoPoisonDamage = false;
            //playerCollider.enabled = true;
            _playerhealth.TakeNoFireDamage = false;
            playerShieldBehaviour._isBlocking = false;
        }
    }

    public IEnumerator AllowShieldBlockAgain()
    {
        ShieldLock = true;
        yield return new WaitForSeconds(2f);
        ShieldLock = false;
    }
}
