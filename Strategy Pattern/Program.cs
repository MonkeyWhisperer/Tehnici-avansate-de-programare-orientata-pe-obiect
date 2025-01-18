using System;
using System.Collections.Generic;

// Interfata pentru strategia de tratament
public interface IStrategieTratament
{
    void AplicaTratament(Pacient pacient);
    string GetDescriere();
}

// Clasa pentru pacient
public class Pacient
{
    public string Nume { get; set; }
    public int Varsta { get; set; }
    public List<string> Alergii { get; set; }
    public List<string> AfectiuniExistente { get; set; }
    public double Temperatura { get; set; }
    public int Puls { get; set; }
    public int TensiuneSistolica { get; set; }

    public Pacient(string nume, int varsta)
    {
        Nume = nume;
        Varsta = varsta;
        Alergii = new List<string>();
        AfectiuniExistente = new List<string>();
    }

    public override string ToString()
    {
        return $"Pacient: {Nume}, Varsta: {Varsta}, " +
               $"Temperatura: {Temperatura}°C, Puls: {Puls}, Tensiune: {TensiuneSistolica}";
    }
}

// Strategii concrete pentru diferite tipuri de tratamente

public class TratamentAntibiotics : IStrategieTratament
{
    private string tipAntibiotic;
    private int durataTratament;

    public TratamentAntibiotics(string tipAntibiotic, int durataTratament)
    {
        this.tipAntibiotic = tipAntibiotic;
        this.durataTratament = durataTratament;
    }

    public void AplicaTratament(Pacient pacient)
    {
        if (pacient.Alergii.Contains(tipAntibiotic))
        {
            Console.WriteLine($"ATENTIE: Pacientul este alergic la {tipAntibiotic}!");
            return;
        }

        Console.WriteLine($"Se aplica tratament cu antibiotic {tipAntibiotic} pentru {durataTratament} zile");
        Console.WriteLine($"Se monitorizeaza efectele adverse pentru {pacient.Nume}");
    }

    public string GetDescriere()
    {
        return $"Tratament antibiotic cu {tipAntibiotic} - {durataTratament} zile";
    }
}

public class TratamentAntitermic : IStrategieTratament
{
    private double pragTemperatura;

    public TratamentAntitermic(double pragTemperatura = 38.5)
    {
        this.pragTemperatura = pragTemperatura;
    }

    public void AplicaTratament(Pacient pacient)
    {
        if (pacient.Temperatura >= pragTemperatura)
        {
            Console.WriteLine($"Se administreaza tratament antitermic pentru {pacient.Nume}");
            Console.WriteLine($"Temperatura actuala: {pacient.Temperatura}°C");
            Console.WriteLine("Se recomanda si masuri fizice de racire");
        }
        else
        {
            Console.WriteLine($"Temperatura {pacient.Temperatura}°C nu necesita interventie medicamentoasa");
        }
    }

    public string GetDescriere()
    {
        return $"Tratament antitermic pentru temperatura peste {pragTemperatura}°C";
    }
}

public class TratamentAntihipertensiv : IStrategieTratament
{
    private int pragTensiune;

    public TratamentAntihipertensiv(int pragTensiune = 140)
    {
        this.pragTensiune = pragTensiune;
    }

    public void AplicaTratament(Pacient pacient)
    {
        if (pacient.TensiuneSistolica >= pragTensiune)
        {
            Console.WriteLine($"Se administreaza tratament antihipertensiv pentru {pacient.Nume}");
            Console.WriteLine($"Tensiunea actuala: {pacient.TensiuneSistolica}");
            Console.WriteLine("Se recomanda monitorizare la 30 minute");
        }
        else
        {
            Console.WriteLine($"Tensiunea {pacient.TensiuneSistolica} nu necesita interventie medicamentoasa");
        }
    }

    public string GetDescriere()
    {
        return $"Tratament antihipertensiv pentru tensiune peste {pragTensiune}";
    }
}

// Context pentru aplicarea strategiei
public class ContextTratament
{
    private IStrategieTratament strategieTratament;

    public void SetStrategie(IStrategieTratament strategie)
    {
        strategieTratament = strategie;
        Console.WriteLine($"S-a selectat: {strategie.GetDescriere()}");
    }

    public void AplicaTratamentPacient(Pacient pacient)
    {
        if (strategieTratament == null)
        {
            Console.WriteLine("Nu s-a selectat nicio strategie de tratament!");
            return;
        }

        Console.WriteLine($"\nAplicare tratament pentru {pacient}");
        strategieTratament.AplicaTratament(pacient);
    }
}

// Exemplu de utilizare
class Program
{
    static void Main()
    {
        // Creare pacient
        var pacient = new Pacient("Ion Popescu", 45)
        {
            Temperatura = 39.2,
            Puls = 85,
            TensiuneSistolica = 150
        };
        pacient.Alergii.Add("Penicilina");
        pacient.AfectiuniExistente.Add("Hipertensiune");

        // Creare context
        var context = new ContextTratament();

        // Demonstrare diferite strategii
        Console.WriteLine("=== Aplicare strategii de tratament ===\n");

        // Strategie antitermica
        context.SetStrategie(new TratamentAntitermic(38.5));
        context.AplicaTratamentPacient(pacient);

        Console.WriteLine("\n---\n");

        // Strategie antibiotica
        context.SetStrategie(new TratamentAntibiotics("Amoxicilina", 7));
        context.AplicaTratamentPacient(pacient);

        Console.WriteLine("\n---\n");

        // Strategie antihipertensiva
        context.SetStrategie(new TratamentAntihipertensiv(140));
        context.AplicaTratamentPacient(pacient);
    }
}