using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject foodCan;

    public int hitCount;

    private void Update()
    {
        BossDead();
    }

    public void BossDead()
    {
        if (hitCount == 5)
        {
            gameObject.GetComponent<Animator>().SetBool("isDead", true);
            gameObject.GetComponent<Enemy>().isDead = true;
            foodCan.SetActive(true);
        }
    }
}
