using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ゲームオーバー時のUIを管理するクラス
public class GameOverUI : MonoBehaviour
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] Text resultWaveText;
    [SerializeField] Text resultScoreText;

    void Update()
    {
        resultWaveText.text = waveManager.currentWave.ToString();
        resultScoreText.text = ScoreManager.Instance.GetScore().ToString();
    }
}
