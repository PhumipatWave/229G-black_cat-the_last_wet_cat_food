using UnityEngine;

public abstract class Humanoid : MonoBehaviour
{
    public LayerMask groundLayer;
    public GameObject groundCheck;

    public Rigidbody rb;
    public Animator anim;

    public float speed;
    public float turnSpeed;
    public float jumpPower;
    public bool isGround;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        Debug.Log("Humanoid Initialized!");
    }

    public abstract void Movement();
    public abstract void Attack();

    public void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.transform.position, -transform.up, out hit, 0.1f, groundLayer))
        {
            Debug.Log($"On Ground");
            isGround = true;
        }
        else
        {
            Debug.Log($"On Air");
            isGround = false;
        }
        Debug.DrawRay(groundCheck.transform.position, Vector3.down, Color.red);
    }
}
