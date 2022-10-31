using System.Collections.Generic;
using UnityEngine;

public class CommandData
{
    public int Value { get; }
    public Sprite Icon { get; }

    public CommandData(CommandConfig commandConfig)
    {
        Value = commandConfig.Value;
        Icon = commandConfig.Icon;
    }

}
