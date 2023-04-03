using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{

    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Health = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage()
    {

    }

}
