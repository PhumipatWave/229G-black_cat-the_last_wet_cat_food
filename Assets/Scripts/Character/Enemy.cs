using System.Collections;
using UnityEngine;

public class Enemy : Humanoid
{
    public AudioClip ratWalk;
    public AudioClip ratHit;
    public AudioClip ratDie;

    public AudioClip catWalk;
    public AudioClip catKick;
    public AudioClip catDeath;

    public AudioClip currentWalk;
    public AudioClip currentAttack;
    public AudioClip currentDeath;

    public bool haveRunAnim;
    public float attatckLenght;

    private Player_Controller player;

    public void Start()
    {
        base.Start();
        Debug.Log($"{nameof(gameObject)} Start");

        player = FindAnyObjectByType<Player_Controller>();

        if (gameObject.tag == "Enemy")
        {
            currentWalk = ratWalk;
            currentAttack = ratHit;
            currentDeath = ratDie;
        }
        else if (gameObject.tag == "Boss")
        {
            currentWalk = catWalk;
            currentAttack = catKick;
            currentDeath = catDeath;
        }

        atkHitBox.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isDead && !isAttackCooldown)
        {
            Movement();
            Attack();

            if (transform.position.y < -30f)
            {
                Dead();
                isDead = true;
            }
        }
    }

    public override void Movement()
    {
        if (!isAttackCooldown && !isDead)
        {
            Vector3 moveDir = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
            rb.transform.position = Vector3.MoveTowards(this.transform.position, moveDir, speed * Time.deltaTime);

            Vector3 lookDir = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(lookDir);

            PlayOnceSound(currentWalk);

            if (haveRunAnim)
            {
                anim.SetFloat("MoveSpeed", 1);
            }
        }
        else
        {
            anim.SetFloat("MoveSpeed", 0);
        }
    }

    public override void Attack()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < attatckLenght && !isAttackCooldown)
        {
            atkHitBox.gameObject.SetActive(true);
            anim.SetTrigger("AttackTrigger");
            isAttackCooldown = true;
            StartCoroutine(AttackRoutine());
            PlayOnceSound(currentAttack);
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
        player.spawner.enemyInWave--;
        Destroy(this.gameObject, 5f);
        PlayOnceSound(currentDeath);
        Debug.Log($"Kick Enemy, Enemy remain : {player.spawner.enemyInWave}");
    }
}
