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
    }
}