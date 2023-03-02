using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUIItemBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _informationObject;

    [Header("Information Data")]
    [SerializeField] private string _weaponName;
    [SerializeField] private string _weaponDescription;


    [Header("Target fields")]
    [SerializeField] private TMP_Text _weaponField; 
    [SerializeField] private TMP_Text _weaponDescriptionField; 

    void Start()
    {
        _weaponField.text = _weaponName;
        _weaponDescriptionField.text = _weaponDescription;
    }

    private void OnMouseEnter() 
    {
        _informationObject.SetActive(true);    
    }

    private void OnMouseExit() 
    {
        _informationObject.SetActive(false);
    }
}
