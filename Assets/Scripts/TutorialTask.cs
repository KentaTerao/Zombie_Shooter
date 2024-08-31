using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのタスクを実装するためのインターフェース
public interface TutorialTask
{
    // チュートリアルタスクが設定された際に実行されるメソッド
    void OnTaskSetting();

    // タスクが達成されたか判定するメソッド
    bool CheckTask();

    // チュートリアルのタイトルを取得するメソッド
    string GetTitle();

    // 説明文を取得するメソッド
    string GetText();

    // 達成後に次のタスクへ遷移するまでの時間(秒)を取得するメソッド
    float GetTransitionTime();
}