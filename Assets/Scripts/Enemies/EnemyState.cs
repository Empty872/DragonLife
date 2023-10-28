using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyState : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float speed;
    public Animator anim;
    private static readonly int TakeDamageAnim = Animator.StringToHash("TakeDamage");
    private float timeToAttack;
    public float cooldown;
    public float maxHeathPoints;
    public float healthPoints;
    public int experience;
    public UnityEngine.Object deathExplosion;
    public EnemyAttackArea attackArea;
    public EnemyBody enemyBody;
    public DifferentAttacks differentAttacks;
    public Rigidbody2D rigidbody;

    void Start()
    {
        // rigidbody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Dragon");
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToAttack > 0)
        {
            timeToAttack -= Time.deltaTime;
        }

        if (!enemyBody.touchCamera)
        {
            RestoreHealth();
        }

        if (enemyBody.touchCamera && !attackArea.touchPlayer)
        {
            Move();
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
            ;
        }

        Attack();
    }

    public void Move()
    {
        var pos = transform.position;
        var difference = ((Vector2)player.transform.position - (Vector2)pos).normalized;
        // transform.position +=
        //     new Vector3(difference.x, difference.y, 0) * speed * Time.deltaTime;
        // transform.Translate(difference * speed * Time.deltaTime, Space.World);
        // transform.Translate();
        // Debug.Log(difference);
        rigidbody.velocity = difference * speed * Time.deltaTime;
        // transform.TransformDirection(difference * speed * Time.deltaTime);
        // Debug.Log(rigidbody.velocity);
        // gameObject.GetComponentInParent<Transform>().position = rigidbody.gameObject.transform.position;
        // Debug.Log(transform.position);

        // rigidbody.velocity = Vector2.left;
        // gameObject.transform.Translate(rigidbody.velocity);
    }

    public void Attack()
    {
        if (timeToAttack <= 0 && attackArea.touchPlayer)
        {
            differentAttacks.Attack();
            timeToAttack = cooldown;
        }
    }

    public void DealDamage(float damage)
    {
        player.GetComponent<DragonState>().TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        anim.SetTrigger(TakeDamageAnim);
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        var deathExplosionObject = (GameObject)Instantiate(deathExplosion);
        deathExplosionObject.transform.position = transform.position;
        Destroy(gameObject);
        player.GetComponent<DragonState>().GetExperience(experience);
    }

    public void RestoreHealth()
    {
        if (healthPoints < maxHeathPoints)
        {
            healthPoints = Mathf.Clamp(healthPoints + maxHeathPoints / 10 * Time.deltaTime, 0, maxHeathPoints);
        }
    }
}