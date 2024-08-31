using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  HP回復アイテムを取得した際の挙動を管理するクラス
public class PickUpMedicalKit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UseMedicalKit useMedicalKit = other.GetComponent<UseMedicalKit>();
            if (useMedicalKit.MedicalKitIsMax())
                return;

            Destroy(gameObject);
            useMedicalKit.IncreaseCurrentMedicalKit(); // プレイヤーの回復アイテム所持数を増加させる
        }
    }
}
