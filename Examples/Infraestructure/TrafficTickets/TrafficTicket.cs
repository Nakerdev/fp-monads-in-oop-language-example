namespace Examples.Infraestructure.TrafficTickets
{
    public sealed class TrafficTicket
    {
        public string Id { get; set; }
        public double Ammount { get; set; }
        public string DriverPersonalIndentificationCode { get; set; }
        public bool IsPaid { get; set; }
        public string ChargeId { get; set; }
    }
}