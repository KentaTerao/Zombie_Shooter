using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのタスク（ジャンプ）
public class TaskJump : TutorialTask
{
    public void OnTaskSetting()
    {
    }

    public bool CheckTask()
    {
        bool jump = Input.GetKey(KeyCode.Space);

        if (jump)
            return true;

        return false;
    }

    public string GetTitle()
    {
        return "基本操作 ジャンプ";
    }

    public string GetText()
    {
        return "spaceキーでジャンプを行います。";
    }


    public float GetTransitionTime()
    {
        return 2f;
    }
}
