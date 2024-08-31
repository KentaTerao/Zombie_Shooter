using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのタスク（攻撃）
public class TaskAttack : TutorialTask
{
    public void OnTaskSetting()
    {
    }

    public bool CheckTask()
    {
        bool attack = Input.GetMouseButtonDown(0);

        if (attack)
            return true;

        return false;
    }

    public string GetTitle()
    {
        return "基本操作 攻撃";
    }

    public string GetText()
    {
        return "左クリックで武器による攻撃を行います。";
    }


    public float GetTransitionTime()
    {
        return 2f;
    }
}
