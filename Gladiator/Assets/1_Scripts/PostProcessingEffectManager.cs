using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingEffectManager : MonoBehaviour
{
    public static PostProcessingEffectManager Instance;
    //[SerializeField] private PostProcessVolume _volume;
    [SerializeField] private GameObject _hurtVolume;
    [SerializeField] private GameObject _dashEffect;
    [SerializeField] private GameObject _burnEffect;
    private LensDistortion lensDistortion;
    Coroutine hurtCO;
    Coroutine dashCO;
    Coroutine burnCO;

    private void Awake() => Instance = this;

    private void Start() 
    {
        _hurtVolume.SetActive(false);
    }

    public void HurtEffect(float flashRedDuration)
    {
        if(hurtCO != null)
        {
            StopCoroutine(hurtCO);
        }
        hurtCO = StartCoroutine(DoHurtEffect(flashRedDuration));
    }

    private IEnumerator DoHurtEffect(float flashRedDuration)
    {
        _hurtVolume.SetActive(true);
        yield return new WaitForSeconds(flashRedDuration);
        _hurtVolume.SetActive(false);
    }

    public void DashEffect(float durationTilTurnOff)
    {
        if(dashCO != null)
        {
            StopCoroutine(dashCO);
        }
        dashCO = StartCoroutine(DoDashEffect(durationTilTurnOff));
    }

    private IEnumerator DoDashEffect(float durationTilTurnOff)
    {
        _dashEffect.SetActive(true);
        yield return new WaitForSeconds(durationTilTurnOff);
        _dashEffect.SetActive(false);
    }

    public void BurnEffect(float durationTilTurnOff)
    {
        if(burnCO != null)
        {
            StopCoroutine(burnCO);
        }
        burnCO = StartCoroutine(DoBurnEffect(durationTilTurnOff));
    }

    private IEnumerator DoBurnEffect(float durationTilTurnOff)
    {
        _burnEffect.SetActive(true);
        yield return new WaitForSeconds(durationTilTurnOff);
        _burnEffect.SetActive(false);
    }
}
