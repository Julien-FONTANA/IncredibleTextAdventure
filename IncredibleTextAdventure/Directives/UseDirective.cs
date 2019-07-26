﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using IncredibleTextAdventure.ITAConsole;
using IncredibleTextAdventure.Service.Context;

namespace IncredibleTextAdventure.Directives
{
    public class UseDirective : IDirective
    {
        private IConsoleWriter _consoleWriter;
        private const string CmdPattern = @"^(use)";
        private const string FullPattern = @"^(use)[ \t]?(the)?[ \t]?(?<sourceObj>(.*))(on)[ \t]?(the)?[ \t]?(?<targetObj>(.*))";

        public UseDirective(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }

        public bool CanApply(string cmd)
        {
            return Regex.IsMatch(cmd, CmdPattern, RegexOptions.IgnoreCase);
        }

        public void TryApply(string cmd, GameContext context)
        {
            var match = Regex.Match(cmd, FullPattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                var sourceObj = match.Groups["sourceObj"]?.Value.Trim();
                var targetObj = match.Groups["targetObj"]?.Value.Trim();

                if (ReferenceEquals(sourceObj, null) || ReferenceEquals(targetObj, null))
                {
                    _consoleWriter.WriteToConsole("What are trying to do?");
                }

                var objectToUse = context.Player.GetItemFromInventory(sourceObj);
                if (ReferenceEquals(objectToUse, null))
                {
                    _consoleWriter.WriteToConsole("What are you trying to [use]? Is the object in your [inventory]?");
                    return;
                }

                var objectToUseOn = context.GetCurrentRoom().GetItemsInRoom().FirstOrDefault(i => i.Name.Equals(targetObj, StringComparison.OrdinalIgnoreCase));
                if (ReferenceEquals(objectToUseOn, null))
                {
                    _consoleWriter.WriteToConsole($"What are you trying to use [{objectToUse.Name}] on?");
                    return;
                }

                if (!objectToUseOn.CanInteractWith(sourceObj))
                {
                    _consoleWriter.WriteToConsole($"You can't use [{objectToUse.Name}] on [{objectToUseOn.Name}] !!!");
                    return;
                }

                // Add object
                objectToUseOn.InteractWith(context);
                context.Player.UseFromInventory(objectToUse);
            }
        }
    }
}
