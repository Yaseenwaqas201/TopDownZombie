using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public Text startTimerTxt;
    public Transform player;
    public static GamePlayManager gameplayinstance;
    public Text gemsNo;
    public Text waveNoTxt;
    [Header("Reference for ")] 
    public GameObject panelsParent;
    public GameObject gameOverPanel;
    public GameObject skillUpgradePanel;
    

    private void Awake()
    {
        gameplayinstance = this;
        EventManager.gameOverEvent += GameOverEvent;
        EventManager.updateGameEconomy += UpdateGem;
        EventManager.waveCompletedEvent += WaveCompleted;
        UpdateGem();
        Time.timeScale = 0;
        AnimateTimer();
    }

    public void AnimateTimer()
    {
        int timer=3;
        StartCoroutine(AnimateTimerC());
        IEnumerator AnimateTimerC()
        {
            while (timer>0)
            {
                startTimerTxt.text = timer + "";
                startTimerTxt.gameObject.transform.DOScale(Vector3.zero, 0.5f).SetUpdate(true).SetEase(Ease.OutBack);
                yield return new WaitForSecondsRealtime(0.6f);
                timer--;
                startTimerTxt.gameObject.transform.localScale=Vector3.one;
            }
            startTimerTxt.gameObject.SetActive(false);

            Time.timeScale = 1;
        }
        

    }

    private void OnDestroy()
    {
        EventManager.gameOverEvent -= GameOverEvent;
        EventManager.updateGameEconomy -= UpdateGem;
        EventManager.waveCompletedEvent -= WaveCompleted;
    }

    public void WaveCompleted()
    {
        Time.timeScale = 1;
        GameConstant.WaveNo += 1;
        waveNoTxt.text = "WAVE "+GameConstant.WaveNo;
        PopUpPanel(skillUpgradePanel);
    }


    public void GameOverEvent()
    {
        Time.timeScale = 0;
        panelsParent.SetActive(true);
        PopUpPanel(gameOverPanel);
    }

    public void UpdateGem()
    {
        gemsNo.text = GameConstant.TotalGems.ToString();
    }

    public void PopUpPanel(GameObject panel)
    {
        panel.transform.parent.gameObject.SetActive(true);
        panel.SetActive(true);
        panel.transform.localScale=Vector3.zero;
        panel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);

    }

    public void PopOutPanel(GameObject panel)
    {
        panel.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            panel.transform.parent.gameObject.SetActive(false);
            panel.SetActive(false);
        });
    }



    public void ClickOnWeaponUpgrade()
    {
        EventManager.InvokeWeaponUpgrade();
        PopOutPanel(skillUpgradePanel);
        Time.timeScale = 1;
    }

    public void ClickOnRangeUpgrade()
    {
        EventManager.InvokeRangeUpgrade();
        PopOutPanel(skillUpgradePanel);
        Time.timeScale = 1;
    }

    public void HealthUpgrade()
    {
        EventManager.InvokeHealthUpgrade();
        PopOutPanel(skillUpgradePanel);
        Time.timeScale = 1;
    }


    public void WarrierBladeUpgrade()
    {
        PopOutPanel(skillUpgradePanel);
        EventManager.InvokeWarrierBladeUpgrade();
        Time.timeScale = 1;
    }

    public void ReplayGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitFromGame()
    {
        Application.Quit();
    }
}
