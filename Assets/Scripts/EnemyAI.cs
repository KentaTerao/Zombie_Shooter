using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 敵の動きを管理するクラス
public class EnemyAI : MonoBehaviour
{
    [SerializeField] protected float chaseRange = 5f; // 敵の検知範囲 
    [SerializeField] protected float turnSpeed = 5f; // 敵の振り向き速度
    [SerializeField] protected bool isAware = false; // 敵がターゲットに気付いているかを示すフラグ

    protected Transform target; // 敵が攻撃する対象
    protected NavMeshAgent navMeshAgent; // 敵の移動を管理するAI
    protected float distanceToTarget = Mathf.Infinity; // ターゲット（プレイヤー）までの距離
    protected EnemyHealth enemyHealth; // 敵のHPを管理するスクリプト
    protected Animator animator; // アニメーションを管理するコンポーネント

    virtual protected void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        target = FindObjectOfType<PlayerHealth>().transform;
    }

    virtual protected void Update()
    {
        // 死後、動かなくする
        if (enemyHealth.GetIsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
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
    }

    // 敵が攻撃を受けた際の挙動を制御するメソッド
    public void OnDamageTaken()
    {
        isAware = true;
    }

    // 攻撃対象との距離に応じて敵の挙動を制御するメソッド
    protected void EngageTarget()
    {
        FaceTarget(); // ターゲットの方向を向く
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget(); ;
        }
    }

    // 攻撃対象を追わせるメソッド
    void ChaseTarget()
    {
        animator.SetBool("attack", false);
        animator.SetBool("move", true);
        navMeshAgent.SetDestination(target.position);
    }

    // 攻撃対象を攻撃させるメソッド
    void AttackTarget()
    {
        animator.SetBool("attack", true);
    }

    // 攻撃対象の方向を向かせるメソッド
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    // 検知範囲をビジュアル化するメソッド
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
