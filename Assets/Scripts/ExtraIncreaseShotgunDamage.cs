using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ショットガンのダメージを増加させるアイテム
public class ExtraIncreaseShotgunDamage : ExtraItem
{
    [SerializeField] float damageAmount = 100f;
    Ammo ammoSlots;

    override protected void Start()
    {
        ammoSlots = FindObjectOfType<Ammo>();
        base.Start();
    }

    public override void SetDescription()
    {
        description =
            "This item increases your shotgun damage by " + damageAmount.ToString() + "\n"
            + "(Current Damage: " + ammoSlots.GetDamage(AmmoType.ShotgunAmmo) + ")";
    }

    public override void Apply()
    {
        ammoSlots.IncreaseDamage(AmmoType.ShotgunAmmo, damageAmount);
        Destroy(gameObject);
    }
}
