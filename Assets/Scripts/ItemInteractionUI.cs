using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// エクストラアイテムに触れた際に表示されるUIを管理するクラス
public class ItemInteractionUI : MonoBehaviour
{
    GameObject targetItem;
    // 取得ボタンが押されたときに呼ばれるメソッド
    public void OnClickAcquireButton()
    {
        if (targetItem != null)
        {
            // アイテムを取得する処理
            Destroy(targetItem); // アイテムを削除
        }

        gameObject.SetActive(false); // UIを非表示にする
    }

    // 取得しないボタンが押されたときに呼ばれるメソッド
    public void OnClickDoNotAcquireButton()
    {
        gameObject.SetActive(false); // UIを非表示にする
    }

    public void SetTargetItem(GameObject item)
    {
        targetItem = item;
    }
}
