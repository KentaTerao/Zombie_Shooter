using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵のHPを管理するクラス
public class EnemyHealth : MonoBehaviour
{
    // 敵が破壊されたときに呼び出されるイベント
    public event Action OnDestroyed;

    [SerializeField] ItemDrop itemDrop;
    [SerializeField] float HP = 100f; // 敵のHP
    [SerializeField] int scoreAmount = 100; // 敵死亡時に取得できるスコア
    [SerializeField] float TimeToDestroy = 60f; // 敵の死後、オブジェクトが消滅するまでの時間

    ScoreManager scoreManager;
    EnemySE enemySE;
    bool isDead = false; // 死んでいるかどうかを示すフラグ（死亡フラグ）
    bool isInvincible = false; // バリアが張られているかどうかを示すフラグ

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        enemySE = GetComponent<EnemySE>();
    }

    // HPが0以下となった際の処理を行うメソッド
    void Die()
    {
        if (isDead)
            return;

        isDead = true; // 死亡フラグを立てる
        gameObject.layer = LayerMask.NameToLayer("DeadEnemy"); // 死亡した敵を特定のレイヤーに変更
        scoreManager.IncreaseScore(scoreAmount); // スコアを加算
        gameObject.GetComponent<CapsuleCollider>().isTrigger = true; // 死後、干渉できなくする

        Animator animator = GetComponent<Animator>();
        if (animator == null) // Enemy2は子オブジェクトにanimatorがアタッチされている
            animator = GetComponentInChildren<Animator>();

        animator.SetTrigger("die"); // 死亡時のアニメーションに移行

        itemDrop.Drop(); // ドロップアイテムを抽選する
        Destroy(gameObject, TimeToDestroy); // 死後、一定時間経過後にオブジェクトを破壊
    }

    // 攻撃された際にダメージの分HPを減らすメソッド
    public void TakeDamage(float damage)
    {
        // バリアが張られている場合はダメージを受けない
        if (isInvincible)
            return;

        BroadcastMessage("OnDamageTaken"); // ダメージを受けた際の処理を呼び出す
        PlayDamagedSound();
        HP -= damage;
        if (HP <= 0)
        {
            OnDestroyed?.Invoke(); // イベントを呼び出し
            Die(); // HPが0以下となった際の処理を呼び出す
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public float GetHP()
    {
        return HP;
    }
    public void SetHP(float hpAmount)
    {
        HP = hpAmount;
    }

    public void SetInvincible(bool invincible)
    {
        isInvincible = invincible;
    }

    void PlayDamagedSound()
    {
        if (enemySE != null)
            enemySE.PlayZombieShotSE();
    }
}
