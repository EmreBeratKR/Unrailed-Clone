using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private FocusChecker focusChecker;

    private GameObject focus { get; set; }


    public void Update()
    {
        focus = focusChecker.focus;
        
        if (focus == null) return;

        if (focus.TryGetComponent(out IDamagable damageable))
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                damageable.TakeDamage(10f);
            }
        }
    }
}
