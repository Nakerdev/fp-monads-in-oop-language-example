using LanguageExt;

namespace Examples.Domain.TrafficTickets
{
    public sealed class TrafficTicket
    {
        public string Id { get; }
        public double Ammount { get; }
        public string DriverPersonalIndentificationCode { get; }
        public bool IsPaid { get; private set; }
        public Option<string> ChargeId { get; private set; }

        public PersistenceState State => new PersistenceState(
            id: Id,
            ammount: Ammount,
            driverPersonalIndentificationCode: DriverPersonalIndentificationCode,
            isPaid: IsPaid,
            chargeId: ChargeId.IfNoneUnsafe(() => null));

        public static TrafficTicket CreateUnpaidTrafficTicket(
            string id,
            double ammount,
            string driverPersonalIndentificationCode) 
        {
            return new TrafficTicket(
               id: id,
               ammount: ammount,
               driverPersonalIndentificationCode: driverPersonalIndentificationCode,
               isPaid: false,
               chargeId: null);
        }

        public TrafficTicket(PersistenceState state)
        {
            Id = state.Id;
            Ammount = state.Ammount;
            DriverPersonalIndentificationCode = state.DriverPersonalIndentificationCode;
            IsPaid = state.IsPaid;
            ChargeId = state.ChargeId;
        }

        private TrafficTicket(
            string id, 
            double ammount, 
            string driverPersonalIndentificationCode, 
            bool isPaid, 
            Option<string> chargeId)
        {
            Id = id;
            Ammount = ammount;
            DriverPersonalIndentificationCode = driverPersonalIndentificationCode;
            IsPaid = isPaid;
            ChargeId = chargeId;
        }

        public void MarkAsPaid(string chargeId)
        {
            IsPaid = true;
            ChargeId = chargeId;
        }

        public sealed class PersistenceState
        {
            public string Id { get; }
            public double Ammount { get; }
            public string DriverPersonalIndentificationCode { get; }
            public bool IsPaid { get; }
            public string ChargeId { get; }

            public PersistenceState(
                string id, 
                double ammount, 
                string driverPersonalIndentificationCode, 
                bool isPaid, 
                string chargeId)
            {
                Id = id;
                Ammount = ammount;
                DriverPersonalIndentificationCode = driverPersonalIndentificationCode;
                IsPaid = isPaid;
                ChargeId = chargeId;
            }
        }
    }
}
