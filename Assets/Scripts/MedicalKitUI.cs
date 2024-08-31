using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// プレイヤーが所持している回復アイテムの数と最大所持数を表示するUIを管理するクラス
public class MedicalKitUI : MonoBehaviour
{
    [SerializeField] UseMedicalKit useMedicalKit;
    [SerializeField] TextMeshProUGUI currentMedicalKitText; // 現在所持している回復アイテム数を表示するテキスト
    [SerializeField] TextMeshProUGUI maxMedicalKitText; // 回復アイテムの最大所持数を表示するテキスト

    void Update()
    {
        UpdateCurrentMedicalKitText();
        UpdateMaxMedicalKitText();
    }

    // 現在所持している回復アイテム数を表示するテキストを更新するメソッド
    void UpdateCurrentMedicalKitText()
    {
        currentMedicalKitText.text = useMedicalKit.GetCurrentMedicalKit().ToString();
    }

    //  回復アイテムの最大所持数を表示するテキストを更新するメソッド
    void UpdateMaxMedicalKitText()
    {
        maxMedicalKitText.text = useMedicalKit.GetMaxMedicalKit().ToString();

    }
}
