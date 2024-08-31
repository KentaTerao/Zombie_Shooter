using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのタスク（ドロップアイテム取得）
public class TaskPickupItem : TutorialTask
{
    GameObject items;
    GameObject pickupItems;

    public void OnTaskSetting()
    {
        items = GameObject.Find("Items");
        pickupItems = items.transform.Find("PickupItems").gameObject;
        pickupItems.SetActive(true);
    }

    public bool CheckTask()
    {
        // 配下の子オブジェクト(5つのアイテム)がすべて破壊されている(取得されている)か確認する
        if (pickupItems != null && pickupItems.transform.childCount == 0)
            return true;

        return false;
    }

    public string GetTitle()
    {
        return "ドロップアイテム取得";
    }

    public string GetText()
    {
        return "敵を倒すと、弾薬や回復アイテムがドロップすることがあります。\n"
                + "ドロップアイテムは触れることで取得可能です。";
    }


    public float GetTransitionTime()
    {
        return 2f;
    }
}
