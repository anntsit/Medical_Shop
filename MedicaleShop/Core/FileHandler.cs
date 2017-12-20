using MedicaleShop.Core.Classes;

namespace MedicaleShop.Core
{
    public class FileHandler
    {
        public static void Load()
        {
            if (!new Client().Load())
                Client.Items.Clear();
            if (!new Medicin().Load())
                Medicin.Items.Clear();
            if (!new Order().Load())
                Order.Items.Clear();
            if (!new Pharmacy().Load())
                Pharmacy.Items.Clear();
            if (!new Purchase().Load())
                Purchase.Items.Clear();
        }

        public static void Save()
        {
            if (Client.Items.Count > 0)
                Client.Indexer[0].Save();
            if (Medicin.Items.Count > 0)
                Medicin.Indexer[0].Save();
            if (Order.Items.Count > 0)
                Order.Indexer[0].Save();
            if (Pharmacy.Items.Count > 0)
                Pharmacy.Indexer[0].Save();
            if (Purchase.Items.Count > 0)
                Purchase.Indexer[0].Save();
        }
    }
}