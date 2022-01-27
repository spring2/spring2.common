using System;
using System.Threading.Tasks;
using Xunit;

namespace Cortside.Common.Threading.Tests {
    public class TaskExtensionsTest {
        public async Task DoStuffAsync() {
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
        }

        public async Task<bool> DoBoolStuffAsync() {
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            return true;
        }

        [Fact]
        public async Task ShouldThrowTimeoutException() {
            await Assert.ThrowsAsync<TimeoutException>(() => DoStuffAsync().WithTimeout(TimeSpan.FromMilliseconds(100))).ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldThrowTimeoutException2() {
            await Assert.ThrowsAsync<TimeoutException>(() => DoBoolStuffAsync().WithTimeout(TimeSpan.FromMilliseconds(100))).ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldNotThrowTimeoutException() {
            await DoStuffAsync().WithTimeout(TimeSpan.FromMilliseconds(300)).ConfigureAwait(false);
            Assert.True(true);
        }

        [Fact]
        public async Task ShouldNotThrowTimeoutException2() {
            await DoBoolStuffAsync().WithTimeout(TimeSpan.FromMilliseconds(300)).ConfigureAwait(false);
            Assert.True(true);
        }

        [Fact]
        public async Task ShouldThrowTimeoutException3() {
            await DoStuffAsync().WithUnwrappedTimeout(TimeSpan.FromMilliseconds(100)).ConfigureAwait(false);
            Assert.True(true);
        }

        [Fact]
        public async Task ShouldNotThrowTimeoutException3() {
            await DoStuffAsync().WithUnwrappedTimeout(TimeSpan.FromMilliseconds(300)).ConfigureAwait(false);
            Assert.True(true);
        }

        [Fact]
        public async Task ShouldUnwrapTimeout() {
            var b = await DoBoolStuffAsync().WithUnwrappedTimeout(TimeSpan.FromMilliseconds(100)).ConfigureAwait(false);
            Assert.False(b);
        }

        [Fact]
        public async Task ShouldUnwrapBoolStuff() {
            var b = await DoBoolStuffAsync().WithUnwrappedTimeout(TimeSpan.FromMilliseconds(300)).ConfigureAwait(false);
            Assert.True(b);
        }
    }
}
