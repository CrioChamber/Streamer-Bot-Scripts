using System;
using System.Collections.Generic;

/*----- Class name should match <filename>.cs -----*/
#if EXTERNAL_EDITOR
public class GameSwitch : CPHInlineBase
#else
public class CPHInline
#endif
/*--------------------------------------------*/
{
    public bool Execute(){

    //Supported platforms for the foreach loop, applying the update to each respective platform.
    List<String> platforms = new() {"twitch", "youtube"};

    if(isModerator(args["isModerator"].ToString())){
        foreach(String platform in platforms)
        {
            changeGame(platform);
        }
        return true;
    }
    else
        platformReply(args["userType"].ToString());

    return true;
    }

    private void changeGame(String platform){
    switch (platform){
        case "twitch":
            SetTwitchGame(platform);
            break;

        case "youtube":
            CPH.LogVerbose("Youtube does not apply.");
            break;

        default:
            CPH.LogVerbose("Unsupported Platform.");
            break;
        }
    }

    private void platformReply(String userType, GameInfo result)
    {
    switch (userType){
        case "twitch":{
            if (result != null && isModerator(args["isModerator"].ToString()))
            {
                CPH.TwitchReplyToMessage($"Game set to: {result.Name}", args["msgId"].ToString(), true, true);
            }
            else if(result == null && isModerator(args["isModerator"].ToString()))
            {
                CPH.TwitchReplyToMessage($"Game not found.", args["msgId"].ToString(), true, true);
            }
            break;
        }
        default:
            break;
        }
    }

    private void platformReply(String userType)
    {
        switch (userType)
        {
            case "twitch":
                if (String.IsNullOrEmpty(args["rawInput"].ToString()) && isModerator(args["isModerator"].ToString()))
                {
                    CPH.TwitchReplyToMessage($"Current game is: {args["broadcasterChannel.gameName"].ToString()}", args["msgId"].ToString(), true, true);
                }
                else
                {
                    CPH.TwitchReplyToMessage($"You are not authorized to use this command.", args["msgId"].ToString(), true, true);
                }
            break;

            default:
            break;
        }
    }

    private bool isModerator(string isModerator)
    {
        switch (isModerator)
        {
            case "true":
            return true;

            default:
            return false;
        }
    }

    private void SetTwitchGame(String platform)
    {
        GameInfo result = CPH.SetChannelGame(args["rawInput"].ToString());
        platformReply(platform, result);
    }
}