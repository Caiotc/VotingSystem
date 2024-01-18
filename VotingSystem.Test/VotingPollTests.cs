namespace VotingSystem.Test;

public class VotingPollTests
{
    [Fact]
    public void ZeroCountersWhenCreated(){
        var poll = new VotingPoll();
        Assert.Empty(poll.Counters);
    }

}

public class VotingPollFactoryTests
{
    private VotingPollFactory _factory = new VotingPollFactory();

    private VotingPollFactory.Request _request = new VotingPollFactory.Request(){
        Names = new[] {"name","name1"},
        Description = "description",
        Title = "title",
    };

    [Fact]
    public void Create_ThrowsWhenLessThanTwoCounterNames(){
        
        
        _request.Names = new[]{"name"};
        Assert.Throws<ArgumentException>(()=>_factory.Create(_request));
        _request.Names = new string[]{};
        Assert.Throws<ArgumentException>(()=>_factory.Create(_request));
            
    }

    [Fact]
    public void Create_CreatesCounterForEachProvidedName(){
        
        var poll = _factory.Create(_request);

        foreach(var name in _request.Names){
            Assert.Contains(poll.Counters,c=>c.Name == name);
        }

            
    }

    [Fact]
    public void Create_AddsTItleToThePoll(){
       
        var poll = _factory.Create(_request);

        Assert.Equal(_request.Title,poll.Title);
                
    }

    [Fact]
    public void Create_AddsDescriptionTothePoll(){
        var poll = _factory.Create(_request);
        Assert.Equal(_request.Description,poll.Description);
    }
}

internal class VotingPollFactory
{
    
    public class Request{
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Names { get; set; }
    }

    public VotingPoll Create(Request request)
    {
        if(request.Names.Length < 2){
            throw new ArgumentException("Must provide at least two names for a voting session");
        }

        return new VotingPoll(){
            Counters = request.Names.Select(n=>new Counter(){Name = n}),
            Title = request.Title,
            Description = request.Description            
        };
    }
        
}


internal class VotingPoll
{
    public IEnumerable<Counter> Counters { get;set ;}
    public string Title { get; set; }

    public string Description { get; set; }

    public VotingPoll()
    {
        Counters = Enumerable.Empty<Counter>();
    }
}