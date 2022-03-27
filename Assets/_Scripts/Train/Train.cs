using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public const float SPEED_MODE_MULTIPLIER = 3f;

    public TrainEngine engine;
    public float speed;
    [SerializeField] private List<Car> cars;

    public float progress { get; private set; }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartEngine();
        }
    }

    private void StartEngine()
    {
        StartCoroutine(Progress_Co());
        EventManager.TrainStarted();
    }

    private IEnumerator Progress_Co()
    {
        progress = 0f;
        while (true)
        {
            progress += Time.deltaTime * speed * (engine == null ? SPEED_MODE_MULTIPLIER : 1f);
            if (progress >= 1f)
            {
                progress = 0f;
                EventManager.TrainPassedNextRail();
            }
            yield return 0;
        }
    }
}
