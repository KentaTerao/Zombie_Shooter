using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UseMedicalKit : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] int maxMedicalKit = 3; // 回復アイテムの最大所持数
    [SerializeField] float healAmount = 150; // 回復アイテム使用時に回復するHPの量
    int currentMedicalKit; // 回復アイテムの現在所持数

    void Start()
    {
        currentMedicalKit = 1; // 初期状態で1つ所持
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            Use();
    }

    // 回復アイテムを使用する際に呼び出されるメソッド
    public void Use()
    {
        // 回復アイテムの現在所持数が0以下の場合は使用不可能
        if (currentMedicalKit <= 0)
            return;

        // 現在HPが最大HPと同値の場合は使用不可能
        if (playerHealth.HPIsMax())
            return;

        // プレイヤーの現在HPを増加するメソッドを呼び出し、回復アイテムの現在所持数をデクリメントする
        playerHealth.HealHP(healAmount);
        currentMedicalKit--;
    }

    public int GetCurrentMedicalKit()
    {
        return currentMedicalKit;
    }

    public int GetMaxMedicalKit()
    {
        return maxMedicalKit;
    }

    public void IncreaseCurrentMedicalKit()
    {
        currentMedicalKit++;
    }

    // 回復アイテムの現在所持数が最大所持数と同値かどうかを判定するメソッド
    public bool MedicalKitIsMax()
    {
        if (currentMedicalKit > maxMedicalKit)
            return true;
        else
            return false;
    }
}
