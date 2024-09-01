using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Properties_Boss ", menuName = "Preoperties/Boss")]
public class ScriptProperties : ScriptableObject
{
    [SerializeField] private int Health;

    [SerializeField] private int Energy;
}
