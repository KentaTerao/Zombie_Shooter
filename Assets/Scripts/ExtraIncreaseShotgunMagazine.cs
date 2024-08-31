using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ショットガンの装弾数を増加させるアイテム
public class ExtraIncreaseShotgunMagazine : ExtraItem
{
    [SerializeField] int magazineAmount = 3;
    Ammo ammoSlots;

    override protected void Start()
    {
        base.Start();
        ammoSlots = FindObjectOfType<Ammo>();
    }

    public override void SetDescription()
    {
        description =
            "This item increases your shotgun magazine by " + magazineAmount.ToString();
    }

    public override void Apply()
    {
        ammoSlots.IncreaseMagazineAmmo(AmmoType.ShotgunAmmo, magazineAmount);
        Destroy(gameObject);
    }
}
