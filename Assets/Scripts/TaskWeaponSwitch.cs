using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのタスク（攻撃）
public class TaskWeaponSwitch : TutorialTask
{
    public void OnTaskSetting()
    {
    }

    public bool CheckTask()
    {
        float switchWheel = Input.GetAxis("Mouse ScrollWheel");
        bool switchNumKey = Input.GetKeyDown(KeyCode.Alpha0)
                            || Input.GetKeyDown(KeyCode.Alpha1)
                            || Input.GetKeyDown(KeyCode.Alpha2)
                            || Input.GetKeyDown(KeyCode.Alpha3);

        if (switchWheel != 0 || switchNumKey)
            return true;

        return false;
    }

    public string GetTitle()
    {
        return "基本操作 武器切り替え";
    }

    public string GetText()
    {
        return "マウスホイール・数字キーで武器の切り替えが可能です。";
    }


    public float GetTransitionTime()
    {
        return 2f;
    }
}
