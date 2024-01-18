using static Xunit.Assert;

namespace VotingSystem.Test;

public class CounterManagerTests
{
    public const string CounterName = "Counter Name";
    public Counter _counter = new Counter() { Name = CounterName,Count= 5 };


    [Fact]
    public void GetStatistcs_IncludesCounterName(){

        var statistics = new CounterManager().GetStatistics(_counter,5);
        Equal(CounterName,statistics.Name);
    }

    [Fact]
    public void GetStatistics_IncludeCounterCount(){
        var statistics = new CounterManager().GetStatistics(_counter,5);
        Equal(5,statistics.Count);
    }

    [Theory]
    [InlineData(5,10,50)]
    [InlineData(1,3,33.33)]
    [InlineData(2,3,66.67)]
    [InlineData(2,8,25)]
    public void GetStatistics_ShowsPercentageUpToTwoDecimalsBasedOnTotalCount(int count,int totalCount,double expectedPercentage){
        _counter.Count = count;
        var statistics = new CounterManager().GetStatistics(_counter,totalCount);
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

    [Fact]
    public void ResolveExcess_AddsExcessToHighestCounter(){
        var counter1 = new Counter(){Count =2, Percentage = 66.66};
        var counter2 = new Counter(){Count =1, Percentage = 33.33};

        var counters = new List<Counter>(){counter1,counter2};
        new CounterManager().ResolveExcess(counters);

        Equal(66.67,counter1.Percentage);
        Equal(33.33,counter2.Percentage);
        
    }

    [Fact]
    public void ResolveExcess_AddsExcessToLowestCounterWhenMoreThanOneHighestCounter(){
        var counter1 = new Counter(){Count =2, Percentage = 44.44};
        var counter2 = new Counter(){Count =2, Percentage = 44.44};
        var counter3 = new Counter(){Count =1, Percentage = 11.11};

        var counters = new List<Counter>(){counter1,counter2,counter3};
        new CounterManager().ResolveExcess(counters);

        Equal(44.44,counter1.Percentage);
        Equal(44.44,counter2.Percentage);
        Equal(11.12,counter3.Percentage);
        
    }

}

public class Counter
{
    public string Name { get; set; }
    public int Count { get; set; }
    public double Percentage { get; set; }


}

public class CounterManager{

    internal Counter GetStatistics(Counter counter,int totalCount)
    {
        counter.Percentage = RoundUp((double)counter.Count * 100 / totalCount);
        return counter;
    }

    public void ResolveExcess(List<Counter> counters)
    {
        var totalPercentage = counters.Sum(x => x.Percentage);
        if(totalPercentage == 100) return;

        var excess = 100 - totalPercentage;

        var highestPercentage = counters.Max(c => c.Percentage);
        var highestCounters = counters.Where(c => c.Percentage == highestPercentage).ToList();

        if(highestCounters.Count == 1){
            counters[0].Percentage += excess;
        }

        else if(highestCounters.Count < counters.Count){
            var lowestPercentage = counters.Min(c => c.Percentage);
            var lowestCounter = counters.First(c => c.Percentage == lowestPercentage);
            lowestCounter.Percentage = RoundUp(lowestCounter.Percentage + excess);
            
            
        }
        
    
    }

    public static double RoundUp(double value)
    {
        return Math.Round(value,2);
    }
}