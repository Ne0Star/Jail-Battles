using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLineHealth : InLine
{
    private void Awake()
    {
        isEmpty = true;
        type = InlineType.ВМедпункт;

    }

    protected override void StartAction(Entity exucutor, Action<Entity> onComplete)
    {
        exucutor.HitBar.AddHealth(1000);
        onComplete?.Invoke(exucutor);
    }
}
