using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour, IDamagable
{
    //custom data type that we can give predefined values
    public enum Team
    {
        Player,
        Enemy
    }

    public string DisplayName;
    public int CurHP;
    public int MaxHP;

    //protected variables like private, cannot be accessed. however protected can be accessed if its part of a subclass instead

    [SerializeField] protected Team team;

    [Header("Audio")]
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip hitSFX;

    public event UnityAction onTakeDamage;
    public event UnityAction onHeal;

    public virtual void TakeDamage(int damageToTake)
    {
        Debug.Log("Took " + damageToTake + " damage");
        CurHP -= damageToTake;

        audioSource.PlayOneShot(hitSFX);

        //the ? is there so that if it is equal to null then itll ignore it, however if it isnt equal to null it will call the event
        onTakeDamage?.Invoke();

        if(CurHP <= 0)
            Die();
    }

    //"virtual" tells the compiler that we have the ability to override this function
    public virtual void Die()
    {
        
    }

    public Team GetTeam()
    {
        return team;
    }

    public virtual void Heal(int healAmount)
    {
        CurHP += healAmount;

        if(CurHP > MaxHP)
            CurHP = MaxHP;

        onHeal?.Invoke();
    }
}
