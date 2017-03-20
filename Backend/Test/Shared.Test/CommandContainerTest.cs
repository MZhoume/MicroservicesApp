namespace Shared.Test
{
    using System;
    using Shared.Container;
    using Shared.Interface;
    using Shared.Request;
    using Shared.Response;
    using Xunit;

    public class CommandContainerTest
    {
        private class TestCommand : ICommand
        {
            public Response Invoke(Request request)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void ContainerShouldWork()
        {
            var container = new CommandContainer();
            container.Register<TestCommand>(Operation.Create);

            Assert.Equal(typeof(TestCommand), container[Operation.Create].GetType());
        }

        private class TestCommand2 : ICommand
        {
            private ICommand command;

            public TestCommand2(ICommand command)
            {
                this.command = command;
            }

            public ICommand Test()
            {
                return this.command;
            }

            public Response Invoke(Request request)
            {
                throw new NotImplementedException();
            }
        }

        private class TestCommand3 : ICommand
        {
            private ICommand command;

            public TestCommand3(TestCommand command)
            {
                this.command = command;
            }

            public ICommand Test()
            {
                return this.command;
            }

            public Response Invoke(Request request)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void ContainerShouldWorkWithConcreteRequirement()
        {
            var container = new CommandContainer();
            container.RegisterRequirement<TestCommand>()
                     .Register<TestCommand3>(Operation.Create);

            Assert.Equal(typeof(TestCommand3), container[Operation.Create].GetType());
            Assert.Equal(typeof(TestCommand), ((TestCommand3)container[Operation.Create]).Test().GetType());
        }

        [Fact]
        public void ContainerShouldWorkWithInterfaceRequirement()
        {
            var container = new CommandContainer();
            container.RegisterRequirement<ICommand, TestCommand>()
                     .Register<TestCommand2>(Operation.Create);

            Assert.Equal(typeof(TestCommand2), container[Operation.Create].GetType());
            Assert.Equal(typeof(TestCommand), ((TestCommand2)container[Operation.Create]).Test().GetType());
        }

        [Fact]
        public void ContainerShouldWorkWithRequirementConcrete()
        {
            var container = new CommandContainer();
            container.RegisterRequirement<ICommand>(() => new TestCommand())
                     .Register<TestCommand2>(Operation.Create);

            Assert.Equal(typeof(TestCommand2), container[Operation.Create].GetType());
            Assert.Equal(typeof(TestCommand), ((TestCommand2)container[Operation.Create]).Test().GetType());
        }
    }
}