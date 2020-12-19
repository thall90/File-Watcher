using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Reflection;

namespace FileWatcher.Extensions
{
    public static class CommandExtensions
    {
        public static Command WithHandler<TCommandClass>(
            this Command command, 
            string methodName,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic)
            where TCommandClass : class
        {
            var classType = typeof(TCommandClass);
            var method = classType.GetMethod(methodName, flags);

            if (method == null)
            {
                throw new MissingMethodException(classType.Name, methodName);
            }
            
            var handler = CommandHandler.Create(method!);
            
            command.Handler = handler;
            
            return command;
        }
    }
}