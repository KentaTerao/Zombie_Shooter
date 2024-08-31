using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 懐中電灯を管理するクラス
// 懐中電灯の光は時間経過で弱く、小さくなる
public class ElectricTorch : MonoBehaviour
{
    [SerializeField] float lightIntensityDecay = 0.1f; // 光の強さの減衰度合い
    [SerializeField] float lightSizeDecay = 1f; // 光の大きさの減衰度合い
    [SerializeField] float lightIntensityMax = 4f; // 光の強さの最大値
    [SerializeField] float lightSizeMax = 50f; // 光の大きさの最大値
    [SerializeField] float lightSizeMin = 30f; // 光の大きさの最小値

    Light light; // 対象の光源

    void Start()
    {
        light = GetComponentInChildren<Light>();
    }

    void Update()
    {
        // 時間経過で光の強さと大きさを減衰させる
        DecreaseLightIntensity();
        DecreaseLightSize();
    }

    // 光の強さを減衰させるメソッド
    public void DecreaseLightIntensity()
    {
        light.intensity -= lightIntensityDecay * Time.deltaTime;
    }

    // 光の大きさを減衰させるメソッド
    public void DecreaseLightSize()
    {
        if (light.spotAngle <= lightSizeMin) // 光の大きさが一定以下にはならないようにする
            return;

        light.spotAngle -= lightSizeDecay * Time.deltaTime;
    }

    // 光の強さを増幅するメソッド
    public void IncreaseLightIntensity(float lightIntensityGrowth)
    {
        // 光の強さが最大値を上回らないようにする
        if (light.intensity + lightIntensityGrowth <= lightIntensityMax)
            light.intensity += lightIntensityGrowth;
        else
            light.intensity = lightIntensityMax;
    }

    // 光の大きさを増幅するメソッド
    public void IncreaseLightSize(float lightSizeGrowth)
    {
        // 光の大きさが最大値を上回らないようにする
        if (light.spotAngle + lightSizeGrowth <= lightIntensityMax)
            light.spotAngle += lightSizeGrowth;
        else
            light.spotAngle = lightSizeMax;
    }
}
