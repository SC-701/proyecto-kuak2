using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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

        public async Task<IEnumerable<MovimientoResponse>> ObtenerTodosLosMovimientos()
        {
            string sqlQuery = @"sp_Movimiento_Mostrar";

            var resultados = await _sqlConnection.QueryAsync<MovimientoResponse>(sqlQuery, commandType: System.Data.CommandType.StoredProcedure);

            return resultados;
        }


        public async Task<MovimientoResponse> ObtenerMovimientoPorId(Guid idMovimiento)
        {
            string sqlQuery = @"sp_Movimiento_ObtenerPorId";
            var resultado = await _sqlConnection.QueryFirstOrDefaultAsync<MovimientoResponse>(sqlQuery, new
            {
                idMovimiento
            }, commandType: System.Data.CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<Guid> EditarMovimiento(Guid idMovimiento, MovimientoRequest movimiento)
        {
            string sqlQuery = @"sp_Movimiento_Editar";

            await _sqlConnection.ExecuteAsync(sqlQuery, new
            {
                idMovimiento,
                idCuenta = movimiento.IdCuenta,
                idCategoria = movimiento.IdCategoria,
                idTipoMovimiento = movimiento.IdTipoMovimiento,
                descripcion = movimiento.Descripcion,
                monto = movimiento.Monto,
                fecha = movimiento.Fecha
            }, commandType: System.Data.CommandType.StoredProcedure);

            return idMovimiento;
        }


        public async Task<Guid> EliminarMovimiento(Guid idMovimiento)
        {
            string sqlQuery = @"sp_Movimiento_Eliminar";

            await _sqlConnection.ExecuteAsync(sqlQuery, new
            {
                idMovimiento
            }, commandType: System.Data.CommandType.StoredProcedure);

            return idMovimiento; 
        }

    }
}