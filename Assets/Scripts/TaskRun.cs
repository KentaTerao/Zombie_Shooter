using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのタスク（ダッシュ）
public class TaskRun : TutorialTask
{
    public void OnTaskSetting()
    {
    }

    public bool CheckTask()
    {
        float axis_v = Input.GetAxis("Vertical");
        bool leftShift = Input.GetKey(KeyCode.LeftShift);

        if (axis_v > 0 && leftShift)
            return true;

        return false;
    }

    public string GetTitle()
    {
        return "基本操作 ダッシュ";
    }

    public string GetText()
    {
        return "左shiftキーを押しながら前進(Wキー)でダッシュが可能です。";
    }


    public float GetTransitionTime()
    {
        return 2f;
    }
}
