using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ウェーブ終了時に出現するエクストラアイテムの抽象クラス
public abstract class ExtraItem : MonoBehaviour
{
    [HideInInspector] public string description;
    protected virtual void Start()
    {
        SetDescription();
    }

    // 抽象メソッドとして宣言し、派生クラスで実装する

    public abstract void SetDescription();
    public abstract void Apply();

    public string GetDescription()
    {
        return description;
    }
}
