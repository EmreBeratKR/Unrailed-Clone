using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Car : MonoBehaviour
{
    private static Train main;
    private Coroutine moveCoroutine;
    protected bool isPathCalculated;
    public Rail attachedRail;
    public GameObject[] destroyBeforeExplode;
    public ParticleSystem[] particles;

    public virtual float progress => main.progress;
    public bool isExploded { get; private set; }


    private void Awake()
    {
        main = this.GetComponentInParent<Train>();
    }

    private void OnEnable()
    {
        EventManager.OnTrainStarted += StartMovement;
        EventManager.OnTrainPassedNextRail += AttachToNextRail;
    }

    private void OnDisable()
    {
        EventManager.OnTrainStarted -= StartMovement;
        EventManager.OnTrainPassedNextRail -= AttachToNextRail;
    }

    public virtual void StartMovement()
    {
        moveCoroutine = StartCoroutine(Move_Co());
    }

    public virtual void StopMovement()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
    }

    public virtual void Explode()
    {
        if (isExploded) return;

        StopMovement();
        foreach (var obj in destroyBeforeExplode)
        {
            Destroy(obj, 0.1f);
        }
        for (int i = 0; i < particles.Length; i++)
        {   
            particles[i].Play();
        }
        Destroy(gameObject, particles[0].main.duration);

        isExploded = true;
    }

    public virtual void AttachToRail(Rail rail)
    {
        attachedRail = rail;
    }

    public virtual void AttachToNextRail()
    {
        if (attachedRail.next == null)
        {
            Explode();
            return;
        }

        attachedRail = (Rail) attachedRail.next;
        isPathCalculated = false;
    }

    protected void CalculatePath(ref Vector3 startPos, ref Vector3 endPos, ref float startAngleY, ref float endAngleY)
    {
        if (attachedRail.isReversed)
        {
            startPos = attachedRail.isCorner ? attachedRail.cornerPath.end.position : attachedRail.normalPath.end.position;
            endPos = attachedRail.isCorner ? attachedRail.cornerPath.start.position : attachedRail.normalPath.start.position;

            startAngleY = 180f + (attachedRail.isCorner ? attachedRail.cornerPath.end.eulerAngles.y : attachedRail.normalPath.end.eulerAngles.y);
            endAngleY = 180f + (attachedRail.isCorner ? attachedRail.cornerPath.start.eulerAngles.y : attachedRail.normalPath.start.eulerAngles.y);
        }
        else
        {
            startPos = attachedRail.isCorner ? attachedRail.cornerPath.start.position : attachedRail.normalPath.start.position;
            endPos = attachedRail.isCorner ? attachedRail.cornerPath.end.position : attachedRail.normalPath.end.position;

            startAngleY = attachedRail.isCorner ? attachedRail.cornerPath.start.eulerAngles.y : attachedRail.normalPath.start.eulerAngles.y;
            endAngleY = attachedRail.isCorner ? attachedRail.cornerPath.end.eulerAngles.y : attachedRail.normalPath.end.eulerAngles.y;
        }

        isPathCalculated = true;
    }


    public virtual IEnumerator Move_Co()
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

            if (attachedRail.isCorner)
            {
                transform.position = Helper.Vector3QLerp(startPosition, attachedRail.cornerPath.curveAnchor.position, targetPosition, progress);
            }
            else
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, progress); 
            }
            transform.eulerAngles = Vector3.up * Mathf.LerpAngle(startAngleY, targetAngleY, progress);

            yield return 0;
        }
    }
}