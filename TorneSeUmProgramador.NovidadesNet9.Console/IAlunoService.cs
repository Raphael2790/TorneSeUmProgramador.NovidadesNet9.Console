namespace TorneSeUmProgramador.NovidadesNet9.Console;

public interface IAlunoService
{
    void Add(Aluno entity);
    void Update(Aluno entity);
    void Delete(Aluno entity);
    Aluno GetById(int id);
    IEnumerable<Aluno> GetAll();
    string BuscarNomeAlunoPorIndentificador(int identificador);
}
