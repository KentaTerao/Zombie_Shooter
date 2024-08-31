using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのタスク（移動）
public class TaskMove : TutorialTask
{
    public void OnTaskSetting()
    {
    }

    public bool CheckTask()
    {
        float axis_h = Input.GetAxis("Horizontal");
        float axis_v = Input.GetAxis("Vertical");

        if (0 < axis_v || 0 < axis_h)
            return true;

        return false;
    }

    public string GetTitle()
    {
        return "基本操作 移動";
    }

    public string GetText()
    {
        return "WSADキーで前後左右に移動を行います。";
    }


    public float GetTransitionTime()
    {
        return 2f;
    }
}