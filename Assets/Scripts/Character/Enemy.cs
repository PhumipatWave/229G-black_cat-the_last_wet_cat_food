using UnityEngine;

public class Enemy : Humanoid
{
    private Player_Controller player;

    public void Start()
    {
        base.Start();
        Debug.Log("Rat Start");

        player = FindAnyObjectByType<Player_Controller>();
    }

    void Update()
    {
        Vector3 moveDir = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        rb.transform.position = Vector3.MoveTowards(this.transform.position, moveDir, speed * Time.deltaTime);

        Vector3 lookDir = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        transform.LookAt(lookDir);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Movement()
    {
        throw new System.NotImplementedException();
    }
}
