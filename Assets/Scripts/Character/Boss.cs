using UnityEngine;

public class Boss : MonoBehaviour
{
    public int hitCount;

    private GameObject foodCan;

    private void Start()
    {
        foodCan = GameObject.Find("Can");
        Debug.Log($"Find can tag : {foodCan}");
    }

    private void Update()
    {
        BossDead();
    }

    public void BossDead()
    {
        if (hitCount == 5)
        {
            foodCan.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Animator>().SetBool("isDead", true);
            gameObject.GetComponent<Enemy>().isDead = true;
            foodCan.SetActive(true);
        }
    }
}
