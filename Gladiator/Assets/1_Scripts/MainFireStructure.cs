using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFireStructure : MonoBehaviour
{
    [Header("Top Section")]
    [SerializeField] private Transform TopObj;
    [SerializeField] private Vector3 topRotation;
    [SerializeField] private float _topspeed;
    

    [Header("Bottom Section")]
    [SerializeField] private FireLine[] _fireArms;
    [SerializeField] private Transform baseObj;
    [SerializeField] private Vector3 baseRotation;
    [SerializeField] private float basespeed;
    [SerializeField] private Vector2 durationSwitchRange;
    private float _switchTimer;
    private float _lowestSwitchDuration;
    private float _highestSwitchDuration;
    private int _howManyActive;
    

    private void Start() 
    {
        _lowestSwitchDuration = durationSwitchRange.x;
        _highestSwitchDuration = durationSwitchRange.y;

        if(RoundData.Wave > 0) //NEeds to be == 2
        {
            _howManyActive = 2;
        }
        else if(RoundData.Wave == 4)
        {
            _howManyActive = 2;
        }
        else
        {
            _howManyActive = 3;
        }

        SwitchBottomSection();
        _switchTimer = Random.Range(_lowestSwitchDuration, _highestSwitchDuration);
    }

    void Update()
    {
        baseObj.Rotate(baseRotation * basespeed * Time.deltaTime);
        TopObj.Rotate(topRotation * _topspeed * Time.deltaTime);

        if(_switchTimer < 0)
        {
            SwitchBottomSection();
            _switchTimer = Random.Range(_lowestSwitchDuration, _highestSwitchDuration);
        }
        _switchTimer -= Time.deltaTime;
    }

    private void SwitchBottomSection()
    {
        foreach (var firearm in _fireArms)
        {
            firearm.TurnOff();
        }

        int[] alreadyUsedGun = new int[_howManyActive];

        for (int i = 0; i < _howManyActive; i++)
        {
            int ranChoice = 0;
            
            do
            {
                ranChoice = Random.Range(0, _fireArms.Length);  
            } while (CheckForRepeats(ranChoice, alreadyUsedGun));

            _fireArms[ranChoice].TurnOn();
            alreadyUsedGun[i] = ranChoice;
        }
    }

    private bool CheckForRepeats(int x, int[] alreadyChoosen)
    {
        foreach (var uoi in alreadyChoosen)
        {
            if(x == uoi)
            {
                return true;
            }
        }
        return false;
    }
}
