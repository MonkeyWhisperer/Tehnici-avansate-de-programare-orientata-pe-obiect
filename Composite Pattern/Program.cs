using System;
using System.Collections.Generic;

// Interfata de baza pentru toate componentele
public interface IComponentaMedicament
{
    void Afiseaza();
    double CalculeazaPret();
    string ObtineNume();
}

// Clasa de baza pentru medicamente individuale
public class Medicament : IComponentaMedicament
{
    private string nume;
    private double pret;
    private string substancaActiva;

    public Medicament(string nume, double pret, string substancaActiva)
    {
        this.nume = nume;
        this.pret = pret;
        this.substancaActiva = substancaActiva;
    }

    public void Afiseaza()
    {
        Console.WriteLine($"Medicament: {nume} - Substanta activa: {substancaActiva} - Pret: {pret} RON");
    }

    public double CalculeazaPret()
    {
        return pret;
    }

    public string ObtineNume()
    {
        return nume;
    }
}

// Clasa pentru pachete de medicamente
public class PachetMedicamente : IComponentaMedicament
{
    private string numePachet;
    private List<IComponentaMedicament> medicamente = new List<IComponentaMedicament>();

    public PachetMedicamente(string numePachet)
    {
        this.numePachet = numePachet;
    }

    public void AdaugaMedicament(IComponentaMedicament medicament)
    {
        medicamente.Add(medicament);
    }

    public void StergeMedicament(IComponentaMedicament medicament)
    {
        medicamente.Remove(medicament);
    }

    public void Afiseaza()
    {
        Console.WriteLine($"\nPachet de medicamente: {numePachet}");
        Console.WriteLine("Continut pachet:");

        foreach (var medicament in medicamente)
        {
            medicament.Afiseaza();
        }

        Console.WriteLine($"Pret total pachet: {CalculeazaPret()} RON\n");
    }

    public double CalculeazaPret()
    {
        double pretTotal = 0;
        foreach (var medicament in medicamente)
        {
            pretTotal += medicament.CalculeazaPret();
        }
        return pretTotal;
    }

    public string ObtineNume()
    {
        return numePachet;
    }
}

// Exemplu de utilizare
public class Program
{
    public static void Main()
    {
        // Creare medicamente individuale
        var paracetamol = new Medicament("Paracetamol", 15.50, "Acetaminofen");
        var ibuprofen = new Medicament("Ibuprofen", 20.75, "Ibuprofen");
        var aspirin = new Medicament("Aspirin", 12.30, "Acid acetilsalicilic");

        // Creare pachet pentru raceala
        var pachetRaceala = new PachetMedicamente("Pachet Raceala");
        pachetRaceala.AdaugaMedicament(paracetamol);
        pachetRaceala.AdaugaMedicament(ibuprofen);

        // Creare pachet pentru durere
        var pachetDurere = new PachetMedicamente("Pachet Durere");
        pachetDurere.AdaugaMedicament(ibuprofen);
        pachetDurere.AdaugaMedicament(aspirin);

        // Creare pachet complet
        var pachetComplet = new PachetMedicamente("Pachet Complet");
        pachetComplet.AdaugaMedicament(pachetRaceala);
        pachetComplet.AdaugaMedicament(pachetDurere);

        // Afisare rezultate
        pachetComplet.Afiseaza();
    }
}