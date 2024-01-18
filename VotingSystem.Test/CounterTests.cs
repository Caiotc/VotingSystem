using System.Diagnostics.Metrics;

namespace VotingSystem.Test;

public class CounterTests
{
    [Fact]
    public void CounterHasName(){
        var name = "Counter Name";
        var counter = new Counter();
        
        Assert.Equal(name, counter.Name);
    }

}

public class Counter
{
    public string Name { get; set; }
    public Counter()
    {
    }
}