using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ピストルの装弾数を増加させるアイテム
public class ExtraIncreasePistolMagazine : ExtraItem
{
    [SerializeField] int magazineAmount = 6;
    Ammo ammoSlots;

    override protected void Start()
    {
        base.Start();
        ammoSlots = FindObjectOfType<Ammo>();
    }

    public override void SetDescription()
    {
        description =
            "This item increases your pistol magazine by " + magazineAmount.ToString();
    }

    public override void Apply()
    {
        ammoSlots.IncreaseMagazineAmmo(AmmoType.PistolAmmo, magazineAmount);
        Destroy(gameObject);
    }
}
