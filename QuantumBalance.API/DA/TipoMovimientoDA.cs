using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class TipoMovimientoDA : ITipoMovimientoDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;
        public TipoMovimientoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerConexion();
        }
        public async Task<IEnumerable<TipoMovimientoResponse>> ObtenerTiposMovimientos()
        {
            string sqlQuery = @"sp_TipoMovimiento_Mostrar";

            var resultados = await _sqlConnection.QueryAsync<TipoMovimientoResponse>(sqlQuery, commandType: System.Data.CommandType.StoredProcedure);

            return resultados;
        }

        public async Task<TipoMovimientoResponse?> ObtenerTipoMovimientoPorId(Guid idTipoMovimiento)
        {
            string sqlQuery = @"sp_TipoMovimiento_ObtenerPorId"; 

            var resultado = await _sqlConnection.QueryFirstOrDefaultAsync<TipoMovimientoResponse>(
                sqlQuery,
                new { Id = idTipoMovimiento },
                commandType: System.Data.CommandType.StoredProcedure
            );

            return resultado;
        }

    }
}
