public class RegistroDAO
{
    private readonly MeuDiarioSENACContext conexao = new();

    public Usuario ObterOuCriarUsuario(string nome)
    {
        var nomeLimpo = nome.Trim();

        foreach (var usuario in conexao.Usuarios)
        {
            if (usuario.Nome == nomeLimpo)
                return usuario;
        }

        var novoUsuario = new Usuario
        {
            Nome = nomeLimpo,
            Registros = new List<Registro>()
        };

        conexao.Usuarios.Add(novoUsuario);
        conexao.SaveChanges();

        return novoUsuario;
    }

    public void Inserir(Registro registro)
    {
        conexao.Registros.Add(registro);
        conexao.SaveChanges();
    }

    public List<Registro> ListarTodos()
    {
        return conexao.Registros.ToList();
    }

    public List<Registro> ListarTodosPorUsuario(int usuarioId)
    {
        var registros = new List<Registro>();

        foreach (var registro in conexao.Registros)
        {
            if (registro.UsuarioId == usuarioId)
                registros.Add(registro);
        }

        return registros;
    }

    public Registro? BuscarPorId(int id)
    {
        foreach (var registro in conexao.Registros)
        {
            if (registro.Id == id)
                return registro;
        }

        return null;
    }

    public bool AtualizarPorId(int id, Registro registroAtualizado)
    {
        Registro? registro = null;

        foreach (var item in conexao.Registros)
        {
            if (item.Id == id)
            {
                registro = item;
                break;
            }
        }

        if (registro == null)
            return false;

        registro.Titulo = registroAtualizado.Titulo;
        registro.Data = registroAtualizado.Data;
        registro.Conteudo = registroAtualizado.Conteudo;
        registro.UsuarioId = registroAtualizado.UsuarioId;
        registro.Usuario = registroAtualizado.Usuario;

        conexao.SaveChanges();
        return true;
    }

    public bool DeletarPorId(int id)
    {
        Registro? registro = null;

        foreach (var item in conexao.Registros)
        {
            if (item.Id == id)
            {
                registro = item;
                break;
            }
        }

        if (registro == null)
            return false;

        conexao.Registros.Remove(registro);
        conexao.SaveChanges();
        return true;
    }
}
