using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// プレイヤーのHPを管理するクラス
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHP = 300f; // プレイヤーの最大HP
    [SerializeField] DamageUI damageUI; // ダメージを受けた際に表示するUI

    float maxMaxHP = 9999f; // プレイヤーの最大HPの最大値
    float currentHP; // プレイヤーのHP
    PlayerSoundManager playerSoundManager;

    void Start()
    {
        playerSoundManager = GetComponent<PlayerSoundManager>();
        currentHP = maxHP;
    }

    // プレイヤーがダメージを受けた際に呼び出すメソッド
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        damageUI.OnTakeDamage(); // ダメージを受けた際にUIを表示する
        playerSoundManager.PlayDamageSound(); // ダメージを受けた際のSEを再生する

        // HPが0以下となった場合に死亡時の処理を行う
        if (currentHP <= 0)
        {
            currentHP = 0;
            GetComponent<DeathHandler>().OnPlayerDeath();
        }
    }

    // プレイヤーの現在HPを増加させるメソッド
    public void HealHP(float healAmount)
    {
        // 現在HPが最大HPを上回らないようにする
        if (currentHP + healAmount > maxHP)
            currentHP = maxHP;
        else
            currentHP += healAmount;
    }

    // プレイヤーの現在HPを増加させるメソッド
    public void IncreaseMaxHP(float healthAmount)
    {
        // 現在HPが最大HPを上回らないようにする
        if (maxHP + healthAmount > maxMaxHP)
            maxHP = maxMaxHP;
        else
            maxHP += healthAmount;
    }

    public float GetCurrentHP()
    {
        return currentHP;
    }

    public float GetMaxHP()
    {
        return maxHP;
    }

    // 現在HPが最大HPと同値かどうかを判定するメソッド
    public bool HPIsMax()
    {
        if (currentHP >= maxHP)
            return true;
        else
            return false;
    }
}
