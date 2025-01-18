using System;

// Clasa abstracta care defineste structura unui medicament oral
abstract class MedicamentOral
{
    public abstract void AfiseazaInfoMedicament(); // Metoda abstracta pentru afisarea informatiilor despre medicament
}

// Clasa abstracta care defineste structura unui medicament injectabil
abstract class MedicamentInjectabil
{
    public abstract void AfiseazaInfoMedicament(); // Metoda abstracta pentru afisarea informatiilor despre medicament
}

// Implementare concreta a unui antibiotic oral
class AntibioticOral : MedicamentOral
{
    public override void AfiseazaInfoMedicament()
    {
        Console.WriteLine("Antibiotic Oral: Utilizat pentru tratarea infectiilor bacteriene.");
    }
}

// Implementare concreta a unui analgezic oral
class AnalgezicOral : MedicamentOral
{
    public override void AfiseazaInfoMedicament()
    {
        Console.WriteLine("Analgezic Oral: Utilizat pentru ameliorarea durerilor.");
    }
}

// Implementare concreta a unui antiinflamator oral
class AntiinflamatorOral : MedicamentOral
{
    public override void AfiseazaInfoMedicament()
    {
        Console.WriteLine("Antiinflamator Oral: Utilizat pentru reducerea inflamatiilor.");
    }
}

// Implementare concreta a unui antibiotic injectabil
class AntibioticInjectabil : MedicamentInjectabil
{
    public override void AfiseazaInfoMedicament()
    {
        Console.WriteLine("Antibiotic Injectabil: Utilizat pentru infectii severe.");
    }
}

// Implementare concreta a unui analgezic injectabil
class AnalgezicInjectabil : MedicamentInjectabil
{
    public override void AfiseazaInfoMedicament()
    {
        Console.WriteLine("Analgezic Injectabil: Utilizat pentru dureri acute.");
    }
}

// Implementare concreta a unui antiinflamator injectabil
class AntiinflamatorInjectabil : MedicamentInjectabil
{
    public override void AfiseazaInfoMedicament()
    {
        Console.WriteLine("Antiinflamator Injectabil: Utilizat pentru inflamatii severe.");
    }
}

// Clasa abstracta pentru fabrica de medicamente (Abstract Factory)
abstract class FabricaMedicamente
{
    public abstract MedicamentOral CreeazaMedicamentOral(); // Metoda pentru crearea unui medicament oral
    public abstract MedicamentInjectabil CreeazaMedicamentInjectabil(); // Metoda pentru crearea unui medicament injectabil
}

// Fabrica concreta pentru producerea de antibiotice
class FabricaAntibiotice : FabricaMedicamente
{
    public override MedicamentOral CreeazaMedicamentOral()
    {
        return new AntibioticOral();
    }

    public override MedicamentInjectabil CreeazaMedicamentInjectabil()
    {
        return new AntibioticInjectabil();
    }
}

// Fabrica concreta pentru producerea de analgezice
class FabricaAnalgezice : FabricaMedicamente
{
    public override MedicamentOral CreeazaMedicamentOral()
    {
        return new AnalgezicOral();
    }

    public override MedicamentInjectabil CreeazaMedicamentInjectabil()
    {
        return new AnalgezicInjectabil();
    }
}

// Fabrica concreta pentru producerea de antiinflamatoare
class FabricaAntiinflamatoare : FabricaMedicamente
{
    public override MedicamentOral CreeazaMedicamentOral()
    {
        return new AntiinflamatorOral();
    }

    public override MedicamentInjectabil CreeazaMedicamentInjectabil()
    {
        return new AntiinflamatorInjectabil();
    }
}

// Clasa care gestioneaza prezentarea medicamentelor utilizand o fabrica specifica
class SistemGestionareMedicamente
{
    private readonly FabricaMedicamente fabrica;

    // Constructor care primeste o fabrica de medicamente
    public SistemGestionareMedicamente(FabricaMedicamente fabrica)
    {
        this.fabrica = fabrica;
    }

    // Metoda care prezinta medicamentele create de fabrica
    public void PrezintaMedicamente()
    {
        var medicamentOral = fabrica.CreeazaMedicamentOral();
        var medicamentInjectabil = fabrica.CreeazaMedicamentInjectabil();

        medicamentOral.AfiseazaInfoMedicament(); // Afiseaza detalii despre medicamentul oral
        medicamentInjectabil.AfiseazaInfoMedicament(); // Afiseaza detalii despre medicamentul injectabil
    }
}

class Program
{
    static void Main()
    {
        // Sistem pentru gestionarea antibioticelor
        Console.WriteLine("Medicamente antibiotice:");
        var sistemAntibiotice = new SistemGestionareMedicamente(new FabricaAntibiotice());
        sistemAntibiotice.PrezintaMedicamente();

        // Sistem pentru gestionarea analgezicelor
        Console.WriteLine("\nMedicamente analgezice:");
        var sistemAnalgezice = new SistemGestionareMedicamente(new FabricaAnalgezice());
        sistemAnalgezice.PrezintaMedicamente();

        // Sistem pentru gestionarea antiinflamatoarelor
        Console.WriteLine("\nMedicamente antiinflamatoare:");
        var sistemAntiinflamatoare = new SistemGestionareMedicamente(new FabricaAntiinflamatoare());
        sistemAntiinflamatoare.PrezintaMedicamente();
    }
}
