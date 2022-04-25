using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate : MonoBehaviour
{
    public int Heath = 0;
    public int Speed = 0;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector3(0, transform.position.y, 20);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
    }

    public void GetDamage(int _damage)
    {
        Heath -= _damage;
        if (Heath <= 0)
            EnemyDeath();
    }



    public void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
