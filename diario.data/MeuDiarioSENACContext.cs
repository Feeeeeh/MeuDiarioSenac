using MySql.Data.MySqlClient;

public class MeuDiarioSENACContext
{
    public static MySqlConnection ObterConexao()
    {
        return new MySqlConnection("server=localhost;database=diario_senac;user=root;password=1234;");
    }
}