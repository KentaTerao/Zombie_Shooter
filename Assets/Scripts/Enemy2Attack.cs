using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵(Enemy2)の攻撃を管理するクラス
public class Enemy2Attack : EnemyAttack
{
    // ジャンプ終了時にフラグをオフにする
    public void JumpEnd()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("jump", false);
    }
}
