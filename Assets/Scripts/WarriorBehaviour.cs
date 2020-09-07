using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarriorBehaviour : Character
{
    private Animator animator;
    private Vector3 directionMovement;
    public AudioSource audioSword;
    public Transform teleportPosition1;
    public Transform teleportPosition2;
    public Slider healthBar;
    public Transform pointAttackRight; 
    public Transform pointAttacLeft;
    private Transform pointAttack = null;
    public float attackRange;
    private bool attackIsReady = true;

    void Start()
    {
        InitializeCharacter();
        pointAttack = pointAttackRight;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();

        Attack();
        Died();

        print(isDead());
    }

    private void Movement() {
        if (!isDead())
        {
            transform.Translate(directionMovement * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                animator.SetInteger("Idle", 1);
                animator.SetBool("WalkLeft", true);
                directionMovement = Vector2.left;
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                animator.SetBool("WalkLeft", false);
                directionMovement = Vector2.zero;
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                animator.SetInteger("Idle", 0);
                animator.SetBool("WalkRight", true);
                directionMovement = Vector2.right;
            }

            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("WalkRight", false);
                directionMovement = Vector2.zero;
            }

            if (directionMovement == Vector3.left)
                pointAttack = pointAttacLeft;

            if (directionMovement == Vector3.right)
                pointAttack = pointAttackRight;
        }
    }

    private void Attack() 
    {
        if (Input.GetMouseButtonDown(0) && attackIsReady && !isDead())
        {
            animator.SetBool("Attack", true);
            audioSword.Play();
            attackIsReady = false;
            Collider2D[] hitEnemies = null;
            hitEnemies = Physics2D.OverlapCircleAll(pointAttack.position, attackRange);


            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.name.Equals("Demon") || enemy.name.Equals("Lizard"))                
                    enemy.GetComponent<DemonBehaviour>().TakeDamage(1);
            }
        }
        if (!attackIsReady)
        {
            delayTimeAttack += Time.deltaTime;
            if (delayTimeAttack > delayAttack)
            {
                attackIsReady = true;
                print("attack is ready");
                delayTimeAttack = 0;
            }
        }
        if (Input.GetMouseButtonUp(0))
           animator.SetBool("Attack", false);       
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
            animator.SetBool("Died", true);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("TeleportGate-1"))
            transform.position = teleportPosition1.position;

        if (collision.name.Equals("TeleportGate-2"))
            transform.position = teleportPosition2.position;
    }

    public override void InitializeCharacter()
    {
        maxHealth = 3;
        currentHealth = maxHealth;
        speed = 5;
        attackDamage = 1;
        delayAttack = 1f;
    }
}
