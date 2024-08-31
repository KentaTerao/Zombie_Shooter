using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ウェーブに関連するUIを管理するクラス
public class WaveUI : MonoBehaviour
{
    public WaveManager waveManager;
    public TextMeshProUGUI waveNumText; // ウェーブ番号を表示するText
    public TextMeshProUGUI waveTimeText; // 残り時間を表示するText

    void Start()
    {
        // UI更新のためのコルーチンを開始
        StartCoroutine(UpdateUI());
    }

    IEnumerator UpdateUI()
    {
        while (true)
        {
            if (waveManager != null)
            {
                if (waveManager.isWaveActive)
                {
                    // ウェーブ中の場合はウェーブ番号と残り時間を表示
                    waveNumText.text = "Wave: " + waveManager.currentWave;

                    // 残り時間を計算
                    float remainingTime = waveManager.waveDuration - (Time.time - waveManager.waveStartTime);
                    remainingTime = Mathf.Max(remainingTime, 0);

                    // 分と秒に分割
                    int minutes = Mathf.FloorToInt(remainingTime / 60);
                    int seconds = Mathf.FloorToInt(remainingTime % 60);

                    // 残り時間をMM:SS形式で表示
                    waveTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                }
                else
                {
                    // 待機時間中の場合は「Standby Time」と残り時間を表示
                    waveNumText.text = "Standby Time";

                    // 残り時間を計算
                    float remainingTime = waveManager.timeBetweenWaves - (Time.time - waveManager.waveEndTime);
                    remainingTime = Mathf.Max(remainingTime, 0);

                    // 分と秒に分割
                    int minutes = Mathf.FloorToInt(remainingTime / 60);
                    int seconds = Mathf.FloorToInt(remainingTime % 60);

                    // 残り時間をMM:SS形式で表示
                    waveTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                }
            }
            yield return null; // 毎フレーム更新
        }
    }
}
