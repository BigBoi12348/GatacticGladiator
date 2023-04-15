using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFireStructure : MonoBehaviour
{
    [Header("Top Section")]
    [SerializeField] private Transform[] _shootingPoints;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private Transform TopObj;
    [SerializeField] private Vector3 _topRotation;
    [SerializeField] private float _topRotationSpeed;
    [SerializeField] private Vector2 durationTopSwitchRange;
    private float _topSwitchTimer;
    private float _lowestTopSwitchDuration;
    private float _highestTopSwitchDuration;
    private int _howManyFireBallsActive;


    [Header("Bottom Section")]
    [SerializeField] private FireLine[] _fireArms;
    [SerializeField] private Transform baseObj;
    [SerializeField] private Vector3 _bottomRotation;
    [SerializeField] private float bottomRotationSpeed;
    [SerializeField] private Vector2 durationBottomSwitchRange;
    private float _bottomSwitchTimer;
    private float _lowestBottomSwitchDuration;
    private float _highestBottomSwitchDuration;
    private int _howManyFireLanesActive;
    
    private void Start() 
    {
        _lowestBottomSwitchDuration = durationBottomSwitchRange.x;
        _highestBottomSwitchDuration = durationBottomSwitchRange.y;

        _lowestTopSwitchDuration = durationTopSwitchRange.x;
        _highestTopSwitchDuration = durationTopSwitchRange.y;

        if(RoundData.Wave > 0) //NEeds to be == 2
        {
            _howManyFireLanesActive = 1;
            _howManyFireBallsActive = 2;
        }
        else if(RoundData.Wave == 4)
        {
            _howManyFireLanesActive = 2;
            _howManyFireBallsActive = 3;
        }
        else
        {
            _howManyFireLanesActive = 3;
            _howManyFireBallsActive = 4;
        }

        SwitchBottomSection();
        _bottomSwitchTimer = Random.Range(_lowestBottomSwitchDuration, _highestBottomSwitchDuration);
        _topSwitchTimer = Random.Range(_lowestTopSwitchDuration, _highestTopSwitchDuration);
    }

    void Update()
    {
        baseObj.Rotate(_bottomRotation * bottomRotationSpeed * Time.deltaTime);
        TopObj.Rotate(_topRotation * _topRotationSpeed * Time.deltaTime);

        if(_bottomSwitchTimer < 0)
        {
            SwitchBottomSection();
            _bottomSwitchTimer = Random.Range(_lowestBottomSwitchDuration, _highestBottomSwitchDuration);
        }
        _bottomSwitchTimer -= Time.deltaTime;


        if(_topSwitchTimer < 0)
        {
            ShootFireBalls();
            _topSwitchTimer = Random.Range(_lowestTopSwitchDuration, _highestTopSwitchDuration);

        }
        _topSwitchTimer -= Time.deltaTime;
    }

    private void ShootFireBalls()
    {
        int[] alreadyUsedGun = new int[_howManyFireBallsActive];

        for (int i = 0; i < _howManyFireBallsActive; i++)
        {
            int ranChoice = 0;
            
            do
            {
                ranChoice = Random.Range(0, _shootingPoints.Length);  
            } while (CheckForRepeats(ranChoice, alreadyUsedGun));

            GameObject newProjectile = Instantiate(_projectilePrefab, _shootingPoints[ranChoice].position, _shootingPoints[ranChoice].rotation) as GameObject;
            Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>(); 
            projectileRigidbody.velocity = _shootingPoints[ranChoice].transform.forward * _projectileSpeed;

            alreadyUsedGun[i] = ranChoice;
        }
    }

    private void SwitchBottomSection()
    {
        foreach (var firearm in _fireArms)
        {
            firearm.TurnOff();
        }

        int[] alreadyUsedLanes = new int[_howManyFireLanesActive];

        for (int i = 0; i < _howManyFireLanesActive; i++)
        {
            int ranChoice = 0;
            
            do
            {
                ranChoice = Random.Range(0, _fireArms.Length);  
            } while (CheckForRepeats(ranChoice, alreadyUsedLanes));

            _fireArms[ranChoice].TurnOn();
            alreadyUsedLanes[i] = ranChoice;
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
