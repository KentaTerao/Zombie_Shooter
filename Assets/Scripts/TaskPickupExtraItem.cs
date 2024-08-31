using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのタスク（エクストラアイテム取得）
public class TaskPickupExtraItem : TutorialTask
{
    GameObject items;
    GameObject extraItems;

    public void OnTaskSetting()
    {
        items = GameObject.Find("Items");
        extraItems = items.transform.Find("ExtraItems").gameObject;
        extraItems.SetActive(true);
    }

    public bool CheckTask()
    {
        // 配下の子オブジェクト(アイテム)がすべて破壊されている(取得されている)か確認する
        if (extraItems != null && extraItems.transform.childCount == 0)
            return true;

        return false;
    }

    public string GetTitle()
    {
        return "ドロップアイテム取得";
    }

    public string GetText()
    {
        return "ウェーブ終了まで生き残ると、エクストラアイテムが出現します。\n"
                + "エクストラアイテムは触れることで説明が閲覧可能です。\n"
                + "また、触れながら左shiftキーを押すことで取得します。\n"
                + "各ウェーブごとに1つのみ取得可能です。";
    }


    public float GetTransitionTime()
    {
        return 2f;
    }
}
