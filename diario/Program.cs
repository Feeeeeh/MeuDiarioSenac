var diario = new Diario();

void CriarRegistro(Diario diario)
{
    Console.WriteLine("Titulo: ");
    string titulo = Console.ReadLine();

    DateTime data;
    try
    {
        Console.WriteLine("Data (yyyy-MM-dd): ");
        data = DateTime.Parse(Console.ReadLine());
    }
    catch (FormatException)
    {
        Console.WriteLine("\nFormato de data inválido. Por favor, use o formato yyyy-MM-dd, com mascara.\n");
        return;
    }

    Console.WriteLine("Conteudo: ");
    string conteudo = Console.ReadLine();

    diario.Inserir(new Registro
    {
        Titulo = titulo,
        Data = data,
        Conteudo = conteudo
    });
    Console.WriteLine("\nRegistro inserido com sucesso!\n");
}

void ListarRegistros(Diario diario)
{
    var registros = diario.ListarTodos();

    if (registros.Count == 0)
    {
        Console.WriteLine("\nNenhum registro encontrado.\n");
        return;
    }

    foreach (var r in registros)
    {
        Console.WriteLine($"ID: {r.Id}, Titulo: {r.Titulo}, Data: {r.Data}, Conteudo: {r.Conteudo}");
    }
}


void BuscarRegistro(Diario diario)
{
    Console.WriteLine("Digite o ID do registro: ");
    int id = int.Parse(Console.ReadLine());

    var r = diario.BuscarPorId(id);

    foreach (var registro in diario.ListarTodos())
    {
        if (registro.Id == id)
        {
            Console.WriteLine($"ID: {registro.Id}, Titulo: {registro.Titulo}, Data: {registro.Data}, Conteudo: {registro.Conteudo}");
            return;
        }
    }
    Console.WriteLine("\nRegistro não encontrado.\n");
}

while (true)
{
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Inserir registro");
    Console.WriteLine("2 - Listar registros");
    Console.WriteLine("3 - Buscar registro por ID");
    Console.WriteLine("4 - Sair");

    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            CriarRegistro(diario);
            Continue();
            break;
        case "2":
            ListarRegistros(diario);
            Continue();
            break;
        case "3":
            BuscarRegistro(diario);
            Continue();
            break;
        case "4":
            return;
        default:
            Console.WriteLine("\nOpção inválida.\n");
            break;
    }
}

void Continue()
{
    Console.WriteLine("\nPressione enter para continuar...");
    Console.ReadLine();
    Console.Clear();
}