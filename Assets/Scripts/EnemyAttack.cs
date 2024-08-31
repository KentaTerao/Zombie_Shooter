using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵の攻撃を管理するクラス
public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target; // 攻撃時にダメージを与える対象
    [SerializeField] float damage = 40f; // ダメージ量

    void Start()
    {
        // 現在、敵がダメージを与える対象はプレイヤーのみ
        // プレイヤー以外にもダメージを与えられるようにするにはtargetの仕様を変更する
        target = FindObjectOfType<PlayerHealth>();
    }

    // 攻撃ヒット時に呼び出されるメソッド
    public void AttackHitEvent()
    {
        if (target == null)
            return;

        target.TakeDamage(damage); // ターゲットのダメージ処理を行うメソッドを呼び出す
    }
}
