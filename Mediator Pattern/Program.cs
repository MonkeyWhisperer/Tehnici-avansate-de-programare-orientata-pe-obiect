// Mediatorul centralizează logica de gestionare a voturilor, iar alegătorii comunică doar cu mediatorul pentru a exprima voturile lor și pentru a primi rezultatele.

// Interfata Mediator
// Defineste contractul pentru mediator, incluzand metode pentru inregistrarea alegatorilor, colectarea voturilor si anuntarea rezultatelor.
public interface IVotingMediator
{
    void RegisterVoter(Voter voter);
    void SubmitVote(string voterName, string candidate);
    void AnnounceResults();
}

// Mediator Concret
// Este implementarea concreta a mediatorului, gestionand lista alegatorilor si voturile lor.
public class VotingMediator : IVotingMediator
{
    private readonly Dictionary<string, string> _votes = new Dictionary<string, string>();
    private readonly List<Voter> _voters = new List<Voter>();

    public void RegisterVoter(Voter voter)
    {
        if (!_voters.Contains(voter))
        {
            _voters.Add(voter);
            voter.SetMediator(this);
        }
    }

    public void SubmitVote(string voterName, string candidate)
    {
        if (_votes.ContainsKey(voterName))
        {
            Console.WriteLine($"{voterName} a votat deja.");
        }
        else
        {
            _votes[voterName] = candidate;
            Console.WriteLine($"{voterName} a votat pentru {candidate}.");
        }
    }

    public void AnnounceResults()
    {
        Console.WriteLine("Rezultatele votării:");
        var results = new Dictionary<string, int>();

        foreach (var vote in _votes.Values)
        {
            if (results.ContainsKey(vote))
            {
                results[vote]++;
            }
            else
            {
                results[vote] = 1;
            }
        }

        foreach (var result in results)
        {
            Console.WriteLine($"{result.Key}: {result.Value} voturi");
        }
    }
}

// Colaborator
// Reprezinta alegatorii care comunica cu mediatorul pentru a trimite voturi.
public class Voter
{
    private IVotingMediator _mediator;
    public string Name { get; }

    public Voter(string name)
    {
        Name = name;
    }

    public void SetMediator(IVotingMediator mediator)
    {
        _mediator = mediator;
    }

    public void Vote(string candidate)
    {
        _mediator?.SubmitVote(Name, candidate);
    }
}


//Simuleaza procesul de votare prin crearea alegatorilor, inregistrarea lor in mediator si exprimarea voturilor.
public class Program
{
    public static void Main(string[] args)
    {
        IVotingMediator votingMediator = new VotingMediator();

        var voter1 = new Voter("Votantul Maria");
        var voter2 = new Voter("Votantul Ion");
        var voter3 = new Voter("Votantul Marcel");

        votingMediator.RegisterVoter(voter1);
        votingMediator.RegisterVoter(voter2);
        votingMediator.RegisterVoter(voter3);

        voter1.Vote("Candidatul A");
        voter2.Vote("Candidatul B");
        voter3.Vote("Candidatul A");

        voter1.Vote("Candidatul C"); // Incercare de a vota din nou

        votingMediator.AnnounceResults();
    }
}

//Functionalitati cheie:
//Alegatorii pot vota o singura data.
//Rezultatele sunt centralizate si afisate de mediator.
//Mediatorul previne comunicarea directa intre alegatori.
