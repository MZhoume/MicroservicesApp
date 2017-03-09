namespace Shared.Container
{
    using System;
    using System.Collections.Generic;
    using Shared.Interface;
    using Shared.Request;

    /// <summary>
    /// Container service for commands
    /// </summary>
    public class CommandContainer
    {
        private Dictionary<Operation, Type> container = new Dictionary<Operation, Type>();

        /// <summary>
        /// Register a command with the key
        /// </summary>
        public CommandContainer Register<TCommand>(Operation operation)
        where TCommand : ICommand
        {
            container.Add(operation, typeof(TCommand));

            return this;
        }

        /// <summary>
        /// Gets the registered command with given key
        /// </summary>
        public ICommand this[Operation key]
        {
            get
            {
                if (!container.ContainsKey(key))
                {
                    throw new NotImplementedException($"Key {key} is not supported.");
                }

                return (ICommand)Activator.CreateInstance(container[key]);
            }
        }
    }
}