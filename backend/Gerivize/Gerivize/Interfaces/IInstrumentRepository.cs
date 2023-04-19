using Gerivize.Models;

namespace Gerivize.Interfaces
{
    public interface IInstrumentRepository
    {
        public List<Instrument> getAll();
        public Instrument? getById(string aNumber);
        public List<Instrument> getByUserId(Guid userId);

        public Instrument createInstrument(Instrument instrument);
        public Instrument updateInstrument(Instrument instrument);
        public Instrument deleteInstrument(string aNumber);
    }
}
