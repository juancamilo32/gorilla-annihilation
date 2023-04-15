using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{

    bool sliding = false;

    public int Health { get; set; }

    FlipSprite flipSprite;
    Animator animator;
    Player player;
    Rigidbody2D rigidBody;
    [SerializeField]
    Transform rockSpawn;

    [SerializeField]
    GameObject rockPrefab;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        Health = 50;
        flipSprite = GetComponent<FlipSprite>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
        UIManager.Instance.ShowHideHealthBar(true);
        StartCoroutine(AttackRotation());
    }

    // Update is called once per frame
    void Update()
    {
        if (sliding)
        {
            flipSprite.enabled = false;
        }
        else
        {
            flipSprite.enabled = true;
        }
        if (dead)
        {
            UIManager.Instance.ActivateVictoryScreen();
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.RestartGame();
            }
        }
    }

    public void TakeDamage()
    {
        Health--;
        UIManager.Instance.UpdateBossHealth(Health);
        if (Health < 1)
        {
            rigidBody.velocity = Vector3.zero;
            StartCoroutine(DeathRoutine());
            animator.SetTrigger("Die");
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1f);
        dead = true;
    }

    IEnumerator AttackRotation()
    {
        yield return new WaitForSeconds(2f);
        while (Health > 0 && player)
        {
            yield return new WaitForSeconds(1f);
            int option = Random.Range(0, 2);
            if (option == 0)
            {
                Slide();
                yield return new WaitForSeconds(5f);
            }
            else
            {
                Shoot();
                yield return new WaitForSeconds(5f);
            }
        }

    }

    void Slide()
    {
        StartCoroutine(SlideRoutine());
    }

    void Shoot()
    {
        StartCoroutine(ShootRoutine());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.collider.CompareTag("Wall") || other.collider.CompareTag("Door") || other.collider.CompareTag("Replacement")) && sliding)
        {
            rigidBody.velocity = Vector3.zero;
            sliding = false;
        }
        else if (other.collider.CompareTag("Player"))
        {
            IDamageable hit = other.collider.GetComponent<IDamageable>();
            if (hit != null)
            {
                hit.TakeDamage();
            }
        }
    }

    IEnumerator SlideRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            sliding = true;
            animator.SetBool("Sliding", true);
            rigidBody.velocity = (player.transform.position - transform.position).normalized * 15f;
            yield return new WaitForSeconds(1f);
        }
        animator.SetBool("Sliding", false);
        sliding = false;
    }

    IEnumerator ShootRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.2f);
            if (rockSpawn.gameObject.activeSelf)
            {
                GameObject rock = Instantiate(rockPrefab, rockSpawn.transform.position, Quaternion.identity);
                rock.transform.localScale *= 2;
            }
        }
    }

}
