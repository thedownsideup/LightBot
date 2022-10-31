using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CommandPick : MonoBehaviour
{
    [FormerlySerializedAs("command")] [SerializeField] private CommandConfig commandConfig;
    public void OnClick()
    {
        CommandData commandData = new CommandData(commandConfig);
        Command_Manager.Instance.AddCommand(commandData);
    }
}
