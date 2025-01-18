using System;
using System.Collections.Generic;

// Interfata pentru Observer
public interface IObservatorMedical
{
    void Actualizeaza(string numePacient, string parametruVital, double valoare);
}

// Interfata pentru Subiect
public interface ISubiectMedical
{
    void AdaugaObservator(IObservatorMedical observator);
    void StergeObservator(IObservatorMedical observator);
    void NotificaObservatori();
}

// Clasa concreta pentru monitorizarea parametrilor vitali
public class MonitorizareParametriVitali : ISubiectMedical
{
    private List<IObservatorMedical> observatori;
    private string numePacient;
    private Dictionary<string, double> parametriVitali;

    public MonitorizareParametriVitali(string numePacient)
    {
        this.numePacient = numePacient;
        observatori = new List<IObservatorMedical>();
        parametriVitali = new Dictionary<string, double>();
    }

    public void AdaugaObservator(IObservatorMedical observator)
    {
        observatori.Add(observator);
    }

    public void StergeObservator(IObservatorMedical observator)
    {
        observatori.Remove(observator);
    }

    public void NotificaObservatori()
    {
        foreach (var parametru in parametriVitali)
        {
            foreach (var observator in observatori)
            {
                observator.Actualizeaza(numePacient, parametru.Key, parametru.Value);
            }
        }
    }

    public void ActualizeazaParametru(string parametru, double valoare)
    {
        parametriVitali[parametru] = valoare;
        NotificaObservatori();
    }
}

// Clasa concreta pentru medic
public class Medic : IObservatorMedical
{
    private string nume;
    private string specializare;

    public Medic(string nume, string specializare)
    {
        this.nume = nume;
        this.specializare = specializare;
    }

    public void Actualizeaza(string numePacient, string parametruVital, double valoare)
    {
        Console.WriteLine($"Medicul {nume} ({specializare}) a primit notificare:");
        Console.WriteLine($"Pacientul {numePacient} - {parametruVital}: {valoare}");
        VerificaValoriCritice(parametruVital, valoare);
    }

    private void VerificaValoriCritice(string parametruVital, double valoare)
    {
        switch (parametruVital.ToLower())
        {
            case "tensiune":
                if (valoare > 140)
                    Console.WriteLine("ATENTIE: Tensiune arteriala ridicata!");
                break;
            case "puls":
                if (valoare > 100)
                    Console.WriteLine("ATENTIE: Puls ridicat!");
                break;
            case "temperatura":
                if (valoare > 38)
                    Console.WriteLine("ATENTIE: Febra!");
                break;
        }
    }
}

// Clasa concreta pentru asistent medical
public class AsistentMedical : IObservatorMedical
{
    private string nume;
    private string sectie;

    public AsistentMedical(string nume, string sectie)
    {
        this.nume = nume;
        this.sectie = sectie;
    }

    public void Actualizeaza(string numePacient, string parametruVital, double valoare)
    {
        Console.WriteLine($"Asistentul medical {nume} din sectia {sectie} a primit notificare:");
        Console.WriteLine($"Pacientul {numePacient} - {parametruVital}: {valoare}");
    }
}

// Exemplu de utilizare
public class Program
{
    public static void Main()
    {
        // Initializare monitorizare pentru un pacient
        var monitorizare = new MonitorizareParametriVitali("Ion Popescu");

        // Creare observatori
        var medicCardiolog = new Medic("Dr. Maria Ionescu", "Cardiologie");
        var medicInternist = new Medic("Dr. Andrei Popa", "Medicina Interna");
        var asistent = new AsistentMedical("Elena Dumitrescu", "Cardiologie");

        // Adaugare observatori
        monitorizare.AdaugaObservator(medicCardiolog);
        monitorizare.AdaugaObservator(medicInternist);
        monitorizare.AdaugaObservator(asistent);

        // Simulare actualizari parametri vitali
        Console.WriteLine("=== Actualizare parametri vitali ===");
        monitorizare.ActualizeazaParametru("tensiune", 145);
        Console.WriteLine();
        monitorizare.ActualizeazaParametru("puls", 90);
        Console.WriteLine();
        monitorizare.ActualizeazaParametru("temperatura", 38.5);
    }
}