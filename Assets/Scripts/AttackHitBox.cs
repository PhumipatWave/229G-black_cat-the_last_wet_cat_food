using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    private GameObject targetObj;
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
                targetObj = (GameObject)other.gameObject;
                targetObj.GetComponent<Rigidbody>().freezeRotation = false;
                targetObj.GetComponent<Rigidbody>().AddForce(transform.forward * 25f, ForceMode.Impulse);

                targetObj.GetComponentInParent<Enemy>().isDead = true;
                targetObj.GetComponentInParent<Enemy>().Dead();
            }

            if (other.CompareTag("Boss"))
            {
                targetObj = (GameObject)other.gameObject;
                targetObj.GetComponent<Rigidbody>().freezeRotation = false;
                targetObj.GetComponent<Rigidbody>().AddForce(transform.forward * 25f, ForceMode.Impulse);

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
                targetObj.GetComponent<Rigidbody>().AddForce(transform.forward * 25f, ForceMode.Impulse);

                Debug.Log("Call Freeze Rotate");
                StartCoroutine(ForcePushRoutine(0.5f));

                Debug.Log($"Kick Player");
            }
        }
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
