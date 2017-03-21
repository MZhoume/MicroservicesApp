namespace Shared.Container
{
    using System;
    using System.Collections.Generic;
    using Shared.Interface;
    using Shared.Request;
    using SimpleInjector;

    /// <summary>
    /// Container service for commands
    /// </summary>
    public class CommandContainer
    {
        private readonly Dictionary<Operation, Type> commands = new Dictionary<Operation, Type>();
        private readonly Container container = new Container();

        /// <summary>
        /// Gets the registered command with given key
        /// </summary>
        /// <param name="operation"> The Operation to register </param>
        public ICommand this[Operation operation]
        {
            get
            {
                if (!this.commands.ContainsKey(operation))
                {
                    throw new ArgumentException($"Operation {Enum.GetName(typeof(Operation), operation)} is not supported.");
                }

                return this.container.GetInstance(this.commands[operation]) as ICommand;
            }
        }

        /// <summary>
        /// Register a command with the key
        /// </summary>
        /// <param name="operation"> The Operation to register </param>
        /// <typeparam name="TCommand"> The type of the command </typeparam>
        /// <returns> This for chaining call </returns>
        public CommandContainer Register<TCommand>(Operation operation)
        where TCommand : ICommand
        {
            if (this.commands.ContainsKey(operation))
            {
                throw new ArgumentException($"{Enum.GetName(typeof(Operation), operation)} already registered.");
            }

            this.commands.Add(operation, typeof(TCommand));
            return this;
        }

        /// <summary>
        /// Register a requirement for the commands
        /// </summary>
        /// <typeparam name="TConcrete"> The type of the instance </typeparam>
        /// <returns> This for chaining call</returns>
        public CommandContainer RegisterRequirement<TConcrete>()
        where TConcrete : class
        {
            this.container.Register<TConcrete>();
            return this;
        }

        /// <summary>
        /// Register a requirement for the commands
        /// </summary>
        /// <typeparam name="TInterface"> The type of the interface </typeparam>
        /// <typeparam name="TConcrete"> The type of the instance </typeparam>
        /// <returns> This for chaining call</returns>
        public CommandContainer RegisterRequirement<TInterface, TConcrete>()
        where TInterface : class
        where TConcrete : class, TInterface
        {
            this.container.Register<TInterface, TConcrete>();
            return this;
        }

        /// <summary>
        /// Register a requirement for the commands
        /// </summary>
        /// <param name="func"> The func that returns the instance </param>
        /// <typeparam name="TType"> The type of the interface </typeparam>
        /// <returns> This for chaining call</returns>
        public CommandContainer RegisterRequirement<TType>(Func<TType> func)
        where TType : class
        {
            this.container.Register<TType>(func);
            return this;
        }
    }
}