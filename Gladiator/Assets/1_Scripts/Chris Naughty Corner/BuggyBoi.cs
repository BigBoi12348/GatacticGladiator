using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuggyBoi : MonoBehaviour
{
    [SerializeField] private GameObject _circleEffect;
    [SerializeField] private GameObject _sparkEffect;

    private void Start() 
    {
        _circleEffect.SetActive(false);
        _sparkEffect.SetActive(false);
    }

    void SpawnCircle()
    {
        _circleEffect.SetActive(true);
    }

    void SpawnSpark()
    {
        SoundManager.Instance.PlaySound3D(SoundManager.Sound.Hammersmash, _circleEffect.transform.position);
        _circleEffect.SetActive(false);
        _sparkEffect.SetActive(true);
    }

    void DespawnSpark()
    {
        _sparkEffect.SetActive(false);
    }
}
