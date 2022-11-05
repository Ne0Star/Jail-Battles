using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInLineAction : AIAction
{

    public GetInLineAction(Entity executor, InlineType type)
    {

        InLine inLine = LevelManager.Instance.InLineManager.GetInline(type);

        inLine.AddExecutor(executor, () =>
        {
            onComplete?.Invoke(this);
        });
    }

    public override void CustomUpdate()
    {

    }

    public override void Initial()
    {

    }
}
