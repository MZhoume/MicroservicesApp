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
        private readonly Dictionary<Operation, Type> container = new Dictionary<Operation, Type>();

        /// <summary>
        /// Gets the registered command with given key
        /// </summary>
        /// <param name="operation"> The Operation to register </param>
        public ICommand this[Operation operation] => (ICommand)Activator.CreateInstance(this.container[operation]);

        /// <summary>
        /// Register a command with the key
        /// </summary>
        /// <param name="operation"> The Operation to register </param>
        /// <typeparam name="TCommand"> The type of the command </typeparam>
        /// <returns> This for chining call </returns>
        public CommandContainer Register<TCommand>(Operation operation)
        where TCommand : ICommand
        {
            this.container.Add(operation, typeof(TCommand));

            return this;
        }
    }
}