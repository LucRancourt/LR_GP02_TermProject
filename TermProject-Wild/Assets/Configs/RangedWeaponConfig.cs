using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeaponConfig", menuName = "Game Configs/RangedWeapon Config")]
public class RangedWeaponConfig : ScriptableObject
{
    public float fireRate = 0.5f;
    public float damage = 1.0f;
    public float bulletSpeed = 5.0f;
}

