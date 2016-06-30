using UnityEngine;
using System.Collections;

public class ObjectMob : ObjectUnit
{
    [SerializeField]
    private Type curType;

    public void setType(Type type)
    {
        curType = type;
    }

    public Type getType()
    {
        return curType;
    }
}