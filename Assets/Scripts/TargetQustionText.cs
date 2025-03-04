
using TMPro;
using UnityEngine;

public class TargetQustionText : MonoBehaviour
{
    public TextMeshProUGUI targetQustion;
    private static TargetQustionText instance;
    private void Awake()
    {
        instance = this;
    }

    public static void SetTextTargetQustion(string textQustion)
    {
        instance.targetQustion.text = textQustion;
    }
}
