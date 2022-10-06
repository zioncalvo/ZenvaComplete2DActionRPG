using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEquipItem : EquipItem
{
    //variable to know where to spawn projectile it is using
    [SerializeField] private Transform muzzle;
    [SerializeField] private AudioClip shootSFX;
    private float lastAttackTime;

    public override void OnUse()
    {
        RangedWeaponItemData i = item as RangedWeaponItemData;

        if(Time.time - lastAttackTime < i.FireRate)
            return;

        if(Inventory.Instance.HasItem(i.ProjectileItemData) == false)
            return;

        lastAttackTime = Time.time;

        i.Fire(muzzle.position,muzzle.rotation, Character.Team.Player);

        Inventory.Instance.RemoveItem(i.ProjectileItemData);

        AudioManager.Instance.PlayPlayerSound(shootSFX);
    }
}
