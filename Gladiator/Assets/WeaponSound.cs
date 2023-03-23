using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    public void SwordSingSoundEffect()
    {
        SoundManager.Instance.PlaySound3D(SoundManager.Sound.PlayerAttack, transform.position);
    }
}
