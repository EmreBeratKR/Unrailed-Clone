using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Harvestable : MonoBehaviour, IHarvestable, IDamagable
{
    public const float maxHealth = 100f;

    [SerializeField] private GameObject drop;
    [SerializeField] private ModelOverHealth modelOverHealth;
    [SerializeField] private ParticleSystem[] particles;
    public GameObject[] destroyBeforeHarvest;

    public bool isHarvested { get; private set; }
    public virtual float health { get; set; }
    public abstract Tool properTool { get; }


    public virtual void Start() => health = maxHealth;

    public virtual void UpdateModel()
    {
        if (health >= maxHealth * 0.67f)
        {
            modelOverHealth.SetHigh();
        }
        else if (health >= maxHealth * 0.33f)
        {
            modelOverHealth.SetMedium();
        }
        else
        {
            modelOverHealth.SetLow();
        }
    }

    public virtual void TakeDamage(float damage)
    {
        this.health -= damage;
        EmitParticles();
        UpdateModel();
        if (this.health <= 0f)
        {
            this.health = 0f;
            Harvest();
        }
    }

    public virtual void Harvest()
    {
        if (isHarvested) return;

        isHarvested = true;

        Instantiate(drop, transform.position, Quaternion.identity);

        foreach (var obj in destroyBeforeHarvest)
        {
            Destroy(obj);
        }
        Destroy(gameObject, particles[0].main.duration);

    }

    public virtual void EmitParticles()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}

[System.Serializable]
public struct ModelOverHealth
{
    public GameObject High;
    public GameObject Medium;
    public GameObject Low;

    public void SetHigh()
    {
        High.SetActive(true);
        Medium.SetActive(false);
        Low.SetActive(false);
    }

    public void SetMedium()
    {
        High.SetActive(false);
        Medium.SetActive(true);
        Low.SetActive(false);
    }

    public void SetLow()
    {
        High.SetActive(false);
        Medium.SetActive(false);
        Low.SetActive(true);
    }
}