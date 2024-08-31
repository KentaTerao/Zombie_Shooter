using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのタスク（メインメニューに戻る）
public class TaskEnd : TutorialTask
{
    public void OnTaskSetting()
    {
    }

    public bool CheckTask()
    {
        return false;
    }

    public string GetTitle()
    {
        return "タイトルへ戻る";
    }

    public string GetText()
    {
        return "お疲れさまでした。\n"
                + "これでチュートリアルは終了です。\n"
                + "escapeキーでポーズ画面を開き、MainMenuボタンを押して戻ってください。";
    }


    public float GetTransitionTime()
    {
        return 2f;
    }
}
