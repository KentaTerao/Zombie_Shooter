using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 敵の動きを管理するクラス
public class OnlyWalkEnemyAI : MonoBehaviour
{
    [SerializeField] float walkRadius = 25f; // ランダムに移動する範囲
    [SerializeField] float minIdleTime = 1f; // 最低停止時間
    [SerializeField] float maxIdleTime = 4f; // 最大停止時間
    [SerializeField] float minWalkTime = 3f; // 最低移動時間
    [SerializeField] float maxWalkTime = 7f; // 最大移動時間
    [SerializeField] float turnSpeed = 10f; // 敵の振り向き速度

    NavMeshAgent navMeshAgent; // 敵の移動を管理するAI
    bool isWalking = false; // 敵が移動中かどうかを示すフラグ

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(RandomMovement());
    }

    // ランダムに歩き回る処理を管理するコルーチン
    IEnumerator RandomMovement()
    {
        while (true)
        {
            if (!isWalking)
            {
                // ランダムな位置を取得して移動開始
                Vector3 newPosition = GetRandomPosition();
                navMeshAgent.SetDestination(newPosition);

                FaceTarget(newPosition);

                // 移動中フラグをオンにして、移動時間をランダムに設定
                isWalking = true;
                GetComponent<Animator>().SetBool("move", true);
                float walkTime = Random.Range(minWalkTime, maxWalkTime);
                yield return new WaitForSeconds(walkTime);

                // 移動が完了したら立ち止まる
                navMeshAgent.ResetPath();
                isWalking = false;
                GetComponent<Animator>().SetBool("move", false);
                float idleTime = Random.Range(minIdleTime, maxIdleTime);
                yield return new WaitForSeconds(idleTime);
            }
            yield return null;
        }
    }

    // ランダムな位置を取得するメソッド
    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        return hit.position;
    }

    // 目的地の方向を向かせるメソッド
    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}

