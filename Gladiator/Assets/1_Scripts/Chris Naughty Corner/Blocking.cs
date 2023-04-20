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
    //public MeshRenderer BlockedMesh;
    public GameObject BlockedBattery;
    [SerializeField] private GameObject _antiParticleEffect;


    [Header("References")]
    [SerializeField] private PlayerShieldBehaviour playerShieldBehaviour;
    [SerializeField] private PlayerHealth _playerhealth;


    [Header("Control Varaibles")]
    public bool IAmBlocking;
    private bool ShieldLock;
    bool _canBlockFire;

    void Start()
    {
        playerShieldBehaviour.Init(this);
        if(PlayerUpgradesData.ShieldThree)
        {
            _canBlockFire = true;
        }
        else
        {
            _canBlockFire = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && !ShieldLock)
        {
            UnBlockedBattery.SetActive(false);
            BlockedBattery.SetActive(true);

            _playerhealth.TakeNoDamage = true;
            
            _antiParticleEffect.SetActive(_canBlockFire);
            _playerhealth.TakeNoFireDamage = _canBlockFire;
            
            playerShieldBehaviour._isBlocking = true;
        }
        else
        {   
            //UnBlockedMesh.enabled = true;
            UnBlockedBattery.SetActive(true);
            //BlockedMesh.enabled = true;
            BlockedBattery.SetActive(false);
            _playerhealth.TakeNoDamage = false;
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
