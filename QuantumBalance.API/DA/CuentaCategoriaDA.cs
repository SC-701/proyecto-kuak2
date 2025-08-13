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
    public class CuentaCategoriaDA : ICuentaCategoriaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public CuentaCategoriaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerConexion();
        }

        public async Task<Guid> CrearCuentaCategoria(CuentaCategoriaRequest cuentaCategoria)
        {
            string sqlQuery = @"sp_CuentaCategoria_Crear";

            Guid idCuentaCategoria = Guid.NewGuid();

            Guid resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
            {
                cuentaCategoria.IdCuenta,
                cuentaCategoria.IdCategoria
            }, commandType: System.Data.CommandType.StoredProcedure);

            return resultadoQuery;
        }

        public async Task<IEnumerable<CuentaCategoriaResponse>> ObtenerCuentaCategorias()
        {
            string sqlQuery = @"sp_CuentaCategoria_Mostrar";

            var resultado = await _sqlConnection.QueryAsync<CuentaCategoriaResponse>(sqlQuery, commandType: System.Data.CommandType.StoredProcedure);

            return resultado;
        }

        public async Task<CuentaCategoriaResponse?> ObtenerCuentaCategoriaPorId(Guid idCuentaCategoria)
        {
            string sqlQuery = @"sp_CuentaCategoria_ObtenerPorId";

            var resultado = await _sqlConnection.QueryFirstOrDefaultAsync<CuentaCategoriaResponse>(sqlQuery, new
            {
                idCuentaCategoria
            }, commandType: System.Data.CommandType.StoredProcedure);

            return resultado;
        }

        public async Task<Guid> EditarCuentaCategoria(Guid idCuentaCategoria, CuentaCategoriaRequest cuentaCategoria)
        {
            string sqlQuery = @"sp_CuentaCategoria_Editar";

            await _sqlConnection.ExecuteAsync(sqlQuery, new
            {
                idCuentaCategoria,
                cuentaCategoria.IdCuenta,
                cuentaCategoria.IdCategoria
            }, commandType: System.Data.CommandType.StoredProcedure);

            return idCuentaCategoria;
        }

        public async Task<Guid> EliminarCuentaCategoria(Guid idCuentaCategoria)
        {
            string sqlQuery = @"sp_CuentaCategoria_Eliminar";

            await _sqlConnection.ExecuteAsync(sqlQuery, new
            {
                idCuentaCategoria
            }, commandType: System.Data.CommandType.StoredProcedure);

            return idCuentaCategoria;
        }
    }
}
