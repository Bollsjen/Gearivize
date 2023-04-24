

using Gerivize.EnumTypes;
using Gerivize.Repositories;

namespace Gerivize.Managers
{
    public class InstrumentsManager
    {
        private readonly InstrumentRepository _instrumentRepository;

        public InstrumentsManager()
        {
            _instrumentRepository = new InstrumentRepository();
        }
    }
}
