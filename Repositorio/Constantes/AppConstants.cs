namespace Repositorio.Constantes
{
    public class AppConstants
    {
        public const string CONNECTION_STRING = "server=us-cdbr-east-03.cleardb.com;user=bf9c7fe9712622;password=e459392c;database=heroku_3dc1bcc8f5cdd12;SSL Mode=None";

        public const string CHAVE_CIFRA = "1e64fdce-e561-4f3d-bb78-0d7c8c86d14b";

        public const string SQL_OBTER_USUARIO_POR_EMAIL = @"SELECT 
	                                                        id_usuario AS Codigo,
                                                            email_usuario AS Email,
                                                            senha_usuario AS Senha,
                                                            nome_usuario AS Nome,
                                                            cpf_usuario AS Cpf,
                                                            telefone_usuario AS NumeroTelefone,
                                                            perfil_usuario AS Perfil,
                                                            data_alteracao AS DataUltimaAlteracaoAlteracao,
                                                            data_criacao AS DataCriacaoRegistro
                                                        FROM usuario
                                                        WHERE email_usuario = @email_usuario";

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

        public const string SQL_CADASTRAR_ENDERECO = @"INSERT INTO `heroku_3dc1bcc8f5cdd12`.`endereco`
                                                    (`chave_endereco`,
                                                    `cep_endereco`,
                                                    `logradouro_endereco`,
                                                    `numero_endereco`,
                                                    `complemento_endereco`,
                                                    `bairro_endereco`,
                                                    `uf_endereco`)
                                                    VALUES
                                                    (@chave_endereco,
                                                    @cep_endereco,
                                                    @logradouro_endereco,
                                                    @numero_endereco,
                                                    @complemento_endereco,
                                                    @bairro_endereco,
                                                    @uf_endereco);";

        public const string SQL_OBTER_CODIGO_ENDERECO_POR_CHAVE = "SELECT id_endereco FROM `heroku_3dc1bcc8f5cdd12`.`endereco` WHERE chave_endereco = @chave_endereco";
    }
}
