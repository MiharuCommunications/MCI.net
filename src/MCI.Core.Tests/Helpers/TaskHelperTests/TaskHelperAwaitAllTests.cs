namespace Miharu.Core.Tests.Helpers.TaskHelperTests
{
    using Miharu.Errors;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class TaskHelperAwaitAllTests
    {
        [Fact]
        public async Task AwaitAll2TestAsync()
        {
            var task1 = new Task<int>(() => 1);
            var task2 = new Task<int>(() => 1);

            await Task.Factory.StartNew(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(2));

                task1.Start();
                task2.Start();
            });

            var result = await TaskHelper.AwaitAll(task1, task2);

            Assert.Equal(2, result.Item1 + result.Item2);
        }

        [Fact]
        public async Task AwaitAll2WithTimeoutTestAsync()
        {
            var task1 = new Task<int>(() => 1);
            var task2 = new Task<int>(() => 1);

            await Task.Factory.StartNew(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(2));

                task1.Start();
                task2.Start();
            });

            var result = await TaskHelper.AwaitAll(task1, task2, TimeSpan.FromSeconds(5));

            Assert.True(result.IsRight);
            Assert.Equal(2, result.Get().Item1 + result.Get().Item2);
        }

        [Fact]
        public async Task AwaitAll2WithTimeoutTestTimeoutAsync()
        {
            var task1 = new Task<int>(() => 1);
            var task2 = new Task<int>(() => 1);

            await Task.Factory.StartNew(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));

                task1.Start();
                task2.Start();
            });

            var result = await TaskHelper.AwaitAll(task1, task2, TimeSpan.FromSeconds(2));

            Assert.True(result.IsLeft);
            Assert.IsType<TimeoutError>(result.Left.Get());
        }
    }
}
