using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IDamageable
{

    public int hp { get; set; }

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite openSprite, closeSprite;

    public bool isOpen = false;

    public void Start()
    {
        hp = 5;
    }

    public void Damage()
    {
        //if chest is open do nothing
        if (isOpen)
        {

        }
        else
        {
            hp--;
            if(hp < 1)
            {
                isOpen = true;
                spriteRenderer.sprite = openSprite;
            }
        }

    }
}
