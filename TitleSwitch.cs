using System;

/*----- Class name should match <filename>.cs -----*/
#if EXTERNAL_EDITOR
public class TitleSwitch : CPHInlineBase
#else
public class CPHInline
#endif
/*--------------------------------------------*/
{
    public bool Execute()
    {
        try
        {
            TitleConfirm();
        }
        catch(System.Exception e)
        {
            CPH.LogError(e.ToString());
        }
        
        return true;
    }

    private void TitleConfirm()
    {
        String title = args["rawInput"].ToString();
        bool result = CPH.SetChannelTitle(title);
        if (String.IsNullOrEmpty(title) && (args["isModerator"].ToString() == "true"))
        {
            CPH.TwitchReplyToMessage($"Current title is: {args["broadcasterChannel.title"].ToString()}.",args["msgId"].ToString(), true, true);
        }
        else if (result)
        {
            CPH.TwitchReplyToMessage($"Successfully set title as: {title}.", args["msgId"].ToString(), true, true);
        }
        else
        {
            CPH.TwitchReplyToMessage("Failed to set title.", args["msgId"].ToString(), true, true);
        }
    }
}