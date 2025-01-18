//Sablonul de proiectare Bridge este folosit pentru a separa o abstractie de implementarea sa, astfel incat ambele sa poata varia independent. Acesta este util atunci cand dorim sa cream un sistem flexibil, evitand o crestere combinatorie a claselor. In contextul medicamentelor, putem modela un sistem care sa separe tipurile de medicamente de modul in care acestea sunt distribuite.

// Aceasta interfata defineste contractul pentru metodele de administrare a medicamentelor.
// Metoda Administer primeste un parametru medicineName si specifica modul de administrare.
public interface IAdministrationMethod
{
    void Administer(string medicineName);
}

// Clase concrete pentru diferite metode de administrare
public class OralAdministration : IAdministrationMethod
{
    public void Administer(string medicineName)
    {
        Console.WriteLine($"{medicineName} se administreaza pe cale orala.");
    }
}

public class CutaneousAdministration : IAdministrationMethod
{
    public void Administer(string medicineName)
    {
        Console.WriteLine($"{medicineName} se administreaza pe piele (cutanat).");
    }
}

// Aceasta clasa abstracta foloseste o implementare a interfetei IAdministrationMethod pentru a separa logica specifica de metoda de administrare. Are un constructor care primeste metoda de administrare si o metoda abstracta Administer.
public abstract class Medicine
{
    protected IAdministrationMethod administrationMethod;

    protected Medicine(IAdministrationMethod method)
    {
        administrationMethod = method;
    }

    public abstract void Administer();
}

// Fiecare clasa extinde Medicine si implementeaza logica specifica tipului de medicament.
public class Pill : Medicine
{
    public Pill(IAdministrationMethod method) : base(method) { }

    public override void Administer()
    {
        Console.WriteLine("Medicamentul este sub forma de pastila.");
        administrationMethod.Administer("Pastila");
    }
}

public class Syrup : Medicine
{
    public Syrup(IAdministrationMethod method) : base(method) { }

    public override void Administer()
    {
        Console.WriteLine("Medicamentul este sub forma de sirop.");
        administrationMethod.Administer("Sirop");
    }
}

public class Ointment : Medicine
{
    public Ointment(IAdministrationMethod method) : base(method) { }

    public override void Administer()
    {
        Console.WriteLine("Medicamentul este sub forma de unguent.");
        administrationMethod.Administer("Unguent");
    }
}

// Utilizarea sablonului Bridge
public class Program
{
    public static void Main(string[] args)
    {
        // Metoda de administrare orala pentru pastila
        Medicine pill = new Pill(new OralAdministration());
        pill.Administer();

        // Metoda de administrare orala pentru sirop
        Medicine syrup = new Syrup(new OralAdministration());
        syrup.Administer();

        // Metoda de administrare cutanata pentru unguent
        Medicine ointment = new Ointment(new CutaneousAdministration());
        ointment.Administer();
    }
}


//Avantaje ale implementarii
//Separarea abstractizarii de implementare: Permite extinderea independenta a medicamentelor si metodelor de administrare.
//Flexibilitate: Noi tipuri de medicamente sau metode de administrare pot fi adaugate usor.
//Reducerea combinarii claselor: Fara sablonul Bridge, fiecare combinatie de medicament si metoda ar necesita o clasa separata.