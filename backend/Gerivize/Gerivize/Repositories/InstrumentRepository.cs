using Gerivize.Interfaces;
using Gerivize.Models;

namespace Gerivize.Repositories
{
    public class InstrumentRepository : IInstrumentRepository
    {
        private readonly GearivizeLocalContext _localContext;

        public InstrumentRepository()
        {
            _localContext = new GearivizeLocalContext();
        }

        public List<Instrument> getAll()
        {
            return _localContext.Instruments.ToList();
        }

        public Instrument? getById(string aNumber)
        {
            return _localContext.Instruments.ToList().Find(i => i.ANumber == aNumber);
        }

        public List<Instrument> getByUserId(Guid userId)
        {
            return _localContext.Instruments.Where(i => i.UserId == userId).ToList();
        }
        public Instrument createInstrument(Instrument instrument)
        {
            instrument.ANumber = nextANumber();
            _localContext.Instruments.Add(instrument);
            _localContext.SaveChanges();
            return instrument;
        }
        public Instrument updateInstrument(Instrument instrument)
        {
            _localContext.Instruments.Update(instrument);
            _localContext.SaveChanges();
            return instrument;
        }
        public Instrument deleteInstrument(string aNumber)
        {
            Instrument instrument = getById(aNumber);
            instrument.Inactive = true;
            _localContext.Update(instrument);
            _localContext.SaveChanges();
            return instrument;
        }

        private string nextANumber()
        {
            List<Instrument> instruments = _localContext.Instruments.ToList();
            List<Instrument> list = instruments.OrderBy(i => int.Parse(new string(i.ANumber.Where(char.IsDigit).ToArray()))).ThenBy(s=>s).ToList();
            int lastNumber = 1;
            if(instruments.Count > 0)
            {
                string lastANumber = list.Last().ANumber;
                lastANumber = lastANumber.Replace('A', ' ');
                lastANumber = lastANumber.Trim();
                lastNumber = Convert.ToInt32(lastANumber);
                lastNumber++;
            }
            string nextANumber = "A" + lastNumber;
            return nextANumber;
        }
    }
}
