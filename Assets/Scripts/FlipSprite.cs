using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{

    Player player;
    SpriteRenderer spriteRenderer;
    IDamageable hit;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hit = GetComponent<IDamageable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit != null)
        {
            if (hit.Health > 0 && player)
            {
                Flip();
            }
        }

    }

    void Flip()
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

}
