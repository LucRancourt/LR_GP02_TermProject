using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "FirearmConfig", menuName = "Game Configs/Firearm Config")]
public class FirearmConfig : ScriptableObject
{
    public float fireRate = 0.5f;
    public float damage = 1.0f;
}

