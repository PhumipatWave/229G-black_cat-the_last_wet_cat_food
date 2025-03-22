using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : Humanoid
{
    public GameObject atkHitBox;
    public SpawnManager spawner;

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
        CheckGround();
        Jump();
        Movement();
        Attack();
    }

    public override void Movement()
    {
        float horizontalInput = moveAction.ReadValue<Vector2>().normalized.x;
        float verticalInput = moveAction.ReadValue<Vector2>().normalized.y;
        Vector3 moveDir = new Vector3(horizontalInput * speed, rb.linearVelocity.y, verticalInput * speed);
        rb.linearVelocity = moveDir;

        if (horizontalInput != 0f || verticalInput != 0f)
        {
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
        }
    }

    public override void Attack()
    {
        if (attackAction.triggered && isGround)
        {
            atkHitBox.gameObject.SetActive(true);
            anim.SetTrigger("AttackTrigger");
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(1f);
        atkHitBox.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyWave"))
        {
            spawner = collision.gameObject.GetComponent<SpawnManager>();
            spawner.Initialize();
        }
    }
}
