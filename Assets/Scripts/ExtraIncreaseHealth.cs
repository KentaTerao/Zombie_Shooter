using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// プレイヤーの最大HPを増加させるアイテム
public class ExtraIncreaseHealth : ExtraItem
{
    [SerializeField] float healthAmount = 100f;

    override protected void Start()
    {
        base.Start();
    }

    public override void SetDescription()
    {
        description =
            "This item increases your max HP by " + healthAmount.ToString();
    }

    public override void Apply()
    {
        // プレイヤーの最大HPを増加させる
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.IncreaseMaxHP(healthAmount);
        playerHealth.HealHP(healthAmount);
        Destroy(gameObject);
    }
}
