using System.Collections;
using UnityEngine;

public abstract class Humanoid : MonoBehaviour
{
    public LayerMask groundLayer;
    public GameObject groundCheck;
    public GameObject atkHitBox;
    public Rigidbody rb;
    public Animator anim;
    public AudioSource audioSource;

    public float speed;
    public float turnSpeed;
    public float jumpPower;
    public float attackCooldown;
    public bool isGround;
    public bool isAttackCooldown;
    public bool isTakeDamage;
    public bool isDead;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        Debug.Log("Humanoid Initialized!");
    }

    public abstract void Movement();
    public abstract void Attack();
    public abstract void Dead();

    public virtual void PlayOnceSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.transform.position, -transform.up, out hit, 0.5f, groundLayer))
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

    public abstract IEnumerator AttackRoutine();
}
