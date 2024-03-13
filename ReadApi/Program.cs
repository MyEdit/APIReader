using ReadApi;
using System.Net;

APIManager APIManager = new();

await APIManager.createMedicine(new Medicines(100, "Глицин", "Артемовский", 1000));
await APIManager.deleteMedicine(0);
await APIManager.updateMedicine(0, new Medicines(100, "Новый препарат", "Новый Склад", 0));

Medicines medicine = await APIManager.getMedicine(0);
if (medicine != null)
{
    Console.WriteLine(medicine.Name);
    Console.WriteLine("========================");
    Console.WriteLine();
}

List<Medicines> medicines = await APIManager.getMedicines();
if (medicines != null)
{
    foreach (Medicines item in medicines)
    {
        Console.WriteLine(item.ID);
        Console.WriteLine(item.Name);
        Console.WriteLine(item.Storage);
        Console.WriteLine(item.Count);
        Console.WriteLine();
    }
}
