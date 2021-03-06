﻿namespace Minuteman.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Xunit;

    public class UserActivityPubSubTests : IDisposable
    {
        private readonly UserActivity publisher;

        public UserActivityPubSubTests()
        {
            publisher = new UserActivity(
                new ActivitySettings(1, ActivityDrilldown.Year));
            publisher.Reset().Wait();
        }

        public void Dispose()
        {
            publisher.Reset().Wait();
        }

        [Fact]
        public async Task ExchangesMessage()
        {
            const string EventName = "login";
            var timestamp = DateTime.UtcNow;

            var signal = new ManualResetEvent(false);

            var subscription = publisher.CreateSubscription(
                EventName,
                e =>
                    {
                        Assert.Equal(EventName, e.EventName);
                        Assert.Equal(timestamp, e.Timestamp);
                        Assert.Contains(1, e.Users);
                        Assert.Contains(2, e.Users);
                        Assert.Contains(3, e.Users);
                        signal.Set();
                    });

            await subscription.Subscribe();

            await publisher.Track(EventName, true, 1, 2, 3);

            signal.WaitOne(TimeSpan.FromSeconds(1));

            await subscription.Unsubscribe();
            subscription.Dispose();
        }
    }
}