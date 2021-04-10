using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text fpsText;
    public Text fpsAverageText;
    public Text fpsMinText;
    public Text fpsMaxText;

    void Update()
    {
        fpsText.text = "FPS : " + GameManager.fps.ToString("0.00");
        fpsAverageText.text = "FPS Average : " + GameManager.fpsAverage.ToString("0.00");
        fpsMinText.text = "FPS Min : " + GameManager.fpsMin.ToString("0.00");
        fpsMaxText.text = "FPS Max : " + GameManager.fpsMax.ToString("0.00");
    }
}