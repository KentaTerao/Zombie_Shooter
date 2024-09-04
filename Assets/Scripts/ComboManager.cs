using UnityEngine;
using UnityEngine.Events;

public class ComboManager : MonoBehaviour
{
    private static ComboManager instance;
    int comboCount = 0; // ダメージを受けずに連続で敵を倒した数


    // シングルトンのインスタンスを返すプロパティ
    public static ComboManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ComboManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<ComboManager>();
                    singletonObject.name = typeof(ComboManager).ToString() + " (Singleton)";
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

    // コンボ数を増加させるメソッド
    public void IncreaseComboCount()
    {
        comboCount++;
    }

    // コンボ数をリセットするメソッド
    public void ResetCombo()
    {
        comboCount = 0;
    }

    // 現在のコンボ数を取得するメソッド
    public int GetComboCount()
    {
        return comboCount;
    }

    // 現在のコンボ数に応じたスコア乗算値を取得するメソッド
    public float GetScoreMultiplier()
    {
        if (comboCount >= 20)
            return 5.0f;
        else if (comboCount >= 15)
            return 4.0f;
        else if (comboCount >= 10)
            return 2.5f;
        else if (comboCount >= 5)
            return 1.5f;
        else
            return 1.0f;
    }
}
