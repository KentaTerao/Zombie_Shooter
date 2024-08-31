using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのタスク（回復アイテム使用）
public class TaskHeal : TutorialTask
{
    public void OnTaskSetting()
    {
        PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(200);
    }

    public bool CheckTask()
    {
        bool heal = Input.GetKey(KeyCode.X);

        if (heal)
            return true;

        return false;
    }

    public string GetTitle()
    {
        return "回復アイテム使用";
    }

    public string GetText()
    {
        return "Xキーで回復アイテムを使用します。";
    }


    public float GetTransitionTime()
    {
        return 2f;
    }
}
