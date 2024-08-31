using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ウェーブ終了時に出現するアイテムを管理するクラス
public class ExtraItemManager : MonoBehaviour
{
    [SerializeField] GameObject[] itemPrefabs; // 複数種類のアイテムプレハブを格納する配列
    [SerializeField] int numberOfItems = 3; // 出現させるアイテムの数
    [SerializeField] Transform playerTransform; // プレイヤーのTransform
    [SerializeField] float spawnDistance = 2f; // アイテムをプレイヤーの前に出現させる距離
    [SerializeField] float spacing = 1.5f; // アイテム間の距離（間隔）
    [SerializeField] GameObject interactUI;

    List<GameObject> spawnedItems = new List<GameObject>(); // 出現したアイテムのリスト

    // ウェーブ終了時にアイテムを生成するメソッド
    public void SpawnItems()
    {
        ClearItems(); // 既存のアイテムをクリア

        // 全体の幅を計算
        float totalWidth = (numberOfItems - 1) * spacing;
        Vector3 startPosition = playerTransform.position + playerTransform.forward * spawnDistance - playerTransform.right * (totalWidth / 2);

        for (int i = 0; i < numberOfItems; i++)
        {
            // ランダムなアイテムを選択
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject selectedItem = itemPrefabs[randomIndex];

            // プレイヤーの前にアイテムを生成
            // アイテムを横並びに配置
            Vector3 spawnPosition = startPosition + playerTransform.right * (i * spacing);
            GameObject spawnedItem = Instantiate(selectedItem, spawnPosition, Quaternion.identity);

            // リストに生成したアイテムを追加
            spawnedItems.Add(spawnedItem);
        }
    }

    // 次のウェーブ開始時に未取得のアイテムを破壊するメソッド
    public void ClearItems()
    {
        foreach (GameObject item in spawnedItems)
        {
            if (item != null)
            {
                Destroy(item); // 未取得のアイテムを破壊
            }
        }
        spawnedItems.Clear(); // リストをクリア
    }

    public GameObject GetInteractUI()
    {
        return interactUI;
    }
}
