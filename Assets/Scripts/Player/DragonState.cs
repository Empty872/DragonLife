using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DragonState : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float maxHealthPoints;
    public float maxStaminaPoints;
    public float maxHungerPoints;
    public float maxThirstPoints;
    public float maxManaPoints;
    public float healthPoints;
    public float staminaPoints;
    public float hungerPoints;
    public float thirstPoints;
    public float manaPoints;
    public float experience;
    public float attackStaminaCost;
    public float fireAttackManaCost;
    public float moveStaminaCost;
    public float timeToAttack;
    public float timeToFireAttack;
    public float timeToTailAttack;
    public float attackCooldown;
    public float fireAttackCooldown;
    public float tailAttackCooldown;
    public float healthPointsRestore;
    public float staminaPointsRestore;
    public float manaPointsRestore;
    public GameObject head;
    public GameObject torso;
    public GameObject neck;
    public GameObject tail;
    public GameObject leftArm;
    public GameObject leftArm2;
    public GameObject rightArm;
    public GameObject rightArm2;
    public GameObject leftLeg;
    public GameObject leftLeg2;
    public GameObject rightLeg;
    public GameObject rightLeg2;
    public int level;
    public Image backBar;
    public Image healthBar;
    public Image staminaBar;
    public Image hungerBar;
    public Image thirstBar;
    public Image manaBar;
    public GameObject manaBarText;
    public int barLength = 400;
    public Animator anim;
    private static int attackTrigger = Animator.StringToHash("Attack1Trigger");
    private static int tailAttackTrigger = Animator.StringToHash("TailAttackTrigger");
    private static int changeNeckPositionTrigger = Animator.StringToHash("ChangeNeckPositionTrigger1");
    public bool isSleeping;
    public GameObject fireball;
    public GameObject fireballPosition;
    public GameObject clawsAttackArea;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        TailAttack();
        FireAttack();
        if (timeToAttack > 0)
        {
            timeToAttack -= Time.deltaTime;
        }

        if (timeToFireAttack > 0)
        {
            timeToFireAttack -= Time.deltaTime;
        }

        if (timeToTailAttack > 0)
        {
            timeToTailAttack -= Time.deltaTime;
        }

        GetHungry(0.01f);
        GetThirsty(0.01f);
        RestoreHealth(healthPointsRestore);
        RestoreStamina(staminaPointsRestore);
        RestoreMana(manaPointsRestore);
        LookAtMouse();
        if (CanMove())
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // anim.enabled = true;
            if (!isSleeping)
            {
                FallAsleep();
            }
            else
            {
                WakeUp();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        UpdateBar(healthBar, healthPoints, maxHealthPoints);
    }

    public void LooseStamina(float loosedStamina)
    {
        staminaPoints -= loosedStamina;
        UpdateBar(staminaBar, staminaPoints, maxStaminaPoints);
    }

    public void RestoreStamina(float stamina)
    {
        staminaPoints = Mathf.Clamp(staminaPoints + maxStaminaPoints / 100 * stamina * Time.deltaTime, 0,
            maxStaminaPoints);
        UpdateBar(staminaBar, staminaPoints, maxStaminaPoints);
    }

    public void RestoreMana(float mana)
    {
        manaPoints = Mathf.Clamp(manaPoints + maxManaPoints / 100 * mana * Time.deltaTime, 0,
            maxManaPoints);
        UpdateBar(manaBar, manaPoints, maxManaPoints);
    }

    public void LooseMana(float mana)
    {
        manaPoints -= mana;
        UpdateBar(manaBar, manaPoints, maxManaPoints);
    }

    public void UpdateBar(Image bar, float points, float maxPoints)
    {
        var width = barLength / maxPoints;
        bar.rectTransform.sizeDelta = new Vector2(points * width, bar.rectTransform.sizeDelta.y);
    }

    public void GetHungry(float hunger)
    {
        if (hungerPoints > 0)
        {
            hungerPoints -= hunger * Time.deltaTime;
            UpdateBar(hungerBar, hungerPoints, maxHungerPoints);
        }
        else
        {
            TakeDamage(hunger * Time.deltaTime * 10);
        }
    }

    public void GetThirsty(float thirst)
    {
        if (thirstPoints > 0)
        {
            thirstPoints -= thirst * Time.deltaTime;
            UpdateBar(thirstBar, thirstPoints, maxThirstPoints);
        }
        else
        {
            TakeDamage(thirst * Time.deltaTime * 10);
        }
    }

    public void GetExperience(int obtainedExperience)
    {
        experience += obtainedExperience;
        if (level == 1 && experience >= 10 || level == 2 && experience >= 50 || level == 3 && experience >= 200 ||
            level == 4 && experience >= 500 || level == 5 && experience >= 1000)
        {
            LevelUp(level + 1);
        }
    }

    public bool CanAttack(float stamina)
    {
        if (staminaPoints - stamina >= 0 && timeToAttack <= 0 && !isSleeping)
            return true;
        return false;
    }

    public bool CanTailAttack(float stamina)
    {
        if (staminaPoints - stamina >= 0 && timeToTailAttack <= 0 && !isSleeping && level >= 6)
            return true;
        return false;
    }

    public bool CanFireAttack(float mana)
    {
        if (manaPoints - mana >= 0 && timeToFireAttack <= 0 && !isSleeping)
            return true;
        return false;
    }

    public void StartAttack()
    {
        if (level <= 2)
        {
            head.GetComponent<LimbAttack>().canDamage = true;
        }
        else
        {
            clawsAttackArea.GetComponent<Claws>().canAttack = true;
            clawsAttackArea.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void StartTailAttack()
    {
        tail.GetComponent<LimbAttack>().canDamage = true;
    }

    public void StopTailAttack()
    {
        tail.GetComponent<LimbAttack>().canDamage = false;
    }

    public void StopAttack()
    {
        if (level <= 2)
        {
            head.GetComponent<LimbAttack>().canDamage = false;
        }
        else
        {
            clawsAttackArea.GetComponent<Claws>().canAttack = false;
            clawsAttackArea.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void RestoreHealth(float restoredHealth)
    {
        healthPoints = Mathf.Clamp(healthPoints + maxHealthPoints / 100 * restoredHealth * Time.deltaTime, 0,
            maxHealthPoints);
        UpdateBar(healthBar, healthPoints, maxHealthPoints);
    }

    public void Move()
    {
        // var direction = Input.GetAxis("Vertical");
        // gameObject.GetComponent<Rigidbody2D>().velocity =
        //     gameObject.transform.TransformDirection(new Vector2(0, direction) * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
        {
            // gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Time.deltaTime * 100, ForceMode2D.Impulse);
            gameObject.GetComponent<Rigidbody2D>().velocity =
                gameObject.transform.TransformDirection(Vector2.up * speed * Time.deltaTime);
            // var a = gameObject.transform.TransformDirection(Vector2.up);
            // transform.Translate(Vector2.up * speed * Time.deltaTime);
            // transform.Translate(Vector2.up * speed * Time.deltaTime);
            LooseStamina(moveStaminaCost * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity =
                gameObject.transform.TransformDirection(Vector2.down * speed / 3 * Time.deltaTime);
            // transform.Translate(Vector2.down * speed * Time.deltaTime / 3);
            LooseStamina(moveStaminaCost * Time.deltaTime);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public bool CanMove()
    {
        return staminaPoints > 0 && !isSleeping;
    }

    public void LookAtMouse()
    {
        if (isSleeping)
        {
            return;
        }

        var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.rotation =
            Quaternion.Euler(0, 0, (float)(Math.Atan2(difference.y, difference.x) * Mathf.Rad2Deg) - 90);
    }

    public void FallAsleep()
    {
        isSleeping = true;
        healthPointsRestore = 2.4f;
        staminaPointsRestore = 10;
        manaPointsRestore = 6.5f;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
    }

    public void WakeUp()
    {
        isSleeping = false;
        healthPointsRestore = 0.8f;
        staminaPointsRestore = 0;
        manaPointsRestore = 1;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    public void Drink(float water)
    {
        if (isSleeping)
        {
            return;
        }

        thirstPoints = Mathf.Clamp(thirstPoints + water, 0, maxThirstPoints);
        UpdateBar(thirstBar, thirstPoints, maxThirstPoints);
    }

    public void Eat(float food)
    {
        if (isSleeping)
        {
            return;
        }

        hungerPoints = Mathf.Clamp(hungerPoints + food, 0, maxHungerPoints);
        UpdateBar(hungerBar, hungerPoints, maxHungerPoints);
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && CanAttack(attackStaminaCost))
        {
            LooseStamina(attackStaminaCost);
            timeToAttack = attackCooldown;
            anim.SetTrigger(attackTrigger);
        }
    }

    public void TailAttack()
    {
        if (Input.GetKeyDown(KeyCode.A) && CanTailAttack(attackStaminaCost))
        {
            LooseStamina(attackStaminaCost);
            timeToTailAttack = tailAttackCooldown;
            anim.SetTrigger(tailAttackTrigger);
        }
    }

    public void FireAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && CanFireAttack(fireAttackManaCost))
        {
            LooseMana(fireAttackManaCost);
            timeToFireAttack = fireAttackCooldown;
            Instantiate(fireball, fireballPosition.transform.position, fireballPosition.transform.rotation);
        }
    }

    public void LevelUp(int nextLevel)
    {
        if (nextLevel == 2)
        {
            anim.SetTrigger(changeNeckPositionTrigger);
            UpgradeCharacterPoints();
            manaBar.enabled = true;
            manaBarText.SetActive(true);
            backBar.rectTransform.sizeDelta = new Vector2(backBar.rectTransform.sizeDelta.x, 187.5f);
            level = nextLevel;
            experience -= 10;
            // anim.enabled = false;
            torso.transform.localScale = new Vector3(1, 1, 1);
            rightArm.transform.localPosition = new Vector3(0.5f, 0.6f, 0);
            leftArm.transform.localPosition = new Vector3(-0.5f, 0.6f, 0);
            rightLeg.transform.localPosition = new Vector3(0.5f, -0.6f, 0);
            leftLeg.transform.localPosition = new Vector3(-0.5f, -0.6f, 0);
            leftArm.transform.localScale = new Vector3(0.3f, 0.45f, 1);
            rightArm.transform.localScale = new Vector3(0.3f, 0.45f, 1);
            leftLeg.transform.localScale = new Vector3(0.3f, 0.45f, 1);
            rightLeg.transform.localScale = new Vector3(0.3f, 0.45f, 1);
            rightArm2.transform.localPosition = new Vector3(0.7f, 0.95f, 0);
            leftArm2.transform.localPosition = new Vector3(-0.7f, 0.95f, 0);
            rightLeg2.transform.localPosition = new Vector3(0.7f, -0.65f, 0);
            leftLeg2.transform.localPosition = new Vector3(-0.7f, -0.65f, 0);
            leftArm2.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            rightArm2.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            leftLeg2.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            rightLeg2.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            neck.transform.localPosition = new Vector3(0, 1, 0);
            tail.transform.localPosition = new Vector3(0, -1.15f, 0);
            attackTrigger = Animator.StringToHash("Attack2Trigger");
            changeNeckPositionTrigger = Animator.StringToHash("ChangeNeckPositionTrigger2");
        }

        if (nextLevel == 3)
        {
            anim.SetTrigger(changeNeckPositionTrigger);
            UpgradeCharacterPoints();
            level = nextLevel;
            experience -= 50;
            torso.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            rightArm.transform.localPosition = new Vector3(0.87f, 1, 0);
            rightArm.transform.localScale = new Vector3(0.5f, 0.7f, 1);
            leftArm.transform.localPosition = new Vector3(-0.87f, 1, 0);
            leftArm.transform.localScale = new Vector3(0.5f, 0.7f, 1);
            leftLeg.transform.localPosition = new Vector3(-0.87f, -1, 0);
            leftLeg.transform.localScale = new Vector3(0.5f, 0.7f, 1);
            rightLeg.transform.localPosition = new Vector3(0.87f, -1, 0);
            rightLeg.transform.localScale = new Vector3(0.5f, 0.7f, 1);
            rightArm2.transform.localPosition = new Vector3(1.2f, 1.65f, 0);
            leftArm2.transform.localPosition = new Vector3(-1.2f, 1.65f, 0);
            rightLeg2.transform.localPosition = new Vector3(1.2f, -1, 0);
            leftLeg2.transform.localPosition = new Vector3(-1.2f, -1, 0);
            leftArm2.transform.localScale = new Vector3(-0.5f, 0.6f, 1);
            rightArm2.transform.localScale = new Vector3(0.5f, 0.6f, 1);
            leftLeg2.transform.localScale = new Vector3(-0.5f, -0.54f, 1);
            rightLeg2.transform.localScale = new Vector3(0.5f, -0.54f, 1);
            neck.transform.localPosition = new Vector3(0, 1.5f, 0);
            neck.transform.localScale = new Vector3(0.5f, 0.6f, 1);
            head.transform.localPosition = new Vector3(0, 1.7f, 0);
            head.transform.localScale = new Vector3(2, 1.3f, 1);
            tail.transform.localPosition = new Vector3(0, -1.7f, 0);
            tail.transform.localScale = new Vector3(0.5f, 4, 1);
            fireballPosition.transform.localPosition = new Vector3(0, 3.3f, 0);
            attackTrigger = Animator.StringToHash("Attack3Trigger");
        }

        if (nextLevel == 4)
        {
            UpgradeCharacterPoints();
            level = nextLevel;
            experience -= 200;
            gameObject.transform.localScale = new Vector3(1, 1, 0);
            tail.transform.localScale = new Vector3(1, 4, 1);
            fireball = (GameObject)Resources.Load("FireBreath");
        }

        if (nextLevel == 5)
        {
            UpgradeCharacterPoints();
            level = nextLevel;
            experience -= 500;
            tail.transform.localScale = new Vector3(1.4f, 6, 1);
        }

        FullRestore();
    }

    public void FullRestore()
    {
        healthPoints = maxHealthPoints;
        staminaPoints = maxStaminaPoints;
        hungerPoints = maxHungerPoints;
        thirstPoints = maxThirstPoints;
        manaPoints = maxManaPoints;
        UpdateBar(staminaBar, staminaPoints, maxStaminaPoints);
        UpdateBar(manaBar, manaPoints, maxManaPoints);
    }

    public void UpgradeCharacterPoints()
    {
        maxHealthPoints += 10;
        maxStaminaPoints += 10;
        maxHungerPoints += 10;
        maxThirstPoints += 10;
        maxManaPoints += 10;
    }
}