using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

/*----- Class name should match <filename>.cs -----*/
#if EXTERNAL_EDITOR
public class UserGreeting : CPHInlineBase
#else
public class CPHInline
#endif
/*--------------------------------------------*/

{
    public bool Execute()
    {
        String userId = args["userId"].ToString();
        String filename = "users.json";
        List<User>? users = null;
        try
        {
            users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(filename));
        }
        catch (FileNotFoundException)
        {
            CPH.LogWarn($"The users list does not exist! Treating as not set.");
            return false;
        }
        catch (Exception e)
        {
            CPH.LogError($"ERROR: Something went wrong. \n {System.DateTime.Now} \n {e}");
            return false;
        }

        if (users != null)
        {
            return reply(users, userId);
        }
        return false;
    }

    private class User
    {
        public string? userId {
            get;
            set; 
        }
        public string? greeting { 
            get;
            set; 
        }
    }
    private bool reply(List<User> users, String userId)
    {
        foreach(User userInfo in users)
        {
            if(userInfo.userId == userId){
                CPH.TwitchReplyToMessage(userInfo.greeting, args["msgId"].ToString(), true, true);
                return true;
            }
        }
        return false;
    }
}