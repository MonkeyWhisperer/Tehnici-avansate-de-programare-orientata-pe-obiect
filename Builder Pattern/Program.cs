using System;

// Clasa Medicament reprezinta obiectul complex care va fi construit folosind Builder Pattern
class Medicament
{
    // Proprietati ale medicamentului
    public string Nume { get; set; } // Numele medicamentului
    public string Concentratie { get; set; } // Concentratia medicamentului
    public string FormaFarmaceutica { get; set; } // Forma farmaceutica (ex: comprimate, capsule)
    public double Pret { get; set; } // Pretul medicamentului

    // Metoda pentru afisarea detaliilor medicamentului
    public void ShowDetails()
    {
        Console.WriteLine("Detalii Medicament:");
        Console.WriteLine($"Nume: {Nume}");
        Console.WriteLine($"Concentratie: {Concentratie}");
        Console.WriteLine($"Forma farmaceutica: {FormaFarmaceutica}");
        Console.WriteLine($"Pret: {Pret} RON");
    }
}

// Clasa abstracta MedicamentBuilder defineste structura unui builder pentru crearea obiectului Medicament
abstract class MedicamentBuilder
{
    protected Medicament medicament; // Obiectul Medicament care va fi construit

    public MedicamentBuilder()
    {
        medicament = new Medicament(); // Initializarea unui obiect Medicament gol
    }

    // Metode abstracte care vor fi implementate de clasele concrete pentru a construi parti ale obiectului
    public abstract void BuildNume();
    public abstract void BuildConcentratie();
    public abstract void BuildFormaFarmaceutica();
    public abstract void BuildPret();

    // Metoda pentru obtinerea obiectului Medicament construit
    public Medicament GetMedicament()
    {
        return medicament;
    }
}

// Clasa concreta pentru construirea unui analgezic (concretizeaza MedicamentBuilder)
class AnalgezicBuilder : MedicamentBuilder
{
    public override void BuildNume()
    {
        medicament.Nume = "Paracetamol"; // Seteaza numele analgezicului
    }

    public override void BuildConcentratie()
    {
        medicament.Concentratie = "500mg"; // Seteaza concentratia analgezicului
    }

    public override void BuildFormaFarmaceutica()
    {
        medicament.FormaFarmaceutica = "Comprimate"; // Seteaza forma farmaceutica
    }

    public override void BuildPret()
    {
        medicament.Pret = 10.0; // Seteaza pretul analgezicului
    }
}

// Clasa concreta pentru construirea unui antibiotic (concretizeaza MedicamentBuilder)
class AntibioticBuilder : MedicamentBuilder
{
    public override void BuildNume()
    {
        medicament.Nume = "Amoxicilina"; // Seteaza numele antibioticului
    }

    public override void BuildConcentratie()
    {
        medicament.Concentratie = "250mg"; // Seteaza concentratia antibioticului
    }

    public override void BuildFormaFarmaceutica()
    {
        medicament.FormaFarmaceutica = "Capsule"; // Seteaza forma farmaceutica
    }

    public override void BuildPret()
    {
        medicament.Pret = 25.0; // Seteaza pretul antibioticului
    }
}

// Clasa MedicamentDirector controleaza procesul de construire a obiectului Medicament
class MedicamentDirector
{
    private MedicamentBuilder builder; // Instanta builderului utilizat pentru constructie

    // Metoda pentru setarea unui builder
    public void SetBuilder(MedicamentBuilder builder)
    {
        this.builder = builder;
    }

    // Metoda pentru construirea obiectului Medicament pas cu pas
    public void ConstructMedicament()
    {
        builder.BuildNume(); // Construieste numele
        builder.BuildConcentratie(); // Construieste concentratia
        builder.BuildFormaFarmaceutica(); // Construieste forma farmaceutica
        builder.BuildPret(); // Construieste pretul
    }
}

 class Program
{
    static void Main(string[] args)
    {
        // Creare director pentru gestionarea procesului de construire
        MedicamentDirector director = new MedicamentDirector();

        // Construire si afisare detalii pentru un analgezic
        MedicamentBuilder analgezicBuilder = new AnalgezicBuilder();
        director.SetBuilder(analgezicBuilder); // Setarea builderului pentru analgezic
        director.ConstructMedicament(); // Construirea analgezicului
        Medicament analgezic = analgezicBuilder.GetMedicament(); // Obtine obiectul construit

        Console.WriteLine("Analgezic");
        analgezic.ShowDetails(); // Afisarea detaliilor analgezicului
        Console.WriteLine();

        // Construire si afisare detalii pentru un antibiotic
        MedicamentBuilder antibioticBuilder = new AntibioticBuilder();
        director.SetBuilder(antibioticBuilder); // Setarea builderului pentru antibiotic
        director.ConstructMedicament(); // Construirea antibioticului
        Medicament antibiotic = antibioticBuilder.GetMedicament(); // Obtine obiectul construit

        Console.WriteLine("Antibiotic");
        antibiotic.ShowDetails(); // Afisarea detaliilor antibioticului
    }
}
