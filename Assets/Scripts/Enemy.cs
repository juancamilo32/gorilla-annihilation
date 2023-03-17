using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    public int Health { get; set; }

    Player player;

    SpriteRenderer spriteRenderer;
    Animator animator;

    [SerializeField]
    Transform rockSpawn;

    [SerializeField]
    GameObject rockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Health = 3;
        player = FindObjectOfType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine(AttackRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Health > 0)
        {
            FlipSprite();
        }
    }

    public void TakeDamage()
    {
        Health--;
        if (Health < 1)
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 1f);
        }
    }

    void FlipSprite()
    {
        if (player.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
    }

    IEnumerator AttackRoutine()
    {
        while (Health > 0)
        {
            animator.SetTrigger("Attack");
            if (rockSpawn.gameObject.activeSelf)
            {
                Instantiate(rockPrefab, rockSpawn.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(2.5f);
        }
    }

}
