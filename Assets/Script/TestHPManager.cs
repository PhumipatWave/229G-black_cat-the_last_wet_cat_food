using UnityEngine;
using System;

public class HPManager : MonoBehaviour
{
    public int heart = 3;
    private int maxHeart;

    public event Action<int> OnHeartChanged;

    void Start()
    {
        maxHeart = 3;
    }

    public void TakeDamage(int damage)
    {
        heart -= damage;
        heart = Mathf.Clamp(heart, 0, maxHeart);
        OnHeartChanged?.Invoke(heart);
    }
}