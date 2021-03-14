﻿namespace Repositorio.Constantes
{
    public class AppConstants
    {
        public const string CONNECTION_STRING = "server=us-cdbr-east-03.cleardb.com;user=bf9c7fe9712622;password=e459392c;database=heroku_3dc1bcc8f5cdd12;SSL Mode=None";

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
                                                    `perfil_usuario`)
                                                    VALUES
                                                    (@email_usuario,
                                                    @senha_usuario,
                                                    @nome_usuario,
                                                    @cpf_usuario,
                                                    @telefone_usuario,
                                                    @perfil_usuario);";
    }
}