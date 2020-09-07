using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int maxHealth { get; set; }
    public int currentHealth { get; set; }
    public int speed { get; set; }
    public float delayAttack { get; set; }
    public float delayTimeAttack { get; set; }
    public float preparationAttack { get; set; }
    public float preparationTimeAttack { get; set; }
    public int attackDamage { get; set; }
    public void Damage(int damage)=>    
        this.currentHealth -= damage;
    public bool isDead() => 
        (currentHealth<=0) ? true : false;
    public abstract void InitializeCharacter();
}
