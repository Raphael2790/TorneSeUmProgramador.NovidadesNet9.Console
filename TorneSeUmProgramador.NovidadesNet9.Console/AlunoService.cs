
namespace TorneSeUmProgramador.NovidadesNet9.Console;

public class AlunoService(IAlunoRepository alunoRepository) : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository = alunoRepository;

    public void Add(Aluno entity)
    {
        throw new NotImplementedException();
    }

    public string BuscarNomeAlunoPorIndentificador(int identificador)
    {
        throw new NotImplementedException();
    }

    public void Delete(Aluno entity)
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
