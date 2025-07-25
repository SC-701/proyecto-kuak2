using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace DA
{
    public class UsuarioDA : IUsuarioDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _conexion;

        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _conexion = _repositorioDapper.ObtenerConexion();
        }

        public async Task<Guid> CrearUsuario(Usuario usuario)
        {
            string sqlQuery = @"CrearUsuario";
            Guid nuevoId = Guid.NewGuid();

            var resultadoQuery = await _conexion.ExecuteScalarAsync<Guid>(
                sqlQuery,
                new
                {
                    idUsuario = nuevoId,
                    nombre = usuario.Nombre,
                    primerApellido = usuario.PrimerApellido,
                    segundoApellido = usuario.SegundoApellido,
                    email = usuario.Email,
                    password = usuario.Password,
                    monedaPrincipal = usuario.MonedaPrincipal,
                    fechaCreacion = DateTime.Now,
                    fechaUltimoAcceso = usuario.FechaUltimoAcceso,
                    estado = usuario.Estado
                }
            );
            return resultadoQuery;
        }

        public async Task<Guid> EditarUsuario(Guid idUsuario, Usuario usuario)
        {
            await VerificarUsuario(idUsuario);

            string sqlQuery = @"EditarUsuario";
            var resultadoQuery = await _conexion.ExecuteScalarAsync<Guid>(
                sqlQuery,
                new
                {
                    idUsuario,
                    nombre = usuario.Nombre,
                    primerApellido = usuario.PrimerApellido,
                    segundoApellido = usuario.SegundoApellido,
                    email = usuario.Email,
                    password = usuario.Password,
                    monedaPrincipal = usuario.MonedaPrincipal,
                    fechaModificacion = DateTime.Now,
                    fechaUltimoAcceso = usuario.FechaUltimoAcceso,
                    estado = usuario.Estado
                }
            );
            return resultadoQuery;
        }

        public async Task<Usuario> ObtenerUsuarioPorId(Guid idUsuario)
        {
            string sqlQuery = @"ObtenerUsuarioPorId";
            var resultadoQuery = await _conexion.QuerySingleOrDefaultAsync<Usuario>(
                sqlQuery,
                new { idUsuario }
            );
            return resultadoQuery;
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            string sqlQuery = @"ObtenerUsuarios";
            var resultadoQuery = await _conexion.QueryAsync<Usuario>(sqlQuery);
            return resultadoQuery;
        }

        private async Task VerificarUsuario(Guid idUsuario)
        {
            Usuario? resultadoVerificacion = await ObtenerUsuarioPorId(idUsuario);

            if (resultadoVerificacion == null)
            {
                throw new Exception("El usuario no existe.");
            }
        }
    }
}
