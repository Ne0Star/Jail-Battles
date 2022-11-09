using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController
{


    public abstract void Update();
    public virtual Vector3 Direction()
    {
        return Vector3.zero;
    }
}
