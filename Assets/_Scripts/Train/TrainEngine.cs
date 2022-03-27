using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainEngine : Car
{

    /* public override IEnumerator Move_Co()
    {
        Vector3 startPosition = Vector3.zero;
        Vector3 targetPosition = Vector3.zero;

        float startAngleY = 0f;
        float targetAngleY = 0f;

        while (true)
        {
            if (!isPathCalculated)
            {
                CalculatePath(ref startPosition, ref targetPosition, ref startAngleY, ref targetAngleY);
            }

            if (attachedRail.next == null)
            {
                EventManager.TrainCrashed();
                break;
            }

            if (attachedRail.isCorner)
            {
                transform.position = Helper.Vector3QLerp(startPosition, attachedRail.cornerPath.curveAnchor.position, targetPosition, progress);
            }
            else
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, progress); 
            }
            transform.eulerAngles = Vector3.up * Mathf.LerpAngle(startAngleY, targetAngleY, progress);

            engineProgress += Time.deltaTime * speed;
            if (engineProgress >= 1f)
            {
                engineProgress = 0f;
                EventManager.TrainPassedNextRail();
            }
            yield return 0;
        }
    } */
}
