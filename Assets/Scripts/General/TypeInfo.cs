using UnityEngine;
using System.Collections;

public enum StateType
{
    Solid,  // 고체
    Liquid, // 액체
    Gas     // 기체
}

public enum StatusEffect
{
    None,
    Burn,       // 화상
    Frostbite,  // 동상
    Corrosion   // 부식
}