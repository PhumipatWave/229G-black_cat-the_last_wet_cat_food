using UnityEngine;
using UnityEngine.UI;

public class HPInterface : MonoBehaviour
{
    public Image[] hearts; 
    public Sprite redHeart;
    public Sprite grayHeart;

    private HPManager healthManager;

    void Start()
    {
        healthManager = FindObjectOfType<HPManager>();

        if (healthManager != null)
        {
            healthManager.OnHeartChanged += UpdateHeartsUI;
            UpdateHeartsUI(healthManager.health);
        }
    }

    void UpdateHeartsUI(int currentHeart)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHeart)
                hearts[i].sprite = redHeart;
            else
                hearts[i].sprite = grayHeart;
        }
    }
}