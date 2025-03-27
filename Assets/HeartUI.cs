using UnityEngine;
using UnityEngine.UI;

public class HPInterface : MonoBehaviour
{
    public Image[] hearts;
    public Sprite redHeart;
    public Sprite grayHeart;

    private HPManager heart;

    void Start()
    {
        heart = FindObjectOfType<HPManager>();

        if (heart != null)
        {
            heartManager.OnHeartChanged += UpdateHeartUI;
            UpdateHeartUI(heartManager.heart);
        }
    }

    void UpdateHeartUI(int currentHeart)
    {
        for (int i = 0; i < heart.Length; i++)
        {
            if (i < currentHeart)
                hearts[i].sprite = redHeart;
            else
                hearts[i].sprite = grayHeart;
        }
    }
}
