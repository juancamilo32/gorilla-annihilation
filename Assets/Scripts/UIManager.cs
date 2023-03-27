using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

}
