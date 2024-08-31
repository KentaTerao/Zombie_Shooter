using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのタスク（ズーム）
public class TaskZoom : TutorialTask
{
    public void OnTaskSetting()
    {
    }

    public bool CheckTask()
    {
        bool zoom = Input.GetMouseButtonDown(1);

        if (zoom)
            return true;

        return false;
    }

    public string GetTitle()
    {
        return "基本操作 ズーム";
    }

    public string GetText()
    {
        return "右クリックでズームを行いながらの攻撃が可能です。\n"
                + "再度右クリックによりズームを解除します。";
    }


    public float GetTransitionTime()
    {
        return 2f;
    }
}
