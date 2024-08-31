using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アサルトライフルの装弾数を増加させるアイテム
public class ExtraIncreaseAssaultMagazine : ExtraItem
{
    [SerializeField] int magazineAmount = 15;
    Ammo ammoSlots;

    override protected void Start()
    {
        base.Start();
        ammoSlots = FindObjectOfType<Ammo>();
    }

    public override void SetDescription()
    {
        description =
            "This item increases your assault magazine by " + magazineAmount.ToString();
    }

    public override void Apply()
    {
        ammoSlots.IncreaseMagazineAmmo(AmmoType.AssaultAmmo, magazineAmount);
        Destroy(gameObject);
    }
}
