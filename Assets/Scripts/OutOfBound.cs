using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y <= -50f)
        {
            Destroy(gameObject);
        }
    }
}
