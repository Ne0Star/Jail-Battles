using UnityEngine;

public class StandartBar : HitBar
{
    [SerializeField] private bool destroyVisual;

    [SerializeField] private Renderer mat;

    private void Awake()
    {
        if (destroyVisual)
        {
            if (transform.GetChild(0))
                Destroy(transform.GetChild(0).transform.gameObject);
        }
        UpdateData();
    }


    //private void OnEnable()
    //{
    //    SetFull();
    //}

    public override void UpdateData()
    {
        if (destroyVisual) return;

        mat.material.SetFloat("_maxHealth", maxHealth);
        mat.material.SetFloat("_currentHealth", health);

        //float resultX = 0f;
        //float heal = Mathf.Clamp(1f / health, 0.001f, 9999f);
        //float maxHeal = Mathf.Clamp(1f / maxHealth, 0.001f, 999f);
        //resultX = Mathf.Clamp((1f / heal) * maxHeal, 0, 1f);
        //HealthTarget.transform.localScale = new Vector3(resultX, HealthTarget.localScale.y, HealthTarget.localScale.z);
    }
}
