using System;
using System.Text;

namespace Core;

public abstract record Message
{
    protected static int Indentation = 0;

    public Message(bool invoke = true)
    {
        if (invoke)
            Invoke();
    }

    public void Invoke()
    {
        Start();
        Send();
        End();
    }

    protected virtual void Send()
    {
        Mediator.Send(this);
    }

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