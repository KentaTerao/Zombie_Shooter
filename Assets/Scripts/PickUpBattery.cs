using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ライトの光量を回復させるバッテリーアイテムを取得した際の挙動を管理するクラス
public class PickUpBattery : MonoBehaviour
{
    [SerializeField] float lightIntensityGrowth = 2f; // 回復させる光の強さ
    [SerializeField] float lightSizeGrowth = 10f; // 回復させる光の大きさ
    ElectricTorch electricTorch; // 回復させる対象のライト

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            electricTorch = other.GetComponentInChildren<ElectricTorch>();
            electricTorch.IncreaseLightIntensity(lightIntensityGrowth);
            electricTorch.IncreaseLightSize(lightSizeGrowth);

        }
    }
}
