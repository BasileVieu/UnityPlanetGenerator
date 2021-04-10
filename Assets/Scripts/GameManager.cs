using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float fps;
    public static float fpsAverage;
    public static float fpsMin = Mathf.Infinity;
    public static float fpsMax;

    private float timerFps;

    private List<float> listFps = new List<float>();

    void Update()
    {
        timerFps += Time.deltaTime;

        if (timerFps > 0.5f)
        {
            CountFps();

            timerFps -= 0.5f;
        }
    }

    void CountFps()
    {
        fps = 1.0f / Time.unscaledDeltaTime;

        if (fps < fpsMin)
        {
            fpsMin = fps;
        }

        if (fps > fpsMax)
        {
            fpsMax = fps;
        }
        
        listFps.Add(fps);

        if (listFps.Count > 1000)
        {
            listFps.RemoveAt(0);
        }

        float totalFps = listFps.Sum();

        fpsAverage = totalFps / listFps.Count;
    }
}