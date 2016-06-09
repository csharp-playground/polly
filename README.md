## Try Polly

https://github.com/App-vNext/Polly

```csharp
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
```


