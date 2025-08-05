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

        public async Task<IEnumerable<CuentaResponse>> MostrarCuentas()
        {
            string sqlQuery = @"sp_Cuenta_Mostrar";

            var cuentas = await _sqlConnection.QueryAsync<CuentaResponse>(sqlQuery, commandType: System.Data.CommandType.StoredProcedure);
            return cuentas;
        }

        public async Task EditarCuenta(CuentaRequest cuenta)
        {
            string sqlQuery = @"sp_Cuenta_Editar";

            await _sqlConnection.ExecuteAsync(sqlQuery, new
            {
                idCuenta = cuenta.IdCuenta,
                nombre = cuenta.Nombre,
                descripcion = cuenta.Descripcion,
                tipo = cuenta.Tipo
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task EliminarCuenta(Guid idCuenta)
        {
            string sqlQuery = @"sp_Cuenta_Eliminar";

            await _sqlConnection.ExecuteAsync(sqlQuery, new
            {
                idCuenta = idCuenta
            }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
