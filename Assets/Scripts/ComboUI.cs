using TMPro;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI comboBonusText;
    [SerializeField] TextMeshProUGUI comboText;

    void Update()
    {
        comboBonusText.text = "COMBO BONUS: x" + ComboManager.Instance.GetScoreMultiplier();
        comboText.text = "COMBO: " + ComboManager.Instance.GetComboCount();
    }
}
