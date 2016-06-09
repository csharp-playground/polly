using System;
using Polly;
using FluentAssertions;
using NUnit.Framework;

namespace TryPolly {
	public class PollySpec {

		[Test]
		public void ShouldRetry() {
			var tries = 0;
			var policy = Policy
				.Handle<Exception>(ex => ex.Message == "jw")
				.Retry();

			try {
				policy.Execute(() => {
					tries++;
					throw new Exception("jw");
				});
			} catch { }

			tries.Should().Be(2);
		}

		[Test]
		public void ShouldRetryMultipleTime() {
			var retry = 0;
			var policy = Policy
				.Handle<Exception>()
				.Retry(3, (ex, count) => {
					retry++;
				});

			try {
				policy.Execute(() => {
					throw new Exception();
				});
			} catch { }

			retry.Should().Be(3);
		}

		[Test]
		public void ShouldFilterException() {
			var tries = 0;
			var policy = Policy
				.Handle<Exception>(ex => ex.Message == "jw")
				.Retry();

			try {
				policy.Execute(() => {
					tries ++;
					throw new Exception("wk");
				});
			} catch { }

			tries.Should().Be(1);

		}
	}
}

