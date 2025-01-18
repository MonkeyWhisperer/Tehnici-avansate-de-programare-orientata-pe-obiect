// Clasa care reprezinta starea medicamentului
public class StareMedicament
{
    public string Nume { get; set; }
    public double Doza { get; set; }
    public string ModAdministrare { get; set; }
    public DateTime DataExpirare { get; set; }

    public StareMedicament(string nume, double doza, string modAdministrare, DateTime dataExpirare)
    {
        Nume = nume;
        Doza = doza;
        ModAdministrare = modAdministrare;
        DataExpirare = dataExpirare;
    }
}

// Clasa Memento care salveaza starea medicamentului
public class MementoMedicament
{
    private StareMedicament _stare;

    public MementoMedicament(StareMedicament stare)
    {
        _stare = new StareMedicament(
            stare.Nume,
            stare.Doza,
            stare.ModAdministrare,
            stare.DataExpirare
        );
    }

    public StareMedicament ObtineStare()
    {
        return _stare;
    }
}

// Clasa Originator care gestioneaza starea curenta si creeaza memento-uri
public class Medicament
{
    private StareMedicament _stare;

    public void SetStare(string nume, double doza, string modAdministrare, DateTime dataExpirare)
    {
        _stare = new StareMedicament(nume, doza, modAdministrare, dataExpirare);
        Console.WriteLine($"Stare curenta: {nume}, Doza: {doza}, Mod administrare: {modAdministrare}, Data expirare: {dataExpirare}");
    }

    // Creeaza un memento cu starea curenta
    public MementoMedicament SalveazaStare()
    {
        return new MementoMedicament(_stare);
    }

    // Restaureaza starea din memento
    public void RestoreazaStare(MementoMedicament memento)
    {
        _stare = memento.ObtineStare();
        Console.WriteLine($"Restaurare stare: {_stare.Nume}, Doza: {_stare.Doza}, " +
                         $"Mod administrare: {_stare.ModAdministrare}, Data expirare: {_stare.DataExpirare}");
    }
}

// Clasa Caretaker care pastreaza istoricul starilor
public class IstoricMedicament
{
    private List<MementoMedicament> _istoricStari = new List<MementoMedicament>();
    private Medicament _medicament;

    public IstoricMedicament(Medicament medicament)
    {
        _medicament = medicament;
    }

    public void SalveazaStare()
    {
        _istoricStari.Add(_medicament.SalveazaStare());
        Console.WriteLine("Stare salvata in istoric");
    }

    public void AnuleazaUltimaModificare()
    {
        if (_istoricStari.Count == 0)
        {
            Console.WriteLine("Nu exista stari salvate!");
            return;
        }

        var memento = _istoricStari[_istoricStari.Count - 1];
        _istoricStari.RemoveAt(_istoricStari.Count - 1);
        _medicament.RestoreazaStare(memento);
    }
}

// Exemplu de utilizare
public class Program
{
    public static void Main()
    {
        var medicament = new Medicament();
        var istoric = new IstoricMedicament(medicament);

        // Setam starea initiala
        medicament.SetStare("Paracetamol", 500, "oral", DateTime.Now.AddYears(2));
        istoric.SalveazaStare();

        // Modificam starea prima data
        medicament.SetStare("Paracetamol", 1000, "oral", DateTime.Now.AddYears(3));
        istoric.SalveazaStare();

        // Modificam starea a doua oara
        medicament.SetStare("Paracetamol", 750, "oral", DateTime.Now.AddYears(4));
        istoric.SalveazaStare();

        // Prima restaurare
        Console.WriteLine("\nPrima restaurare la starea anterioara:");
        istoric.AnuleazaUltimaModificare();

        // A doua restaurare
        Console.WriteLine("\nA doua restaurare la starea anterioara:");
        istoric.AnuleazaUltimaModificare();

        // A treia restaurare
        Console.WriteLine("\nA doua restaurare la starea anterioara:");
        istoric.AnuleazaUltimaModificare();
    }
}