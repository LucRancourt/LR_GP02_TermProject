using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeaponConfig", menuName = "Game Configs/RangedWeapon Config")]
public class RangedWeaponConfig : ScriptableObject
{
    [Header("General")]
    public float damage;
    public float projectileSpeed;
    public float fireRate;
    public bool isAutomatic;


    [Header("Ammo")]
    public int maxAmmo;
    public int ammoRequired;
}

