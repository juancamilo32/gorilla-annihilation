using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField]
    GameObject bossPrefab;
    Player player;
    Vector3 spawnPoint;
    GameObject boss;
    bool animationEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnBoss();
        }
        if (boss != null)
        {
            if (boss.transform.position.y > transform.position.y && !animationEnded)
            {
                boss.transform.position += Vector3.down * Time.deltaTime * 4.2f;
            }
        }
    }

    void SpawnBoss()
    {

        if (player.transform.position.x > transform.position.x || player.transform.position.y > transform.position.y)
        {
            //Spawn Left
            spawnPoint = transform.position + new Vector3(-6f, 7f, 0);
        }
        else
        {
            //Spawn right
            spawnPoint = transform.position + new Vector3(6f, 7f, 0);
        }

        boss = Instantiate(bossPrefab, spawnPoint, Quaternion.identity);
    }

}
