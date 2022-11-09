using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformBar : HitBar
{
    [SerializeField] private bool destroyVisual;

    [SerializeField] private Transform healthTarget;

    private void Awake()
    {
        if (destroyVisual)
        {
            if (transform.GetChild(0))
                Destroy(transform.GetChild(0).transform.gameObject);
        }
        UpdateData();
    }


    private void OnEnable()
    {
        SetFull();
    
    
    }


    private void Update()
    {
        UpdateData();
    }

    public override void UpdateData()
    {
        if (destroyVisual) return;

        float resultX = 0f;
        float heal = Mathf.Clamp(1f / health, 0.001f, 9999f);
        float maxHeal = Mathf.Clamp(1f / maxHealth, 0.001f, 999f);
        resultX = Mathf.Clamp((1f / heal) * maxHeal, 0, 1f);
        healthTarget.transform.localScale = new Vector3(resultX, healthTarget.localScale.y, healthTarget.localScale.z);
    }
}
