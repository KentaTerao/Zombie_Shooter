using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ピストルのダメージを増加させるアイテム
public class ExtraIncreasePistolDamage : ExtraItem
{
    [SerializeField] float damageAmount = 40f;
    Ammo ammoSlots;

    override protected void Start()
    {
        ammoSlots = FindObjectOfType<Ammo>();
        base.Start();
    }

    public override void SetDescription()
    {
        description =
            "This item increases your pistol damage by " + damageAmount.ToString() + "\n"
            + "(Current Damage: " + ammoSlots.GetDamage(AmmoType.PistolAmmo) + ")";
    }

    public override void Apply()
    {
        ammoSlots.IncreaseDamage(AmmoType.PistolAmmo, damageAmount);
        Destroy(gameObject);
    }
}
