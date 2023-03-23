using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    BoxCollider2D boxCollider2D;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void LateUpdate()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        if (enemies.Length > 0)
        {
            boxCollider2D.isTrigger = false;
        }
        else
        {
            StartCoroutine(OpenDoorsRoutine());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator OpenDoorsRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        boxCollider2D.isTrigger = true;
    }

}
