using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DifferentAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    public string type;
    public Animator anim;
    private static readonly int AttackAnim = Animator.StringToHash("Attack");
    public GameObject weaponSprite;
    public UnityEngine.Object weapon;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShootWeapon()
    {
        Instantiate(weapon, weaponSprite.transform.position, weaponSprite.transform.rotation);
    }

    public void Push()
    {
    }

    public void Attack()
    {
        if (type is "Goblin" or "Elf")
        {
            ShootWeapon();
        }

        if (type is "Slime" or "Golem")
        {
            anim.SetTrigger(AttackAnim);
        }
    }
}