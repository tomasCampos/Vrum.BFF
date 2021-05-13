namespace Repositorio.Constantes
{
    public class AppConstants
    {
        #region INFRAESTRUTURA

        public const string CONNECTION_STRING = "server=us-cdbr-east-03.cleardb.com;user=bf9c7fe9712622;password=e459392c;database=heroku_3dc1bcc8f5cdd12;SSL Mode=None";

        public const string CHAVE_CIFRA = "1e64fdce-e561-4f3d-bb78-0d7c8c86d14b";

        #endregion

        #region USUARIO_SQL

        public const string SQL_OBTER_USUARIO_POR_EMAIL =   @"SELECT 
	                                                        u.id_usuario AS Codigo,
                                                            u.email_usuario AS Email,
                                                            u.senha_usuario AS Senha,
                                                            u.nome_usuario AS Nome,
                                                            u.cpf_usuario AS Cpf,
                                                            u.telefone_usuario AS NumeroTelefone,
                                                            u.perfil_usuario AS Perfil,
                                                            e.id_endereco AS CodigoEndereco,
                                                            e.chave_endereco AS ChaveIdentificacaoEndereco,
                                                            e.cep_endereco AS CepEndereco,
                                                            e.logradouro_endereco AS LogradouroEndereco,
                                                            e.numero_endereco AS NumeroEndereco,
                                                            e.complemento_endereco AS ComplementoEndereco,
                                                            e.bairro_endereco AS BairroEndereco,
                                                            e.uf_endereco AS UfEndereco
                                                        FROM usuario u
                                                        INNER JOIN endereco e ON u.id_endereco = e.id_endereco
                                                        WHERE email_usuario = @email_usuario";

        public const string SQL_OBTER_USUARIO_POR_CODIGO = @"SELECT 
	                                                        u.id_usuario AS Codigo,
                                                            u.email_usuario AS Email,
                                                            u.senha_usuario AS Senha,
                                                            u.nome_usuario AS Nome,
                                                            u.cpf_usuario AS Cpf,
                                                            u.telefone_usuario AS NumeroTelefone,
                                                            u.perfil_usuario AS Perfil,
                                                            e.id_endereco AS CodigoEndereco,
                                                            e.chave_endereco AS ChaveIdentificacaoEndereco,
                                                            e.cep_endereco AS CepEndereco,
                                                            e.localidade_endereco AS LocalidadeEndereco,
                                                            e.logradouro_endereco AS LogradouroEndereco,
                                                            e.numero_endereco AS NumeroEndereco,
                                                            e.complemento_endereco AS ComplementoEndereco,
                                                            e.bairro_endereco AS BairroEndereco,
                                                            e.uf_endereco AS UfEndereco
                                                        FROM usuario u
                                                        INNER JOIN endereco e ON u.id_endereco = e.id_endereco
                                                        WHERE id_usuario = @id_usuario";

        public const string SQL_CADASTRAR_USUARIO = @"INSERT INTO `heroku_3dc1bcc8f5cdd12`.`usuario`
                                                    (`email_usuario`,
                                                    `senha_usuario`,
                                                    `nome_usuario`,
                                                    `cpf_usuario`,
                                                    `telefone_usuario`,
                                                    `perfil_usuario`,
                                                     `id_endereco`)
                                                    VALUES
                                                    (@email_usuario,
                                                    @senha_usuario,
                                                    @nome_usuario,
                                                    @cpf_usuario,
                                                    @telefone_usuario,
                                                    @perfil_usuario,
                                                    @id_endereco);";

        public const string SQL_ATUALIZAR_USUARIO = @"UPDATE 
	                                                    usuario
                                                    INNER JOIN endereco ON
	                                                    usuario.id_endereco = endereco.id_endereco
                                                    SET
                                                        usuario.email_usuario = @email_usuario,
                                                        usuario.senha_usuario = @senha_usuario,
                                                        usuario.nome_usuario = @nome_usuario,
                                                        usuario.cpf_usuario = @cpf_usuario,
                                                        usuario.telefone_usuario = @telefone_usuario,
                                                        endereco.bairro_endereco = @bairro_endereco,
                                                        endereco.cep_endereco = @cep_endereco,
                                                        endereco.complemento_endereco = @complemento_endereco,
                                                        endereco.logradouro_endereco = @logradouro_endereco,
                                                        endereco.numero_endereco = @numero_endereco,
                                                        endereco.uf_endereco = @uf_endereco,
                                                        endereco.localidade_endereco = @localidade_endereco
                                                  WHERE usuario.id_usuario = @id_usuario";

        #endregion

        #region ENDERECO_SQL

        public const string SQL_CADASTRAR_ENDERECO = @"INSERT INTO `heroku_3dc1bcc8f5cdd12`.`endereco`
                                                    (`chave_endereco`,
                                                    `cep_endereco`,
                                                    `logradouro_endereco`,
                                                    `numero_endereco`,
                                                    `complemento_endereco`,
                                                    `bairro_endereco`,
                                                    `uf_endereco`,
                                                    `localidade_endereco`)
                                                    VALUES
                                                    (@chave_endereco,
                                                    @cep_endereco,
                                                    @logradouro_endereco,
                                                    @numero_endereco,
                                                    @complemento_endereco,
                                                    @bairro_endereco,
                                                    @uf_endereco,
                                                    @localidade_endereco);";

        public const string SQL_OBTER_CODIGO_ENDERECO_POR_CHAVE = "SELECT id_endereco FROM `heroku_3dc1bcc8f5cdd12`.`endereco` WHERE chave_endereco = @chave_endereco";

        #endregion

        #region CARRO_SQL

        public const string SQL_CADASTRAR_CARRO =   @"INSERT INTO `heroku_3dc1bcc8f5cdd12`.`carro`
                                                    (`placa_carro`,
                                                    `modelo_carro`,
                                                    `marca_carro`,
                                                    `cor_carro`,
                                                    `numero_assentos_carro`,
                                                    `disponibilidade_carro`,
                                                    `descricao_carro`,
                                                    `imagem_carro`,
                                                    `ano_carro`,
                                                    `preco_diaria_carro`,                                                    
                                                    `id_usuario`)
                                                    VALUES
                                                    (@placa_carro,
                                                    @modelo_carro,
                                                    @marca_carro,
                                                    @cor_carro,
                                                    @numero_assentos_carro,
                                                    @disponibilidade_carro,
                                                    @descricao_carro,
                                                    @imagem_carro,
                                                    @ano_carro,
                                                    @preco_diaria_carro,
                                                    @id_usuario)";

        public const string SQL_LISTAR_CARRO = @"SELECT
                                                    c.id_carro AS Codigo,
	                                                c.placa_carro AS Placa,
                                                    c.modelo_carro AS Modelo,
                                                    c.marca_carro AS Marca,
                                                    c.cor_carro AS Cor,
                                                    c.numero_assentos_carro AS NumeroDeAssentos,
                                                    c.disponibilidade_carro AS Disponibilidade,
                                                    c.descricao_carro AS Descricao,
                                                    c.imagem_carro AS Imagem,
                                                    c.ano_carro AS Ano,
                                                    c.preco_diaria_carro AS PrecoDaDiaria,
                                                    u.id_usuario AS CodigoDoUsuarioDoDono,
                                                    u.nome_usuario AS NomeDoDono,
                                                    u.email_usuario AS EmailDoDono,
                                                    u.telefone_usuario AS TelefoneDoDono,
                                                    e.logradouro_endereco AS Localizacao,
                                                    e.uf_endereco AS EstadoLocalizacao,
                                                    (SELECT a.id_aluguel FROM aluguel a WHERE a.id_carro = c.id_carro AND a.situacao_aluguel = 1) AS CodigoAluguelEmAndamento
                                                FROM carro c
                                                INNER JOIN usuario u ON c.id_usuario = u.id_usuario
                                                INNER JOIN endereco e ON u.id_endereco = e.id_endereco
                                                WHERE 1 = 1
                                                {0}";

        public const string SQL_LISTAR_CARRO_FILTRO_GENERICO = @"
                                                                SELECT
	                                                                c.id_carro AS Codigo,
	                                                                c.placa_carro AS Placa,
	                                                                c.modelo_carro AS Modelo,
	                                                                c.marca_carro AS Marca,
	                                                                c.cor_carro AS Cor,
	                                                                c.numero_assentos_carro AS NumeroDeAssentos,
	                                                                c.disponibilidade_carro AS Disponibilidade,
	                                                                c.descricao_carro AS Descricao,
	                                                                c.imagem_carro AS Imagem,
	                                                                c.ano_carro AS Ano,
	                                                                c.preco_diaria_carro AS PrecoDaDiaria,
	                                                                u.id_usuario AS CodigoDoUsuarioDoDono,
	                                                                u.nome_usuario AS NomeDoDono,
	                                                                u.email_usuario AS EmailDoDono,
	                                                                u.telefone_usuario AS TelefoneDoDono,
	                                                                e.logradouro_endereco AS Localizacao,
	                                                                e.uf_endereco AS EstadoLocalizacao
                                                                FROM carro c
                                                                INNER JOIN usuario u ON c.id_usuario = u.id_usuario
                                                                INNER JOIN endereco e ON u.id_endereco = e.id_endereco
                                                                WHERE
                                                                {0}";

        public const string SQL_ATUALIZAR_CARRO = @"UPDATE carro
                                                    SET
                                                    placa_carro = @placa_carro,
                                                    modelo_carro = @modelo_carro,
                                                    marca_carro = @marca_carro,
                                                    cor_carro = @cor_carro,
                                                    numero_assentos_carro = @numero_assentos_carro,
                                                    disponibilidade_carro = @disponibilidade_carro,
                                                    descricao_carro = @descricao_carro,
                                                    imagem_carro = @imagem_carro,
                                                    ano_carro = @ano_carro,
                                                    preco_diaria_carro = @preco_diaria_carro
                                                    WHERE id_carro = @id_carro";

        public const string SQL_DELETAR_CARRO = "DELETE FROM carro WHERE carro.id_carro = @id_carro";

        #endregion

        #region ALUGUEL_SQL

        public const string SQL_CADASTRAR_ALUGUEL = @"INSERT INTO `aluguel`
                                                    (`chave_reserva_aluguel`,
                                                    `data_ini_reserva_aluguel`,
                                                    `data_fim_reserva_aluguel`,
                                                    `preco_total_aluguel`,
                                                    `id_usuario`,
                                                    `id_carro`)
                                                    VALUES
                                                    (@chave_reserva_aluguel,
                                                    @data_ini_reserva_aluguel,
                                                    @data_fim_reserva_aluguel,
                                                    @preco_total_aluguel,
                                                    @id_usuario,
                                                    @id_carro)";

        public const string SQL_LISTAR_ALUGUEL = @"SELECT 
	                                                a.id_aluguel AS Codigo,
                                                    a.chave_reserva_aluguel AS ChaveIdentificacaoReserva,
                                                    a.data_ini_reserva_aluguel AS DataInicioReserva,
                                                    a.data_fim_reserva_aluguel AS DataFimReserva,
                                                    a.data_devolucao_carro_aluguel AS DataDevolucaoCarro,
                                                    a.situacao_aluguel AS Situacao,
                                                    a.data_criacao AS DataDoCadastro,
                                                    a.preco_total_aluguel AS PrecoTotal,
                                                    u.id_usuario AS CodigoUsuarioLocatario,
                                                    u.nome_usuario AS NomeUsuarioLocatario,
                                                    u.cpf_usuario AS CpfUsuarioLocatario,
                                                    u.email_usuario AS EmailUsuarioLocatario,
                                                    u.telefone_usuario AS NumeroTelefoneUsuarioLocatario,
                                                    c.id_carro AS CodigoCarroAlugado,
                                                    c.placa_carro AS PlacaCarroAlugado,
                                                    c.marca_carro AS MarcaCarroAlugado,
                                                    c.modelo_carro AS ModeloCarroAlugado,
                                                    c.numero_assentos_carro AS NumeroDeAssentosCarroAlugado,
                                                    c.preco_diaria_carro AS PrecoDaDiariaCarroAlugado,
                                                    c.id_usuario AS CodigoUsuarioLocador,
                                                    u2.nome_usuario AS NomeUsuarioLocador,
                                                    u2.cpf_usuario AS CpfUsuarioLocador,
                                                    u2.email_usuario AS EmailUsuarioLocador,
                                                    u2.telefone_usuario AS NumeroTelefoneUsuarioLocador
                                                FROM aluguel a
                                                INNER JOIN usuario u ON a.id_usuario = u.id_usuario
                                                INNER JOIN carro c ON a.id_carro = c.id_carro
                                                INNER JOIN usuario u2 ON c.id_usuario = u2.id_usuario
                                                WHERE 1 = 1
                                                {0}";

        public const string SQL_ATUALIZAR_ALUGUEL = @"UPDATE aluguel
                                                    SET
                                                    data_ini_reserva_aluguel = @data_ini_reserva_aluguel,
                                                    data_fim_reserva_aluguel = @data_fim_reserva_aluguel,
                                                    data_devolucao_carro_aluguel = @data_devolucao_carro_aluguel,
                                                    situacao_aluguel = @situacao_aluguel,
                                                    preco_total_aluguel = @preco_total_aluguel,
                                                    id_carro = @id_carro
                                                    WHERE id_aluguel = @id_aluguel";

        #endregion
    }
}
