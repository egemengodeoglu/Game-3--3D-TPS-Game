using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangedInteger
{

    public int min;
    public int max;

    public int RandomValue()
    {
        return Random.Range(min,max);
    }
}
