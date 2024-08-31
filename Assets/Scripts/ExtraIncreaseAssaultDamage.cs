using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アサルトライフルのダメージを増加させるアイテム
public class ExtraIncreaseAssaultDamage : ExtraItem
{
    [SerializeField] float damageAmount = 10f;
    Ammo ammoSlots;

    override protected void Start()
    {
        ammoSlots = FindObjectOfType<Ammo>();
        base.Start();
    }

    public override void SetDescription()
    {
        description =
            "This item increases your assault damage by " + damageAmount.ToString() + "\n"
            + "(Current Damage: " + ammoSlots.GetDamage(AmmoType.AssaultAmmo) + ")";
    }

    public override void Apply()
    {
        ammoSlots.IncreaseDamage(AmmoType.AssaultAmmo, damageAmount);
        Destroy(gameObject);
    }
}
