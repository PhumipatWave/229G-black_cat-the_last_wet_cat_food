using System.Collections;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Transform Pos1;
    public Transform Pos2;
    public float speed;

    private Rigidbody rb;
    private Vector3 targetPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPos = Pos2.position;
    }

    private void Update()
    {
        rb.MovePosition(Vector3.MoveTowards(rb.position, targetPos, speed * Time.deltaTime));

        if (Vector3.Distance(rb.position, targetPos) < 0.1f)
        {
            if (targetPos == Pos1.position)
            {
                targetPos = Pos2.position;
            }
            else
            {
                targetPos = Pos1.position;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
