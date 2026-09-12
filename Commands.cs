using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Wpf.Ui.Controls;

/*----- Class name should match <filename>.cs -----*/
#if EXTERNAL_EDITOR
public class Commands : CPHInlineBase
#else
public class CPHInline
#endif
/*--------------------------------------------*/
{
    public bool Execute(){
    string userType = args["userType"].ToString();
    try
    {
        commandList(userType);
    }

    catch (System.Exception e){   
        CPH.TwitchReplyToMessage($"Error: Something went wrong. Check logs.", args["msgId"].ToString(), true, true);
        CPH.LogDebug($"Error: Something went wrong! {System.DateTime.Now}");
        CPH.LogError(e.ToString());
    }

    return true;
    }
    
    private void commandList(String userType){
    List<CommandData> commandList = CPH.GetCommands();
        switch (userType){
            case "twitch":
                TwitchCommands(commandList);
                break;

            default:
                CPH.LogError($"Error: {userType} is not supported!");
                break;
        }
    }
    private void TwitchCommands(List<CommandData> commandList)
    {
        CPH.TwitchReplyToMessage($"Working...", args["msgId"].ToString(), true, true);
        string reply = "";
        foreach(CommandData command in commandList)
        {   
            string commands = String.Join(", ", command.Commands);
            if (String.IsNullOrEmpty(reply))
                reply = commands;
            else
                reply = reply + ", " + commands;
        }
        CPH.TwitchReplyToMessage($"Available Commands Are: {reply}", args["msgId"].ToString(), true, true);
    }
}