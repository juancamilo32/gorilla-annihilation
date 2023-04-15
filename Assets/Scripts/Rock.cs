using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    Player player;
    bool canHit = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * 10f;
        StartCoroutine(CollisionRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy") && !other.CompareTag("Room") && !other.CompareTag("Rock") && canHit)
        {
            IDamageable hit = other.GetComponent<IDamageable>();
            if (hit != null)
            {
                hit.TakeDamage();
            }
            Destroy(gameObject);
        }
    }

    IEnumerator CollisionRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        canHit = true;
    }

}
