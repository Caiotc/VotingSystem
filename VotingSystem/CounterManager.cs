using VotingSystem.Models;

namespace VotingSystem;

public class CounterManager
{
    public Counter GetStatistics(Counter counter,int totalCount)
    {
        counter.Percent = RoundUp(counter.Count / totalCount;
        throw new NotImplementedException();
    }

}
