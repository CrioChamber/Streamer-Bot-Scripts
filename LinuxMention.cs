using System;

#if EXTERNAL_EDITOR
public class LinuxMention : CPHInlineBase
#else
public class CPHInline
#endif
{
    public bool Execute()   
    {
        var command = args["command"].ToString();
        //CPH.SendMessage($"Command: {command}");
        if (command.IndexOf(" ") != -1)
        {
            command = command.Substring(0, command.IndexOf(" "));
        }
        if (string.Equals(command, "!distro", StringComparison.OrdinalIgnoreCase))
        {
            CPH.SendMessage("Crio's Linux Distro is Fedora KDE Plasma. https://fedoraproject.org/kde/");
            return true;
        }
        return false;
    }
}