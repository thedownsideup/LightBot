using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Command Config", menuName = "Command Config")]
public class CommandConfig : ScriptableObject
{
    [SerializeField] private int value;
    [SerializeField] private Sprite icon;

    public int Value => value;
    public Sprite Icon => icon;
}