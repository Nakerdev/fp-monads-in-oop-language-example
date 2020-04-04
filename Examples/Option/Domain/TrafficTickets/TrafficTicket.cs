namespace Examples.Option.Domain.TrafficTickets
{
    public sealed class TrafficTicket
    {
        public string Id { get; }
        public double Ammount { get; }
        public Driver Driver { get; }
    }

    public sealed class Driver
    {
        public string PersonalIndetificationCode { get; }
    }
}
