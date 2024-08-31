using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 投げ斧を管理するクラス
public class ThrowedAxe : MonoBehaviour
{
    [SerializeField] float TimeToDestroy = 5f; // 斧が投げられた後、消滅するまでの時間
    float damage; // 投げ斧が与えるダメージ

    void Start()
    {
        Destroy(gameObject, TimeToDestroy); // 一定時間後に投げ斧が消滅するように設定
    }

    private void OnCollisionEnter(Collision other)
    {
        // 投げ斧がプレイヤーに干渉しないようにする
        if (other.gameObject.CompareTag("Player"))
            return;

        // 敵にダメージを与える
        EnemyHealth target = other.gameObject.GetComponent<EnemyHealth>();
        if (target != null)
            target.TakeDamage(damage);

        // 何かに衝突時、投げ斧を破壊する
        Destroy(gameObject);
    }

    // 投げ斧にダメージを設定するメソッド
    public void SetDamage(float axeDamage)
    {
        damage = axeDamage;
    }
}
