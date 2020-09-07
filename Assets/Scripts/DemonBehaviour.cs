using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonBehaviour : Character
{
    private Animator animator;
    private Vector3 warriorPosition;
    private float distanceToWarrior;
    public Slider healthBar;
    public AudioSource audioSword;
    private bool attackIsReady;
    public Transform pointAttackRight;
    public Transform pointAttackLeft;
    private Transform pointAttack = null;
    public float attackRange;
    private bool warriorIsDead;
    
    void Start()
    {
        InitializeCharacter();

        pointAttack = pointAttackLeft;
        healthBar.maxValue = maxHealth;
        healthBar.value = healthBar.maxValue;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();

        LookAtWarrior();

        Attack();

        Died();
    }

    private void Movement() 
    {
        if (!isDead())
        {
            distanceToWarrior = Vector3.Distance(transform.position, warriorPosition);
            if (distanceToWarrior < 8f && distanceToWarrior > 2f)
            {
                animator.SetBool("Walk", true);
                transform.position = new Vector2(Vector2.MoveTowards(transform.position, warriorPosition, speed * Time.deltaTime).x, transform.position.y);
                preparationTimeAttack = 0;
            }
            else
                animator.SetBool("Walk", false);
        }
    }

    private void Attack() {
        if (distanceToWarrior < 2.5f && attackIsReady && !isDead() && !warriorIsDead)
        {
            preparationTimeAttack += Time.deltaTime;
            if (preparationTimeAttack > preparationAttack)
            {
                animator.SetBool("Attack", true);
                audioSword.Play();
                attackIsReady = false;

                Collider2D[] hitEnemies = null;
                hitEnemies = Physics2D.OverlapCircleAll(pointAttack.position, attackRange);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.name.Equals("Warrior"))
                        enemy.GetComponent<WarriorBehaviour>().TakeDamage(attackDamage);
                }
            }
        }
        else
            animator.SetBool("Attack", false);

        if (!attackIsReady)
        {
            delayTimeAttack += Time.deltaTime;
            if (delayTimeAttack > delayAttack)
            {
                attackIsReady = true;
                preparationTimeAttack = 0;
                delayTimeAttack = 0;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Damage(damage);
        healthBar.value = currentHealth;
    }

    private void OnDrawGizmosSelected()=>
        Gizmos.DrawWireSphere(pointAttack.position, attackRange);

    private void Died()
    {
        if (currentHealth <= 0)
        {
            animator.SetBool("Die", true);
        }
    }

    public void SetWarriorPosition(Vector3 position) =>    
        this.warriorPosition = position;

    public void SetWarriorIsDead(bool isDead) =>
        this.warriorIsDead = isDead;

    public override void InitializeCharacter()
    {
        if (gameObject.name.Equals("Demon"))
        {
            maxHealth = 5;
            currentHealth = maxHealth;
            speed = 2;
            attackDamage = 1;
            delayAttack = 1f;
            preparationAttack = 0.3f;
        }
        if (gameObject.name.Equals("Lizard"))
        {
            maxHealth = 1;
            currentHealth = maxHealth;
            speed = 4;
            attackDamage = 1;
            delayAttack = 1f;
            preparationAttack = 0.1f;
        }
    }

    private void LookAtWarrior()
    {
        if (!isDead())
        {
            if (transform.position.x - warriorPosition.x >= 0.01f)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                pointAttack = pointAttackLeft;
            }
            if (transform.position.x - warriorPosition.x <= -0.01f)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                pointAttack = pointAttackRight;
            }
        }
    }
}
