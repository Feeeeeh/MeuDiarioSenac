var diario = new RegistroDAO();

Console.WriteLine("Digite seu nome:");
var nomeUsuario = Console.ReadLine();

while (string.IsNullOrWhiteSpace(nomeUsuario))
{
    Console.WriteLine("Nome inválido. Digite novamente:");
    nomeUsuario = Console.ReadLine();
}

var usuarioAtual = diario.ObterOuCriarUsuario(nomeUsuario);

while (true)
{
    Console.Clear();
    Console.WriteLine($"Usuário: {usuarioAtual.Nome}");
    Console.WriteLine("1 - Inserir registro");
    Console.WriteLine("2 - Listar meus registros");
    Console.WriteLine("3 - Listar todos os registros");
    Console.WriteLine("4 - Buscar registro por ID");
    Console.WriteLine("5 - Atualizar registro por ID");
    Console.WriteLine("6 - Excluir registro por ID");
    Console.WriteLine("7 - Sair");

    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.WriteLine("Título: ");
            var titulo = Console.ReadLine();

            Console.WriteLine("Data (yyyy-MM-dd): ");
            DateTime data;
            if (!DateTime.TryParse(Console.ReadLine(), out data))
            {
                Console.WriteLine("\nFormato de data inválido. Use yyyy-MM-dd.\n");
                break;
            }

            Console.WriteLine("Conteúdo: ");
            var conteudo = Console.ReadLine();

            diario.Inserir(new Registro
            {
                Titulo = titulo,
                Data = data,
                Conteudo = conteudo,
                UsuarioId = usuarioAtual.Id,
                Usuario = usuarioAtual
            });
            Console.WriteLine("\nRegistro inserido com sucesso!\n");
            break;

        case "2":
            var meusRegistros = diario.ListarTodosPorUsuario(usuarioAtual.Id);
            if (meusRegistros.Count == 0)
            {
                Console.WriteLine("\nNenhum registro encontrado.\n");
                break;
            }

            foreach (var r in meusRegistros)
            {
                Console.WriteLine($"ID: {r.Id,-4} | Título: {r.Titulo,-15} | Data: {r.Data:dd/MM/yyyy} | Conteúdo: {r.Conteudo}");
            }
            break;

        case "3":
            var todosRegistros = diario.ListarTodos();
            if (todosRegistros.Count == 0)
            {
                Console.WriteLine("\nNenhum registro encontrado.\n");
                break;
            }

            foreach (var r in todosRegistros)
            {
                Console.WriteLine($"ID: {r.Id,-4} | Título: {r.Titulo,-15} | Usuário: {r.UsuarioId} | Data: {r.Data:dd/MM/yyyy} | Conteúdo: {r.Conteudo}");
            }
            break;

        case "4":
            Console.WriteLine("Digite o ID do registro: ");
            if (!int.TryParse(Console.ReadLine(), out var idBusca))
            {
                Console.WriteLine("\nID inválido.\n");
                break;
            }

            var registro = diario.BuscarPorId(idBusca);
            if (registro == null)
            {
                Console.WriteLine("\nRegistro não encontrado.\n");
                break;
            }

            Console.WriteLine($"ID: {registro.Id} | Título: {registro.Titulo} | Usuário: {registro.UsuarioId} | Data: {registro.Data:dd/MM/yyyy} | Conteúdo: {registro.Conteudo}");
            break;

        case "5":
            Console.WriteLine("Digite o ID do registro para atualizar: ");
            if (!int.TryParse(Console.ReadLine(), out var idAtualizar))
            {
                Console.WriteLine("\nID inválido.\n");
                break;
            }

            var registroAtual = diario.BuscarPorId(idAtualizar);
            if (registroAtual == null)
            {
                Console.WriteLine("\nRegistro não encontrado para atualização.\n");
                break;
            }

            Console.WriteLine("Novo título: ");
            var tituloAtualizado = Console.ReadLine();

            Console.WriteLine("Nova data (yyyy-MM-dd): ");
            DateTime dataAtualizada;
            if (!DateTime.TryParse(Console.ReadLine(), out dataAtualizada))
            {
                Console.WriteLine("\nFormato de data inválido. Use yyyy-MM-dd.\n");
                break;
            }

            Console.WriteLine("Novo conteúdo: ");
            var conteudoAtualizado = Console.ReadLine();

            var registroCompleto = new Registro
            {
                Titulo = tituloAtualizado,
                Data = dataAtualizada,
                Conteudo = conteudoAtualizado,
                UsuarioId = registroAtual.UsuarioId,
                Usuario = registroAtual.Usuario
            };

            if (diario.AtualizarPorId(idAtualizar, registroCompleto))
            {
                Console.WriteLine("\nRegistro atualizado com sucesso!\n");
            }
            else
            {
                Console.WriteLine("\nNão foi possível atualizar o registro.\n");
            }
            break;

        case "6":
            Console.WriteLine("Digite o ID do registro para excluir: ");
            if (!int.TryParse(Console.ReadLine(), out var idExcluir))
            {
                Console.WriteLine("\nID inválido.\n");
                break;
            }

            if (diario.DeletarPorId(idExcluir))
            {
                Console.WriteLine("\nRegistro excluído com sucesso!\n");
            }
            else
            {
                Console.WriteLine("\nRegistro não encontrado para exclusão.\n");
            }
            break;

        case "7":
            Console.Clear();
            return;

        default:
            Console.WriteLine("\nOpção inválida.\n");
            break;
    }

    Console.WriteLine("\nPressione enter para continuar...");
    Console.ReadLine();
    Console.Clear();
}
