using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuPlay : MonoBehaviour
{
    public GameObject hoverSound;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Plays()
    {
        RoundData.Wave = 1;
        RoundData.PlayerPoints = 0;
        RoundData.DifficultyRank = 0;
        RoundData.PlayerMaxHealth = 60;
        ResetPlayerUpgradeData();
        GameManager.Instance.LoadThisScene(GameManager.GAMESCENE);
    }
    public void HoverOver()
    {
        hoverSound.SetActive(true);
    }
    public void HoverOff()
    {
        hoverSound.SetActive(false);
    }

    private void ResetPlayerUpgradeData()
    {
        PlayerUpgradesData.AttackOne = false;
        PlayerUpgradesData.AttackTwo = false;
        PlayerUpgradesData.AttackThree = false;
        PlayerUpgradesData.AttackFour = false;
        PlayerUpgradesData.AttackFive = false;
        PlayerUpgradesData.ShieldOne = false;
        PlayerUpgradesData.ShieldTwo = false;
        PlayerUpgradesData.ShieldThree = false;
        PlayerUpgradesData.ShieldFour = false;
        PlayerUpgradesData.ShieldFive = false;
        PlayerUpgradesData.StarOne = false;
        PlayerUpgradesData.StarTwo = false;
        PlayerUpgradesData.StarThree = false;
        PlayerUpgradesData.StarFour = false;
        PlayerUpgradesData.StarFive = false;
    }
}
