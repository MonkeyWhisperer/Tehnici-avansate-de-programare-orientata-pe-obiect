// Interfata pentru comanda
public interface IComanda
{
    void Executa();
    void Anuleaza();
}

// Clasa care reprezinta medicamentul
public class Medicament
{
    public string Nume { get; set; }
    public int Stoc { get; private set; }

    public Medicament(string nume, int stocInitial)
    {
        Nume = nume;
        Stoc = stocInitial;
    }

    public void AdaugaStoc(int cantitate)
    {
        Stoc += cantitate;
        Console.WriteLine($"Adaugat {cantitate} la stocul medicamentului {Nume}. Stoc actual: {Stoc}");
    }

    public void ScoateStoc(int cantitate)
    {
        if (cantitate > Stoc)
        {
            Console.WriteLine($"Nu se poate scoate {cantitate} din stocul medicamentului {Nume}. Stoc insuficient.");
        }
        else
        {
            Stoc -= cantitate;
            Console.WriteLine($"Scoatere {cantitate} din stocul medicamentului {Nume}. Stoc actual: {Stoc}");
        }
    }
}

// Comanda pentru adaugarea de stoc
public class AdaugaStocComanda : IComanda
{
    private readonly Medicament _medicament;
    private readonly int _cantitate;

    public AdaugaStocComanda(Medicament medicament, int cantitate)
    {
        _medicament = medicament;
        _cantitate = cantitate;
    }

    public void Executa()
    {
        _medicament.AdaugaStoc(_cantitate);
    }

    public void Anuleaza()
    {
        _medicament.ScoateStoc(_cantitate);
    }
}

// Comanda pentru scoaterea din stoc
public class ScoateStocComanda : IComanda
{
    private readonly Medicament _medicament;
    private readonly int _cantitate;

    public ScoateStocComanda(Medicament medicament, int cantitate)
    {
        _medicament = medicament;
        _cantitate = cantitate;
    }

    public void Executa()
    {
        _medicament.ScoateStoc(_cantitate);
    }

    public void Anuleaza()
    {
        _medicament.AdaugaStoc(_cantitate);
    }
}

// Clasa pentru gestionarea comenzilor
public class GestionareComenzi
{
    private readonly Stack<IComanda> _istoricComenzi = new Stack<IComanda>();

    public void ExecutaComanda(IComanda comanda)
    {
        comanda.Executa();
        _istoricComenzi.Push(comanda);
    }

    public void AnuleazaUltimaComanda()
    {
        if (_istoricComenzi.Count > 0)
        {
            var ultimaComanda = _istoricComenzi.Pop();
            ultimaComanda.Anuleaza();
        }
        else
        {
            Console.WriteLine("Nu exista comenzi de anulat.");
        }
    }
}

// Exemplu de utilizare
public class Program
{
    public static void Main(string[] args)
    {
        Medicament paracetamol = new Medicament("Paracetamol", 100);

        GestionareComenzi gestionareComenzi = new GestionareComenzi();

        // Creare comenzi
        IComanda adaugaStoc = new AdaugaStocComanda(paracetamol, 50);
        IComanda scoateStoc = new ScoateStocComanda(paracetamol, 30);

        // Executare comenzi
        gestionareComenzi.ExecutaComanda(adaugaStoc);
        gestionareComenzi.ExecutaComanda(scoateStoc);

        // Anulare ultima comanda
        gestionareComenzi.AnuleazaUltimaComanda();
    }
}
