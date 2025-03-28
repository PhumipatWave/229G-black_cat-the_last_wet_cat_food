using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : Humanoid
{
    public AudioClip catWalk;
    public AudioClip catKick;
    public AudioClip jump;
    public AudioClip catDeath;
    public AudioClip win;
    public AudioClip lose;
    public SpawnManager spawner;
    public GameObject winMenu;
    public GameObject loseMenu;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;

    public void Start()
    {
        base.Start();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");

        atkHitBox.gameObject.SetActive(false);

        Debug.Log("Cat Start");
    }

    private void Update()
    {
        if (!isDead)
        {
            CheckGround();
            Jump();
            Movement();
            Attack();

            if (transform.position.y < -30f)
            {
                Dead();
                isDead = true;
            }
        }
        else
        {
            Dead();
        }
    }

    public override void Movement()
    {
        float horizontalInput = moveAction.ReadValue<Vector2>().normalized.x;
        float verticalInput = moveAction.ReadValue<Vector2>().normalized.y;
        Vector3 moveDir = new Vector3(horizontalInput * speed, rb.linearVelocity.y, verticalInput * speed);

        if (horizontalInput != 0f || verticalInput != 0f && !isTakeDamage)
        {
            rb.linearVelocity = moveDir;

            Vector3 lookDir = new Vector3(horizontalInput, 0f, verticalInput);
            Quaternion targetRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            Debug.Log($"Movement directrion : {lookDir}");
            anim.SetFloat("MoveInput", 1);
        }
        else
        {
            anim.SetFloat("MoveInput", 0);
        }
    }

    public void Jump()
    {
        if (jumpAction.triggered && isGround)
        {
            Debug.Log("Jump");
            Vector3 jumpDir = new Vector3(0f, jumpPower, 0f);
            rb.linearVelocity = jumpDir;
            anim.SetTrigger("JumpTrigger");
            PlayOnceSound(jump);
        }
    }

    public override void Attack()
    {
        if (attackAction.triggered && !isAttackCooldown && isGround)
        {
            atkHitBox.gameObject.SetActive(true);
            anim.SetTrigger("AttackTrigger");
            isAttackCooldown = true;
            StartCoroutine(AttackRoutine());
            PlayOnceSound(catKick);
        }
    }

    public override IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(1f);
        atkHitBox.gameObject.SetActive(false);

        yield return new WaitForSeconds(attackCooldown);
        isAttackCooldown = false;
    }

    public override void Dead()
    {
        loseMenu.SetActive(true);
        PlayOnceSound(catDeath);
        PlayOnceSound(lose);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyWave"))
        {
            collision.gameObject.GetComponent<Collider>().enabled = false;
            spawner = collision.gameObject.GetComponent<SpawnManager>();
            spawner.Initialize();
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            anim.SetBool("IsWin", true);
            winMenu.SetActive(true);
            PlayOnceSound(win);
        }
    }
}
