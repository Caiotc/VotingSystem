using static Xunit.Assert;

namespace VotingSystem.Test;

public class CounterTests
{
    public const string CounterName = "Counter Name";
    public Counter _counter = new Counter() { Name = CounterName,Count= 5 };
    [Fact]
    public void  HasName(){

        
        Equal(CounterName, _counter.Name);
    }

    [Fact]
    public void GetStatistcs_IncludesCounterName(){

        var statistics = _counter.GetStatistics(5);
        Equal(CounterName,statistics.Name);
    }

    [Fact]
    public void GetStatistics_IncludeCounterCount(){
        var statistics = _counter.GetStatistics(5);
        Equal(5,statistics.Count);
    }

    [Theory]
    [InlineData(5,10,50)]
    public void GetStatistics_ShowsPercentageUpToTwoDecimalsBasedOnTotalCount(int count,int totalCount,double expectedPercentage){
        _counter.Count = count;
        var statistics = _counter.GetStatistics(totalCount);
        Equal(expectedPercentage,statistics.Percentage);
    
    }

    [Fact]
    public void ResolveExcess_DoesntAddExcessWhenAllCountersAreEqual(){
        var counter1 = new Counter() { Count= 1,Percentage= 33.33};
        var counter2 = new Counter() { Count= 1,Percentage= 33.33};
        var counter3 = new Counter() { Count= 1,Percentage= 33.33};

        var counters = new List<Counter>(){counter1,counter2,counter3};

        new CounterManager().ResolveExcess(counters);

        Equal(33.33,counter1.Percentage);
        Equal(33.33,counter2.Percentage);
        Equal(33.33,counter3.Percentage);




    }

}

public class Counter
{
    public string Name { get; set; }
    public int Count { get; set; }
    public double Percentage { get; set; }

    internal Counter GetStatistics(int totalCount)
    {
        Percentage = Math.Round(Count*100.0 / totalCount,2);
        return this;
    }
}

public class CounterManager{
    public void ResolveExcess(List<Counter> counters)
    {
        var highestPercentage = counters.Max(c => c.Percentage);
        var highestCounters = counters.Where(c => c.Percentage == highestPercentage).ToList();

        if(highestCounters.Count == 1){
            counters[0].Percentage += 0.01;
        }

        else if(highestCounters.Count < counters.Count){
            var lowestPercentage = counters.Min(c => c.Percentage);
            var lowestCounters = counters.Where(c => c.Percentage == lowestPercentage).ToList();
        }
    }
}