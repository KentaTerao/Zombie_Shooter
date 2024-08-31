using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// プレイヤーのHPを表示するUIを管理するクラス
public class HPUI : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] TextMeshProUGUI currentHPText; // プレイヤーの現在HPを表示するテキスト
    [SerializeField] TextMeshProUGUI maxHPText; // プレイヤーの最大HPを表示するテキスト

    void Update()
    {
        UpdateCurrentHPText();
        UpdateMaxHPText();
    }

    // プレイヤーの現在HPを表示するテキストを更新するメソッド
    void UpdateCurrentHPText()
    {
        currentHPText.text = playerHealth.GetCurrentHP().ToString();
    }

    // プレイヤーの最大HPを表示するテキストを更新するメソッド
    void UpdateMaxHPText()
    {
        maxHPText.text = playerHealth.GetMaxHP().ToString();

    }
}