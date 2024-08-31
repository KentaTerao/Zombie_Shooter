using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アイテムドロップを管理するクラス
public class ItemDrop : MonoBehaviour
{
    // ドロップするアイテムを管理するクラス
    [System.Serializable]
    public class DropItem
    {
        public GameObject itemPrefab = null; // ドロップするアイテムのプレハブ
        [Range(0f, 100f)] public float weight; // ドロップする確率（%）
    }

    [SerializeField] List<DropItem> Items; // 複数のアイテムを登録するリスト

    float totalWeight;

    // 重み付け抽選によるアイテムドロップを行うメソッド
    public void Drop()
    {
        if (Items.Count != 0)
        {
            // 重みの総和計算
            foreach (DropItem item in Items)
            {
                totalWeight += item.weight;
            }

            float randomValue = Random.Range(0f, totalWeight); // 0から重みの総和の間でランダムな値を生成
            SortItemsByDropChance(); // ドロップ確率が低いアイテムから抽選するようにソートする

            float currentTotalWeight = 0f; // 現在のドロップ確率
            foreach (DropItem item in Items)
            {
                // 現在要素までの重みの総和を求める
                currentTotalWeight += item.weight;

                // 乱数値が現在要素の範囲内かチェック
                if (randomValue < currentTotalWeight)
                {
                    // 抽選に当たったアイテムをインスタンス化する
                    if (item.itemPrefab != null)
                        Instantiate(item.itemPrefab, transform.position, Quaternion.identity);

                    return; // ドロップなし(itemPrefabがnull)が当選した場合はそのままreturn
                }
            }
        }
    }

    // アイテムリストをドロップ確率が低い順に並び替えるメソッド
    void SortItemsByDropChance()
    {
        Items.Sort((drop1, drop2) => drop1.weight.CompareTo(drop2.weight));
    }
}
