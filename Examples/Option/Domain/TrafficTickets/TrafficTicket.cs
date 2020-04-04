using System;
using LanguageExt;

namespace Examples.Option.Domain.TrafficTickets
{
    public sealed class TrafficTicket
    {
        public string Id { get; }
        public double Ammount { get; }
        public string DriverPersonalIndentificationCode { get; }
        public bool IsPaid { get; private set; }
        public Option<string> ChargeId { get; private set; }

        public void MarkAsPaid(string chargeId)
        {
            IsPaid = true;
            ChargeId = chargeId;
        }
    }
}
