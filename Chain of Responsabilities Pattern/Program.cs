using System;

// Chain of Responsibility este un sablon de proiectare comportamental care permite trimiterea unui request de la un obiect la altul de-a lungul unui lant de handleri, pana cand cererea este procesata. 
// Intr-un sistem medical, acest model poate fi utilizat pentru a gestiona fluxul de aprobare sau procesare a unor actiuni, cum ar fi aprobarea tratamentelor, procesarea platilor sau managementul cerintelor pacientilor.

// Sa presupunem ca dorim sa implementam un sistem in care un pacient poate solicita un tratament, iar cererea trece prin mai multe niveluri de aprobare:
// Asistentul medical verifica daca pacientul are reteta necesara.
// Doctorul aproba tratamentul.
// Managerul spitalului aproba costurile tratamentului daca este necesar.

// Interfata de baza pentru handleri
// Approver este o clasa abstracta care defineste metoda HandleRequest si lantul urmator prin SetNext
public abstract class Aprobare
{
    protected Aprobare _urmatorulHandler;

    public void SetNext(Aprobare urmatorulHandler)
    {
        _urmatorulHandler = urmatorulHandler;
    }

    public abstract void ProceseazaCererea(string cerere);
}

// Handler concret: Asistentul medical
public class Asistent : Aprobare
{
    public override void ProceseazaCererea(string cerere)
    {
        if (cerere == "Verifica Reteta")
        {
            Console.WriteLine("Asistentul medical a verificat reteta.");
        }
        else if (_urmatorulHandler != null)
        {
            _urmatorulHandler.ProceseazaCererea(cerere);
        }
    }
}

// Handler concret: Doctorul
public class Doctor : Aprobare
{
    public override void ProceseazaCererea(string cerere)
    {
        if (cerere == "Aproba Tratamentul")
        {
            Console.WriteLine("Doctorul a aprobat tratamentul.");
        }
        else if (_urmatorulHandler != null)
        {
            _urmatorulHandler.ProceseazaCererea(cerere);
        }
    }
}

// Handler concret: Managerul spitalului
public class ManagerSpital : Aprobare
{
    public override void ProceseazaCererea(string cerere)
    {
        if (cerere == "Aproba Costurile")
        {
            Console.WriteLine("Managerul spitalului a aprobat costurile.");
        }
        else
        {
            Console.WriteLine("Cererea nu a fost procesata.");
        }
    }
}

// Testarea modelului
class Program
{
    static void Main(string[] args)
    {
        // Crearea handlerilor
        var asistent = new Asistent();
        var doctor = new Doctor();
        var managerSpital = new ManagerSpital();

        // Configurarea lantului
        asistent.SetNext(doctor);
        doctor.SetNext(managerSpital);

        // Trimiterea cererilor
        Console.WriteLine("Cerere: Verifica Reteta");
        asistent.ProceseazaCererea("Verifica Reteta");

        Console.WriteLine("\nCerere: Aproba Tratamentul");
        asistent.ProceseazaCererea("Aproba Tratamentul");

        Console.WriteLine("\nCerere: Aproba Costurile");
        asistent.ProceseazaCererea("Aproba Costurile");

        Console.WriteLine("\nCerere: Cerere Necunoscuta");
        asistent.ProceseazaCererea("Cerere Necunoscuta");
    }
}

// Avantaje
// Separarea responsabilitatilor intre handleri.
// Usor de extins prin adaugarea de noi handleri.
// Cresterea flexibilitatii sistemului, permitand modificari fara a afecta celelalte componente.
