using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    private GameObject targetObj;
    private bool isPlayerParrent;
    private bool isEnemyParrent;
    private float force, mass, acc;

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

    private void PlayerForceSetting()
    {
        targetObj.GetComponent<Rigidbody>().freezeRotation = false;

        mass = targetObj.GetComponent<Rigidbody>().mass;
        acc = targetObj.GetComponent<Enemy>().speed;
        force = mass * acc;

        targetObj.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
        Debug.Log($"Player - Enemy/Boss : F action = {force}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerParrent)
        {
            if (other.CompareTag("Enemy"))
            {
                targetObj = (GameObject)other.gameObject;
                PlayerForceSetting();

                Player_Controller p = gameObject.GetComponentInParent<Player_Controller>();
                p.GetComponent<Rigidbody>().AddForce(-transform.forward * force, ForceMode.Impulse);
                Debug.Log($"Player - Enemy : F reaction = {force}");

                targetObj.GetComponentInParent<Enemy>().isDead = true;
                targetObj.GetComponentInParent<Enemy>().Dead();
            }

            if (other.CompareTag("Boss"))
            {
                targetObj = (GameObject)other.gameObject;
                PlayerForceSetting();

                Player_Controller p = gameObject.GetComponentInParent<Player_Controller>();
                p.GetComponent<Rigidbody>().AddForce(-transform.forward * force, ForceMode.Impulse);
                Debug.Log($"Player - Boss : F reaction = {force}");

                targetObj.GetComponent<Boss>().hitCount++;

                Debug.Log("Call Freeze Rotate");
                StartCoroutine(ForcePushRoutine(0.5f));
            }
        }
        else if (isEnemyParrent)
        {
            if (other.CompareTag("Player"))
            {
                targetObj = (GameObject)other.gameObject;
                targetObj.GetComponent<Player_Controller>().isTakeDamage = true;
                targetObj.GetComponent<Rigidbody>().freezeRotation = false;

                mass = targetObj.GetComponent<Rigidbody>().mass;
                acc = targetObj.GetComponent<Player_Controller>().speed;
                force = mass * acc;

                targetObj.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
                Debug.Log($"Enemy/Boss - PLayer : F action = {force}");

                Enemy e = gameObject.GetComponentInParent<Enemy>();

                if (e.CompareTag("Enemy"))
                {
                    e.GetComponent<Rigidbody>().AddForce(-transform.forward * force, ForceMode.Impulse);
                    Debug.Log($"Enemy - PLayer : F reaction = {force}");
                }
                else if (e.CompareTag("Boss"))
                {
                    e.GetComponent<Rigidbody>().AddForce(-transform.forward * force, ForceMode.Impulse);
                    Debug.Log($"Boss - PLayer : F reaction = {force}");
                }

                targetObj.GetComponent<HPManager>().TakeDamage(1);

                Debug.Log("Call Freeze Rotate");
                StartCoroutine(ForcePushRoutine(0.5f));


                Debug.Log($"Kick Player");
            }
        }

        Debug.Log(force);
    }

    IEnumerator ForcePushRoutine(float cooldownTime)
    {
        Debug.Log("Disable Freeze Rotate");
        yield return new WaitForSeconds(cooldownTime);
        targetObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        targetObj.GetComponent<Player_Controller>().isTakeDamage = false;
        Debug.Log("Enable Freeze Rotate");
    }
}
