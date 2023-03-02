using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUIItemBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _informationObject;
    [SerializeField] private GameObject _hoverEffectObject;

    [Header("Information Data")]
    [SerializeField] private string _weaponName;
    [SerializeField] private string _weaponDescription;


    [Header("Target fields")]
    [SerializeField] private TMP_Text _weaponField; 
    [SerializeField] private TMP_Text _weaponDescriptionField; 

    void Start()
    {
        //_weaponField.text = _weaponName;
        //_weaponDescriptionField.text = _weaponDescription;
    }

    private void OnMouseEnter() 
    {
        _informationObject.SetActive(true);    
        _hoverEffectObject.SetActive(true);
    }

    private void OnMouseExit() 
    {
        _informationObject.SetActive(false);
        _hoverEffectObject.SetActive(false);
    }
}
