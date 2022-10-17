using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Command_Manager : MonoBehaviour
{
    public static Command_Manager Instance;
    
    private List<Command> commands = new List<Command>();
    
    [SerializeField] private GameObject commandContainer;
    [SerializeField] private Transform mainContainer;

    private Bot bot;
    
    private void Start()
    {
        Instance = this;
        bot = GameObject.Find("Bot").GetComponent<Bot>();

        if (bot == null)
            Debug.LogError("Bot Is Empty");
    }
    public void AddCommand(Command command)
    {
        commands.Add(command);
        ShowCommandItem(command);
    }

    public void RemoveCommand(Command command)
    {
        commands.Remove(command);
    }

    private void ShowCommandItem(Command command)
    {
        GameObject commandPrefab = Instantiate(commandContainer, mainContainer);
        commandPrefab.GetComponent<Image>().sprite = command.icon;
    }

    public void OnRun()
    {
        CommandBot();
    }

    private void CommandBot()
    {
        StartCoroutine(bot.Move(commands));
    }
}