
using System.Text;

namespace Hex_Space_Rpg.Core;

public record Base
{
    protected static int Indentation = 0;

    protected virtual void Start()
    {
        Write_log("Started");
        Indentation++;
    }

    protected virtual void End()
    {
        Indentation--;
        Write_log("Ended");
    }

    protected void Write_log(string message)
    {
        var sb = new StringBuilder();
        sb.Append(DateTime.Now.ToString("HH:mm:ss:ff"));
        for (int i = 0; i < Indentation; i++)
            sb.Append('\t');
        Console.WriteLine($"{sb} {GetType().Name} {message}");
    }
}
