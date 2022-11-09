using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : Enemu
{

    protected override void Enabled()
    {
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Кухня)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Кухня)));
    }

    protected override void OnCustomTriggerStay(Entity e)
    {

    }
}
