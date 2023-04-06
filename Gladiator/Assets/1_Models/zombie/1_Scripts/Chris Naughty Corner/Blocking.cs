using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blocking : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerShieldBehaviour playerShieldBehaviour;
    [SerializeField] private PlayerHealth _playerhealth;


    [Header("Chris Art")]
    public Transform shieldTransform;
    //public MeshRenderer UnBlockedMesh;
    public GameObject UnBlockedBattery;
    //public MeshRenderer BlockedMesh;
    public GameObject BlockedBattery;
    public Collider playerCollider;


    [Header("Control Varaibles")]
    public bool IAmBlocking;
    private bool ShieldLock;

    void Start()
    {
        playerShieldBehaviour.Init(this);
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && !ShieldLock)
        {
            //UnBlockedMesh.enabled = false;
            UnBlockedBattery.SetActive(false);
            BlockedBattery.SetActive(true);
            //BlockedMesh.enabled = true;
            //playerCollider.enabled = false;
            _playerhealth.TakeNoDamage = true;
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
            playerShieldBehaviour._isBlocking = false;
        }

        // if(IAmBlocking)
        // {

        // }
    }

    public IEnumerator AllowShieldBlockAgain()
    {
        ShieldLock = true;
        yield return new WaitForSeconds(2f);
        ShieldLock = false;
    }
}
