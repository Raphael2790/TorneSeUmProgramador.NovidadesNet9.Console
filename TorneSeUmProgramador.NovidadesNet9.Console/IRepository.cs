namespace TorneSeUmProgramador.NovidadesNet9.Console;

public interface IRepository<T>
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    T GetById(int id);
    IEnumerable<T> GetAll();
}

public interface IAlunoRepository : IRepository<Aluno>
{
    string BuscarNomeAlunoPorIndentificador(int identificador);
}

public class AlunoRepository : IAlunoRepository, IDisposable
{
    public void Add(Aluno entity)
    {
        throw new NotImplementedException();
    }

    public string BuscarNomeAlunoPorIndentificador(int identificador)
    {
        return $"Aluno {identificador}";
    }

    public void Delete(Aluno entity)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Aluno> GetAll()
    {
        throw new NotImplementedException();
    }

    public Aluno GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Aluno entity)
    {
        throw new NotImplementedException();
    }
}

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; }
}
