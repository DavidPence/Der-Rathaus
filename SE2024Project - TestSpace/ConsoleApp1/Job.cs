class Job{
    public List<object> playerRats = new List<object>();
    public List<object> opForRats = new List<object>();
    int eventType;
    int payout = 0;

    //Constructor
    public Job(List<object> playerRats, List<object> opForRats, int eventType) {
        this.playerRats = playerRats;
        this.opForRats = opForRats;
        this.eventType = eventType;
    }

}