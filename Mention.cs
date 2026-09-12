using System;

/*----- Class name should match <filename>.cs -----*/
#if EXTERNAL_EDITOR
public class Mention : CPHInlineBase
#else
public class CPHInline
#endif
/*--------------------------------------------*/

{
    public bool Execute()
    {
        String userId = args["userId"].ToString();
        if (!String.IsNullOrEmpty(userId))
        {
            switch (userId)
            {
                case "USER-ID-REMOVED":
                    CPH.TwitchReplyToMessage($"It's a @Vortox14! How're you?", args["msgId"].ToString(), true, true);
                    break;

                default:
                    break;
            }
            return true;
        }
        return false;
    }
}