using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("UI Manager is null.");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    GameObject[] hearts;
    [SerializeField]
    GameObject deathScreen;
    [SerializeField]
    GameObject victoryScreen;
    [SerializeField]
    Slider bossHealthBar;

    public void UpdateHealthUI(int health)
    {
        if (health >= 0)
        {
            hearts[health].SetActive(false);
        }
    }

    public void ActivateDeathScreen()
    {
        CanvasGroup canvasGroup = deathScreen.GetComponent<CanvasGroup>();
        if (canvasGroup)
        {
            canvasGroup.alpha += Time.deltaTime * 2;
        }
        ShowHideHealthBar(false);
    }

    public void UpdateBossHealth(int health)
    {
        bossHealthBar.value = health;
    }

    public void ShowHideHealthBar(bool setactive)
    {
        bossHealthBar.gameObject.SetActive(setactive);
    }

    public void ActivateVictoryScreen()
    {
        CanvasGroup canvasGroup = victoryScreen.GetComponent<CanvasGroup>();
        if (canvasGroup)
        {
            canvasGroup.alpha += Time.deltaTime * 2;
        }
        ShowHideHealthBar(false);
    }

}
