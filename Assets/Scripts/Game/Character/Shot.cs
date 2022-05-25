using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public int damage { set; get; }
    public Transform target { set; private get; }

    public float speed = 5;

    [SerializeField] private int health;
    [SerializeField, Range(0.1f,4f)] private float radiusHit = 1;
    [SerializeField] GameObject explosionPrefub;

    public void OnTriggerEnter(Collider other)
    {
        if (string.Equals(other.transform.tag, "Enemy"))
        {
            Hurt();
            if (explosionPrefub)
                Instantiate(explosionPrefub, transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (DataManager.targets.Length > 0 && health > 1)
        {
            target = DataManager.targets[0].transform;
        }
    }

    void Hurt()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, radiusHit, DataManager.ENEMY_LAYER_MASK);

        if (targets.Length > 0)
        {
            foreach (Collider target in targets)
            {
                if (string.Equals(target.gameObject.tag, "Enemy"))
                {
                    Enemy enemy = target.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(damage);
                    health--;

                    if (health <= 1)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    void Update()
    {
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        else Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusHit);
    }
}
