using Gerivize.Interfaces;

namespace Gerivize.Managers
{
    public class InstrumentsManager : IInstrumentsManager
    {
        private readonly IInstrumentRepository _instrumentRepository;

        public InstrumentsManager(IInstrumentRepository instrumentRepository)
        {
            _instrumentRepository = instrumentRepository;
        }
    }
}
