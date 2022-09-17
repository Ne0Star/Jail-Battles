using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private bool isAttack = false;

    public bool IsAttack { get => isAttack; }

    public void SetIsAttack(bool val)
    {
        StartCoroutine(Wait(val));
    }
    private IEnumerator Wait(bool val)
    {
        yield return new WaitForSeconds(0.2f);
        isAttack = val;
    }
}
