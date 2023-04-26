using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands
{
    /// <summary>
    /// Макрокоманда
    /// </summary>
    public class MacroCommand : ICommand
    {
        /// <summary>
        /// Макрокоманда
        /// </summary>
        /// <param name="commands">Список команд</param>
        public MacroCommand(List<ICommand> commands)
        {
            this.commands = commands;
        }
        public bool CanUndo => true;

        protected List<ICommand> commands;

        protected Stack<ICommand> successCommands = new Stack<ICommand>();

        public async Task Execute()
        {
            try
            {
                for (int i = 0; i < commands.Count; i++)
                {
                    await commands[i].Execute();
                    successCommands.Push(commands[i]);
                }
            }
            catch
            {
                await Undo();
                throw;
            }
        }

        public async Task Undo()
        {
            while (successCommands.Count > 0)
            {
                var command = successCommands.Pop();
                if (command.CanUndo)
                {
                    await command.Undo();
                }
            }
        }
    }
}
