using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class MovimientoDA : IMovimientoDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public MovimientoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerConexion();
        }

        public async Task<Guid> CrearMovimiento(MovimientoRequest movimiento)
        {
            string sqlQuery = @"sp_Movimiento_Crear";

            Guid idMovimiento = Guid.NewGuid();

            Guid resultado = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
            {
                idMovimiento,
                idCuenta = movimiento.IdCuenta,
                idCategoria = movimiento.IdCategoria,
                idTipoMovimiento = movimiento.IdTipoMovimiento,
                descripcion = movimiento.Descripcion,
                monto = movimiento.Monto,
                fecha = movimiento.Fecha
            }, commandType: System.Data.CommandType.StoredProcedure);

            return resultado;
        }

        public async Task<IEnumerable<TipoMovimientoResponse>> MostrarMovimientos()
        {
            string sqlQuery = @"sp_Movimiento_Mostrar";

            var resultados = await _sqlConnection.QueryAsync<TipoMovimientoResponse>(sqlQuery, commandType: System.Data.CommandType.StoredProcedure);

            return resultados;
        }

        public async Task EditarMovimiento(MovimientoRequest movimiento)
        {
            string sqlQuery = @"sp_Movimiento_Editar";

            await _sqlConnection.ExecuteAsync(sqlQuery, new
            {
                idMovimiento = movimiento.IdMovimiento,
                idCuenta = movimiento.IdCuenta,
                idCategoria = movimiento.IdCategoria,
                idTipoMovimiento = movimiento.IdTipoMovimiento,
                descripcion = movimiento.Descripcion,
                monto = movimiento.Monto,
                fecha = movimiento.Fecha
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task EliminarMovimiento(Guid idMovimiento)
        {
            string sqlQuery = @"sp_Movimiento_Eliminar";

            await _sqlConnection.ExecuteAsync(sqlQuery, new
            {
                idMovimiento
            }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}