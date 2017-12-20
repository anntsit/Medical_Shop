using System.Runtime.Serialization;

namespace MedicaleShop.Core.Classes
{
    // Медикаменти
    [DataContract]
    public class Medicin : Base<Medicin>, IFileManager
    {
        public Medicin(string name) : base(name)
        {
        }

        public Medicin()
        {
        }

        [DataMember]
        public string Producer { get; set; } // Виробник

        [DataMember]
        public string UnitActiveSubstance { get; set; } // Одиниця виміру діючої речовини

        [DataMember]
        public int CountOfActiveSubstance { get; set; } // Кількість діючої речовини

        [DataMember]
        public int Packing { get; set; } // Кількість в упаковці

        [DataMember]
        public bool NeedRecipe { get; set; } // Рецепт

        [DataMember]
        public decimal Price { get; set; } // Ціна

        [DataMember]
        public string Appointment { get; set; } // Призначення (короткий опис призначення)

        [DataMember]
        public string Instruction { get; set; } // Інструкція (посилання на файл з інструкцією)

        public override string ToString()
        {
            return base.ToString() + " [шт. " + Packing + ", ціна: " + Price + "]";
        }
    }
}