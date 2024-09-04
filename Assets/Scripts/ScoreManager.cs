using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スコアを管理するクラス
public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;

    int score = 0; // スコア

    // シングルトンのインスタンスを返すプロパティ
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<ScoreManager>();
                    singletonObject.name = typeof(ScoreManager).ToString() + " (Singleton)";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        // 他のインスタンスが存在する場合は、自己破壊する
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // シーン遷移時に破棄されないようにする
        }
    }

    public void IncreaseScore(int scoreAmount)
    {
        // ComboManagerからスコア乗算値を取得し、それを適用
        float multiplier = ComboManager.Instance.GetScoreMultiplier();
        int multipliedScore = Mathf.RoundToInt(scoreAmount * multiplier);
        score += multipliedScore;
    }

    public void DecreaseScore(int scoreAmount)
    {
        score -= scoreAmount;
    }

    public void ResetScore()
    {
        score = 0;
        ComboManager.Instance.ResetCombo(); // スコアをリセットする際にコンボもリセット
    }

    public int GetScore()
    {
        return score;
    }
}
