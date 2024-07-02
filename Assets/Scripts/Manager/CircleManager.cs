using UnityEngine;
using System.Collections;

public class CircleManager : Singleton<CircleManager>
{
    [SerializeField] private RhythmCircle[] rhythmCircles; // Array of RhythmCircle components
    [SerializeField] private float delayBetweenCircles = 0.5f; // Delay between circles

    private int currentCircleIndex = 0;

    public void StartGimmick()
    {
        StartCoroutine(StartRhythmCircles());
    }

    private IEnumerator StartRhythmCircles()
    {
        while (currentCircleIndex < rhythmCircles.Length)
        {
            rhythmCircles[currentCircleIndex].StartAnimation();
            currentCircleIndex++;
            yield return new WaitForSeconds(delayBetweenCircles); // Delay between circles
        }
    }
    
}
