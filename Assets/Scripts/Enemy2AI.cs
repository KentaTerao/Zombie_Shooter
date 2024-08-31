using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2AI : EnemyAI
{
    [Header("Jump")]
    [SerializeField] float hpThreshold = 500f; // HPがこの量削れるごとにジャンプする
    [SerializeField] float jumpDistance = 5f; // プレイヤーの近くにジャンプする際の距離

    [Header("Barrier")]
    [SerializeField] float shieldRadius = 10f; // Enemy1を検知する範囲
    [SerializeField] GameObject shieldEffect; // バリアエフェクトを表示するオブジェクト
    [SerializeField] LineRenderer lineRenderer; // 光の線を描画するためのLineRenderer

    float lastJumpHP; // 最後にジャンプした時のHP
    bool isShielded = false; // バリアの状態を管理するフラグ
    List<EnemyAI> nearbyEnemies = new List<EnemyAI>(); // 周囲のEnemy1を管理するリスト



    override protected void Start()
    {
        base.Start();
        lastJumpHP = enemyHealth.GetHP(); // 初期HPを設定
        shieldEffect.SetActive(false); // バリアエフェクトを初期状態で非表示にする
        lineRenderer.positionCount = 0; // LineRendererの初期化
    }

    override protected void Update()
    {
        // 死後、動かなくする
        if (enemyHealth.GetIsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
            return; // これ以上の処理を行わないようにする
        }

        DetectNearbyEnemies(); // 周囲のEnemy1を検知してバリアを張るかどうかを判定する
        if (isShielded)
        {
            // バリアがある場合はダメージを受けない
            enemyHealth.SetInvincible(true);
            shieldEffect.SetActive(true); // バリアエフェクトを表示
            DrawLinesToEnemies(); // Enemy1と光の線でつなぐ
        }
        else
        {
            // バリアがない場合はダメージを受ける
            enemyHealth.SetInvincible(false);
            shieldEffect.SetActive(false); // バリアエフェクトを非表示
            lineRenderer.positionCount = 0; // 光の線をクリア
        }

        // 検知範囲内にターゲットがいるか、攻撃を受けた際にターゲットを追跡し、攻撃する
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isAware)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isAware = true;
        }

        // HPが一定量減少したらジャンプする
        if (lastJumpHP - enemyHealth.GetHP() >= hpThreshold)
        {
            Jump();
            lastJumpHP = enemyHealth.GetHP(); // ジャンプ後に現在のHPを記録
        }
    }

    // 周囲のEnemy1を検知してバリアを張るかどうかを判定するメソッド
    void DetectNearbyEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, shieldRadius);
        nearbyEnemies.Clear();

        foreach (Collider collider in hitColliders)
        {
            // 自身を検知しないようにする
            if (collider.gameObject == gameObject)
            {
                continue;
            }

            // 死んでいないEnemy1のみを検知する
            EnemyAI enemy1 = collider.GetComponent<EnemyAI>();
            EnemyHealth enemy1Health = collider.GetComponent<EnemyHealth>();
            if (enemy1 != null && enemy1Health != null)
            {
                if (!enemy1Health.GetIsDead())
                    nearbyEnemies.Add(enemy1);
            }
        }

        isShielded = nearbyEnemies.Count > 0;
    }

    // Enemy1と光の線でつなぐ
    private void DrawLinesToEnemies()
    {
        lineRenderer.positionCount = nearbyEnemies.Count * 2;

        for (int i = 0; i < nearbyEnemies.Count; i++)
        {
            lineRenderer.SetPosition(i * 2, transform.position); // Enemy2の位置
            lineRenderer.SetPosition(i * 2 + 1, nearbyEnemies[i].transform.position); // Enemy1の位置
        }
    }

    void Jump()
    {
        animator.SetBool("jump", true);

        // プレイヤーの近くに移動
        Vector3 playerPosition = target.position;
        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;
        Vector3 jumpPosition = playerPosition + directionToPlayer * jumpDistance;

        // 高さを維持しつつ移動
        jumpPosition.y = transform.position.y;

        // 敵の位置をジャンプ後の位置に設定
        transform.position = jumpPosition;

    }
}