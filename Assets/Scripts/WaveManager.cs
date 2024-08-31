using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [HideInInspector] public int currentWave = 0; // 現在ウェーブ番号
    [HideInInspector] public bool isWaveActive = false; // ウェーブ中であるかどうかを示すフラグ
    [HideInInspector] public float waveStartTime; // ウェーブ開始時刻
    [HideInInspector] public float waveEndTime; // ウェーブ終了時刻

    public float waveDuration = 20f; // ウェーブの持続時間
    public float timeBetweenWaves = 5f; // ウェーブ間の待機時間

    [SerializeField] ExtraItemManager extraItemManager; // ItemManagerの参照
    [SerializeField] GameObject enemyPrefab; // 出現する敵のプレハブ
    [SerializeField] Transform player; // プレイヤーのTransform
    [SerializeField] float spawnInterval = 2f; // 敵の出現間隔
    [SerializeField] float minSpawnDistance = 10f; // プレイヤーと敵出現位置の最小距離
    [SerializeField] float maxSpawnDistance = 20f; // プレイヤーと敵出現位置の最大距離
    [SerializeField] int baseHealth = 100; // 敵の基本体力
    [SerializeField] int healthIncrement = 10; // ウェーブごとに増加する体力

    List<GameObject> enemies = new List<GameObject>(); // スポーンした敵を管理するリスト;

    void Start()
    {
        // ゲーム開始時にウェーブをスタートする
        StartCoroutine(StartWaves());
    }

    // ウェーブを開始するコルーチン
    IEnumerator StartWaves()
    {
        while (true) // 無限ループでウェーブを管理
        {
            currentWave++; // ウェーブを進める
            isWaveActive = true; // ウェーブフラグを立てる
            waveStartTime = Time.time; // ウェーブの開始時間を記録

            // 敵を出現させ続ける
            yield return StartCoroutine(SpawnWave());

            isWaveActive = false; // ウェーブフラグを下げる
            waveEndTime = Time.time; // ウェーブの終了時間を記録

            DestroyAllEnemies(); // ウェーブ終了時に全ての敵を破壊
            extraItemManager.SpawnItems(); // ウェーブクリア時にアイテムを生成

            // ウェーブとウェーブの間の待機時間
            yield return new WaitForSeconds(timeBetweenWaves);

            extraItemManager.ClearItems(); // 未取得のアイテムを破壊
        }
    }

    // 現在のウェーブで敵を出現させ続けるコルーチン
    IEnumerator SpawnWave()
    {
        // ウェーブの開始時刻を記録
        float startTime = Time.time;

        // ウェーブが指定時間続く間、敵を出現させ続ける
        while (Time.time - startTime < waveDuration)
        {
            SpawnEnemy(); // 敵を出現させる
            yield return new WaitForSeconds(spawnInterval); // 敵の出現間隔を待つ
        }
    }

    // 敵を指定された条件で出現させるメソッド
    void SpawnEnemy()
    {
        // ランダムな位置に敵をスポーンさせる
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // ウェーブごとに増加する体力を設定する
        enemy.GetComponent<EnemyHealth>().SetHP(baseHealth + currentWave * healthIncrement);

        // 敵をリストに追加
        enemies.Add(enemy);

        // 敵が破壊されたときにリストから削除するためのコールバックを設定
        enemy.GetComponent<EnemyHealth>().OnDestroyed += () => RemoveEnemyFromList(enemy);
    }

    // プレイヤーから一定距離A以上B以下の範囲でランダムな位置を取得するメソッド
    Vector3 GetRandomSpawnPosition()
    {
        // ランダムな方向を計算（X-Z平面）
        float randomAngle = Random.Range(0f, 360f);
        Vector3 direction = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle)).normalized;

        // ランダムな距離を取得（minSpawnDistanceからmaxSpawnDistanceの間）
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

        // 敵の出現位置を計算
        Vector3 spawnPosition = player.position + direction * randomDistance;

        // 地形が存在する場合、その地形の高さを反映する
        spawnPosition.y = Terrain.activeTerrain.SampleHeight(spawnPosition);
        return spawnPosition;
    }

    // 敵が破壊されたときにリストから削除するメソッド
    void RemoveEnemyFromList(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    // 全ての敵オブジェクトを破壊するメソッド
    void DestroyAllEnemies()
    {
        // すべての敵オブジェクトを破壊
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
                Destroy(enemy);
        }

        // リストをクリアする
        enemies.Clear();
    }
}
