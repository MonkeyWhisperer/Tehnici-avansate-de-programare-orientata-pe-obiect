using System;
using System.ComponentModel;

//Este baza pentru toate tipurile de medicamente.
//Contine metoda abstracta Descriere, care trebuie implementata de toate clasele derivate.
//Rolul clasei este de a oferi o interfata comuna pentru toate tipurile de medicamente.
abstract class Medicament
{
    // Metoda abstracta pentru afisarea descrierii medicamentului
    public abstract void Descriere();
}

// Clase concrete pentru diferite tipuri de Medicamente
// Acestea implementeaza metoda Descriere pentru a afisa informatii specifice despre fiecare medicament.
class Paracetamol : Medicament
{
    // Implementarea metodei pentru descrierea Paracetamolului
    public override void Descriere()
    {
        Console.WriteLine("Acesta este un Paracetamol. Este utilizat pentru reducerea febrei si calmarea durerii.");
    }
}

class Ibuprofen : Medicament
{
    // Implementarea metodei pentru descrierea Ibuprofenului
    public override void Descriere()
    {
        Console.WriteLine("Acesta este un Ibuprofen. Este utilizat pentru reducerea inflamatiei si calmarea durerii.");
    }
}

class Antibiotic : Medicament
{
    // Implementarea metodei pentru descrierea Antibioticului
    public override void Descriere()
    {
        Console.WriteLine("Acesta este un Antibiotic. Este utilizat pentru combaterea infectiilor bacteriene.");
    }
}

// Clasa abstracta pentru fabrica de Medicamente
abstract class MedicamentFactory
{
    // Metoda abstracta pentru crearea unui medicament
    public abstract Medicament CreareMedicament();
}

// Clase concrete de fabrici pentru fiecare tip de Medicament
class ParacetamolFactory : MedicamentFactory
{
    // Crearea unui obiect de tip Paracetamol
    public override Medicament CreareMedicament()
    {
        return new Paracetamol();
    }
}

class IbuprofenFactory : MedicamentFactory
{
    // Crearea unui obiect de tip Ibuprofen
    public override Medicament CreareMedicament()
    {
        return new Ibuprofen();
    }
}

class AntibioticFactory : MedicamentFactory
{
    // Crearea unui obiect de tip Antibiotic
    public override Medicament CreareMedicament()
    {
        return new Antibiotic();
    }
}

// Programul principal
class Program
{
    static void Main(string[] args)
    {
        // Crearea fabricilor
        MedicamentFactory paracetamolFactory = new ParacetamolFactory();
        MedicamentFactory ibuprofenFactory = new IbuprofenFactory();
        MedicamentFactory antibioticFactory = new AntibioticFactory();

        // Crearea medicamentelor utilizand fabricile
        Medicament paracetamol = paracetamolFactory.CreareMedicament();
        Medicament ibuprofen = ibuprofenFactory.CreareMedicament();
        Medicament antibiotic = antibioticFactory.CreareMedicament();

        // Afisarea descrierilor medicamentelor
        paracetamol.Descriere();
        ibuprofen.Descriere();
        antibiotic.Descriere();
    }
}

//Avatanje
//Extensibilitate - Adaugarea unui nou tip de medicament necesita doar crearea unei noi clase de medicament si a unei fabrici corespunzatoare. Nu este necesar sa se modifice codul existent.
//Separarea responsabilitatilor - Logica de creare a obiectelor este separata de utilizarea lor.
//Claritate si reutilizare - Fabricile sunt reutilizabile pentru crearea mai multor instante ale aceluiasi tip de medicament.