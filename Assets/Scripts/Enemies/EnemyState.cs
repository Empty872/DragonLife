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

    void Start()
    {
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

        Attack();
    }

    public void Move()
    {
        var pos = transform.position;
        var difference = ((Vector2)player.transform.position - (Vector2)pos).normalized;
        transform.position += new Vector3(difference.x, difference.y, 0) * speed * Time.deltaTime;
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