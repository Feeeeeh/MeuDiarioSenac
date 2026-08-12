using MySql.Data.MySqlClient;

public class Diario
{
    public void Inserir(Registro r)
    {
        using var c = MeuDiarioSENACContext.ObterConexao();
        c.Open();

        using var cmd = new MySqlCommand("INSERT INTO registros (titulo, data_registro, conteudo) VALUES (@titulo, @data, @conteudo)", c);
        cmd.Parameters.AddWithValue("@titulo", r.Titulo);
        cmd.Parameters.AddWithValue("@data", r.Data);
        cmd.Parameters.AddWithValue("@conteudo", r.Conteudo);

        cmd.ExecuteNonQuery();
    }

    public List<Registro> ListarTodos()
    {
        var lista = new List<Registro>();

        using var c = MeuDiarioSENACContext.ObterConexao();
        c.Open();

        using var cmd = new MySqlCommand("SELECT * FROM registros", c);
        using var r = cmd.ExecuteReader();

        while (r.Read())
        {
            lista.Add(new Registro
            {
                Id = r.GetInt32("id"),
                Titulo = r.GetString("titulo"),
                Data = r.GetDateTime("data_registro"),
                Conteudo = r.GetString("conteudo")
            }
            );
        }

        return lista;
    }

    public Registro? BuscarPorId(int id)
    {
        using var c = MeuDiarioSENACContext.ObterConexao();
        c.Open();

        using var cmd = new MySqlCommand("SELECT * FROM registros WHERE id = @id", c);
        cmd.Parameters.AddWithValue("@id", id);
        using var r = cmd.ExecuteReader();

        if (!r.Read())
        {
            return null;
        }

        return new Registro
        {
            Id = r.GetInt32("id"),
            Titulo = r.GetString("titulo"),
            Data = r.GetDateTime("data_registro"),
            Conteudo = r.GetString("conteudo")
        };
    }
}
