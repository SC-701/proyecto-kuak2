using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

using System.Linq;
using System.Text;
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
            var resultadoQuery = await _conexion.ExecuteScalarAsync<Guid>(
                sqlQuery,
                new
                {
                    idUsuario = Guid.NewGuid(),
                    nombre = usuario.Nombre,
                    email = usuario.Email,
                    fechaCreacion = DateTime.Now,
                    activo = usuario.Activo
                }
            );
            return resultadoQuery;
        }

        public async Task<Guid> EditarUsuario(Guid idUsuario, Usuario usuario)
        {
            await verificarUsuario(idUsuario);

            string sqlQuery = @"EditarUsuario";
            var resultadoQuery = await _conexion.ExecuteScalarAsync<Guid>(
                sqlQuery,
                new
                {
                    idUsuario,
                    nombre = usuario.Nombre,
                    email = usuario.Email,
                    fechaModificacion = DateTime.Now,
                    activo = usuario.Activo
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

        private async Task verificarUsuario(Guid idUsuario)
        {
            Usuario? resultadoVerificacion = await ObtenerUsuarioPorId(idUsuario);

            if (resultadoVerificacion == null)
            {
                throw new Exception("El usuario no existe.");
            }
        }

    }
}
