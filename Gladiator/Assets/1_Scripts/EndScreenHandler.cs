using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenHandler : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator _endScreenAnim;
    [SerializeField] private AnimationClip _endScreenOpenClip;
    const string OPEN_ENDSCREEN = "Game Over";
    bool canPress;


    [Header("Texts")]
    [SerializeField] private TMP_Text _totalKillsText;
    [SerializeField] private TMP_Text _totalFireCompanionKillsText;
    [SerializeField] private TMP_Text _highestComboRetainedText;
    [SerializeField] private TMP_Text _totalDamageTakenText;
    [SerializeField] private TMP_Text _totalDamageHealedText;
    [SerializeField] private TMP_Text _totalTimePlayedText;
    [SerializeField] private TMP_Text _highestWaveReachedText;
    [SerializeField] private TMP_Text _totalUpgradesGottenText;
    [SerializeField] private TMP_Text _totalCreditsEarnedText;

    void Start()
    {
        canPress = false;
    }
    
    private void OnEnable() 
    {
        GameEvents.playerFinsihedGame += DisplayResults;
        canPress = false;
    }

    private void OnDisable() 
    {
        GameEvents.playerFinsihedGame -= DisplayResults;
    }

    private void DisplayResults(bool state)
    {
        if(!state)
        {
            _totalKillsText.text = PlayerRoundStats.TotalEnemiesKilled.ToString();
            _totalFireCompanionKillsText.text = PlayerRoundStats.FireCompanionKills.ToString();
            _highestComboRetainedText.text = PlayerRoundStats.HighestComboRetained.ToString();
            _totalDamageTakenText.text = PlayerRoundStats.DamageTaken.ToString();
            _totalDamageHealedText.text = PlayerRoundStats.DamageHealed.ToString();
            _totalTimePlayedText.text = PlayerRoundStats.TimePlayed.ToString();
            _highestWaveReachedText.text = PlayerRoundStats.HighestComboRetained.ToString();
            _totalUpgradesGottenText.text = PlayerRoundStats.UpgradesGotten.ToString();
            _totalCreditsEarnedText.text = PlayerRoundStats.CreditsEarned.ToString();
            _endScreenAnim.Play(OPEN_ENDSCREEN);
            StartCoroutine(delayFinishGame());
        }
    }

    IEnumerator delayFinishGame()
    {
        canPress = false;
        yield return new WaitForSecondsRealtime(_endScreenOpenClip.length);
        while (Input.anyKey != true)
        {
            yield return null;
        }
        GameManager.Instance.LoadThisScene(GameManager.MAINMENUSCENE);
    }
}
