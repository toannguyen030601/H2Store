using asm1.Entities;

namespace asm1.Models
{
    public class DienThoaiModel
    {
        private AspmvcH2storeContext db = new AspmvcH2storeContext();

        public List<DienThoai> FindAll()
        {
            var a = db.DienThoais.ToList();
            return a;
        }

        public DienThoai Find(int id)
        {
            var a = db.DienThoais.Find(id);
            return a;
        }
    }
}
