using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// スコアを表示するUIを管理するクラス
public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentScoreText; // 現在のスコアを表示するテキスト

    void Update()
    {
        UpdateCurrentScoreText();
    }

    // 現在のスコアを表示するテキストを更新するメソッド
    void UpdateCurrentScoreText()
    {
        currentScoreText.text = "SCORE:" + ScoreManager.Instance.GetScore().ToString("000000000");
    }
}
