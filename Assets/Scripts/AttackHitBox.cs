using Unity.VisualScripting;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public Player_Controller player;

    private bool isPlayerParrent;
    private bool isEnemyParrent;

    private void Start()
    {
        var p = gameObject.GetComponentInParent<Player_Controller>();
        var e = gameObject.GetComponentInParent<Enemy>();

        if (p != null)
        {
            isPlayerParrent = true;
        }
        else if (e != null)
        {
            isEnemyParrent = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerParrent)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(other.GetComponent<Enemy>());
                other.GetComponent<Rigidbody>().freezeRotation = false;
                other.GetComponent<Rigidbody>().AddForce(transform.forward * 25f, ForceMode.Impulse);

                player.spawner.enemyInWave--;

                Debug.Log($"Kick Enemy, Enemy remain : {player.spawner.enemyInWave}");
            }
        }
        else if (isEnemyParrent)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Rigidbody>().AddForce(transform.forward * 25f, ForceMode.Impulse);
                Debug.Log("Kick Player");
            }
        }
    }
}
