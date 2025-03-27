using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public Image[] hearts; // ����¹���ͨҡ heart �� hearts
    public Sprite redHeart;
    public Sprite grayHeart;

    private HPManager healthManager;

    void Start()
    {
        healthManager = FindObjectOfType<HPManager>();

        if (healthManager != null)
        {
            healthManager.OnHeartChanged += UpdateGameplayUI;
            UpdateGameplayUI(healthManager.health);
        }
        else
        {
            Debug.LogError("HPManager not found in the scene!");
        }
    }

    void UpdateGameplayUI(int currentHeart)
    {
        for (int i = 0; i < hearts.Length; i++) // ����¹�� hearts
        {
            if (i < currentHeart)
                hearts[i].sprite = redHeart;
            else
                hearts[i].sprite = grayHeart;
        }
    }
}
