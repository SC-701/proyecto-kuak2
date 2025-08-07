using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;

namespace DA
{
    public class CuentaDA : ICuentaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public CuentaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerConexion();
        }

        public async Task<Guid> CrearCuenta(CuentaRequest cuenta)
        {
            string sqlQuery = @"sp_Cuenta_Crear";

            Guid resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
            {
                idCuenta = Guid.NewGuid(),
                idUsuario = cuenta.IdUsuario,
                nombre = cuenta.Nombre,
                descripcion = cuenta.Descripcion,
                tipo = cuenta.Tipo
            }, commandType: System.Data.CommandType.StoredProcedure);

            return resultadoQuery;
        }

        public async Task<IEnumerable<CuentaResponse>> ObtenerTodasLasCuentas()
        {
            string sqlQuery = @"sp_Cuenta_Mostrar";

            var cuentas = await _sqlConnection.QueryAsync<CuentaResponse>(sqlQuery, commandType: System.Data.CommandType.StoredProcedure);
            return cuentas;
        }
        public async Task<CuentaResponse> ObtenerCuentaPorId(Guid idCuenta)
        {
            string sqlQuery = @"sp_Cuenta_MostrarPorId";
            var cuenta = await _sqlConnection.QueryFirstOrDefaultAsync<CuentaResponse>(sqlQuery, new
            {
                idCuenta = idCuenta
            }, commandType: System.Data.CommandType.StoredProcedure);
            return cuenta;
        }

        public async Task<Guid> EditarCuenta(Guid idCuenta, CuentaRequest cuenta)
        {
            string sqlQuery = @"sp_Cuenta_Editar";

            await _sqlConnection.ExecuteAsync(sqlQuery, new
            {
                idCuenta = idCuenta,
                nombre = cuenta.Nombre,
                descripcion = cuenta.Descripcion,
                tipo = cuenta.Tipo
            }, commandType: System.Data.CommandType.StoredProcedure);

            return idCuenta;
        }


        public async Task<Guid> EliminarCuenta(Guid idCuenta)
        {
            string sqlQuery = @"sp_Cuenta_Eliminar";

            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>(
                sqlQuery,
                new { idCuenta = idCuenta },
                commandType: System.Data.CommandType.StoredProcedure
            );

            return resultado;
        }

    }
}
