using System;

public class KeyMapper
{
    public KeyMapper()
    {
        
    }

    public void AddKeyMapping(string command, Action<int, List<object>, string> action, string message, ControlType control, Key key, bool adminOnly = false)
    {
        RegisterCommand(command, action, adminOnly);

    }

}


public enum ControlType
{
    keyboard,
    gamepad

}

public enum Key
{
    a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z
}



class Program
{
    
    public void Main()
    {
        var keyMap = new KeyMapper();

        keyMap.AddKeyMapping(string command, 
            new Action<int, List<object>, string>((source, args, rawCommand) =>
            {
                if (source > 0) // it's a player.
                {
                    // Create a message object.
                    dynamic messageObject = new ExpandoObject();
                    // Set the message object args (message author, message content)
                    messageObject.args = new string[] { GetPlayerName(source.ToString()), "PONG!" };
                    // Set the message color (r, g, b)
                    messageObject.color = new int[] { 5, 255, 255 };

                    // Trigger the client event with the message object on all clients.
                    TriggerClientEvent("chat:addMessage", messageObject);
                }
                // It's not a player, so it's the server console, a RCON client, or a resource.
                else
                {
                    Debug.WriteLine("This command was executed by the server console, RCON client, or a resource.");
                }
            }),
            message, ControlType.keyboard, Key.x);
    }
}
