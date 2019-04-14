using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moss_Giant : Enemy, IDamageable
{

    public int hp { get; set; }

    //to initalize custom stuff to the moss giant
    public override void Init()
    {
        base.Init();

        //assign total hp to moss
        base.hp = 5;

        //set the current hp to whatever the base class is
        this.hp = base.hp;
    }

    public void Damage()
    {
        //subtract the hp by 1 when the dmg method is called
        hp--;

        //play the hit animation
        animator.SetTrigger("Hit");

        //enemy is now hit
        isHit = true;

        //in combat animator is on
        animator.SetBool("InCombat", true);

        //if hp is less than 1 it means it has 0 hp
        if(hp < 1)
        {
            isDead = true;
            animator.SetTrigger("Death");
            player.totalEnemiesKilled += 1;

        }
    }

    public override void Movement()
    {
        base.Movement();
    }
}
