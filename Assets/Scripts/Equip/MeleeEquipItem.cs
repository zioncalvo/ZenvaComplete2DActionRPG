using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEquipItem : EquipItem
{
    [SerializeField] private LayerMask hitLayerMask;
    [SerializeField] private Animator anim;
    private float lastAttackTime;

    [SerializeField] private AudioClip swingSFX;

    public override void OnUse()
    {
        MeleeWeaponItemData i = item as MeleeWeaponItemData;

        if(Time.time - lastAttackTime < i.AttackRate)
            return;

        lastAttackTime = Time.time;

        
        anim.SetTrigger("Attack");


        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, i.Range, hitLayerMask);

        if(hit.collider != null)
        {
            IDamagable damagable = hit.collider.GetComponent<IDamagable>();

            if(damagable != null)
            {
                Debug.Log("its detecting");
                damagable.TakeDamage(i.Damage);
            }

        }

    }
}
